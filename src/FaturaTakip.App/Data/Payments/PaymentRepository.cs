using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Payments;

public sealed class PaymentRepository
{
    private readonly string _databasePath;
    private readonly string _rootDirectory;
    private readonly string _paymentAttachmentDirectory;

    public PaymentRepository(string databasePath)
    {
        _databasePath = Path.GetFullPath(databasePath);
        _rootDirectory = ResolveRootDirectory(_databasePath);
        _paymentAttachmentDirectory = Path.Combine(_rootDirectory, "attachments", "payments");
    }

    public IReadOnlyList<Payment> GetAll()
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = SelectSql + """

            ORDER BY payment_date DESC, id DESC;
            """;

        using var reader = command.ExecuteReader();
        var payments = new List<Payment>();
        while (reader.Read())
        {
            payments.Add(ReadPayment(reader));
        }

        return payments;
    }

    public IReadOnlyList<Payment> GetForInvoice(long invoiceId)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = SelectSql + """

            WHERE invoice_id = $invoiceId
            ORDER BY payment_date DESC, id DESC;
            """;
        command.Parameters.AddWithValue("$invoiceId", invoiceId);

        using var reader = command.ExecuteReader();
        var payments = new List<Payment>();
        while (reader.Read())
        {
            payments.Add(ReadPayment(reader));
        }

        return payments;
    }

    public decimal GetTotalPaid(long invoiceId)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT COALESCE(SUM(CAST(amount AS REAL)), 0)
            FROM payments
            WHERE invoice_id = $invoiceId;
            """;
        command.Parameters.AddWithValue("$invoiceId", invoiceId);

        return Convert.ToDecimal(command.ExecuteScalar(), CultureInfo.InvariantCulture);
    }

    public Payment Add(PaymentInput input)
    {
        var normalized = Normalize(input);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        var invoiceAmount = GetInvoiceAmount(connection, transaction, normalized.InvoiceId);
        if (invoiceAmount <= 0)
        {
            throw new InvalidOperationException("Tutarı sıfır olan faturaya ödeme eklenemez.");
        }

        var paidBefore = GetTotalPaid(connection, transaction, normalized.InvoiceId);
        if (paidBefore + normalized.Amount > invoiceAmount)
        {
            throw new InvalidOperationException("Ödeme tutarı kalan tutarı aşamaz.");
        }

        var now = DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture);

        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            INSERT INTO payments (
                invoice_id,
                payment_date,
                amount,
                description,
                created_at,
                updated_at)
            VALUES (
                $invoiceId,
                $paymentDate,
                $amount,
                $description,
                $createdAt,
                $updatedAt)
            RETURNING id;
            """;
        command.Parameters.AddWithValue("$invoiceId", normalized.InvoiceId);
        command.Parameters.AddWithValue("$paymentDate", FormatDate(normalized.PaymentDate));
        command.Parameters.AddWithValue("$amount", FormatDecimal(normalized.Amount));
        command.Parameters.AddWithValue("$description", normalized.Description);
        command.Parameters.AddWithValue("$createdAt", now);
        command.Parameters.AddWithValue("$updatedAt", now);

        var id = Convert.ToInt64(command.ExecuteScalar(), CultureInfo.InvariantCulture);
        RefreshInvoiceStatus(connection, transaction, normalized.InvoiceId, invoiceAmount, now);

        using var readCommand = connection.CreateCommand();
        readCommand.Transaction = transaction;
        readCommand.CommandText = SelectSql + "\nWHERE id = $id;";
        readCommand.Parameters.AddWithValue("$id", id);

        using var reader = readCommand.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Ödeme kaydı okunamadı.");
        }

        var payment = ReadPayment(reader);
        transaction.Commit();
        return payment;
    }

    public Payment AttachPdf(long id, string sourcePdfPath)
    {
        var payment = GetRequired(id);
        var sourceFullPath = ValidatePdfSource(sourcePdfPath);
        var hash = ComputeSha256Hash(sourceFullPath);
        var relativePath = CopyPdfToAttachmentDirectory(payment, sourceFullPath, hash);
        var originalFileName = Path.GetFileName(sourceFullPath);
        var now = DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE payments
            SET pdf_file_path = $pdfFilePath,
                pdf_original_file_name = $pdfOriginalFileName,
                pdf_sha256_hash = $pdfSha256Hash,
                pdf_attached_at = $pdfAttachedAt,
                updated_at = $updatedAt
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$pdfFilePath", relativePath);
        command.Parameters.AddWithValue("$pdfOriginalFileName", originalFileName);
        command.Parameters.AddWithValue("$pdfSha256Hash", hash);
        command.Parameters.AddWithValue("$pdfAttachedAt", now);
        command.Parameters.AddWithValue("$updatedAt", now);

        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("PDF eklenecek ödeme kaydı bulunamadı.");
        }

        return GetRequired(id);
    }

    public string GetPdfAbsolutePath(Payment payment)
    {
        if (!payment.HasPdf)
        {
            return string.Empty;
        }

        return Path.IsPathRooted(payment.PdfFilePath)
            ? payment.PdfFilePath
            : Path.GetFullPath(Path.Combine(_rootDirectory, payment.PdfFilePath));
    }

    public bool PdfFileExists(Payment payment)
    {
        var path = GetPdfAbsolutePath(payment);
        return !string.IsNullOrWhiteSpace(path) && File.Exists(path);
    }

    public bool IsPdfMissing(Payment payment)
    {
        return !payment.HasPdf || !PdfFileExists(payment);
    }

    private Payment GetRequired(long id)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = SelectSql + "\nWHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Ödeme kaydı bulunamadı.");
        }

        return ReadPayment(reader);
    }

    private static PaymentInput Normalize(PaymentInput input)
    {
        var normalized = input with
        {
            PaymentDate = input.PaymentDate.Date,
            Description = input.Description.Trim(),
        };

        if (normalized.InvoiceId <= 0)
        {
            throw new InvalidOperationException("Ödeme için geçerli bir fatura seçin.");
        }

        if (normalized.Amount <= 0)
        {
            throw new InvalidOperationException("Ödeme tutarı sıfırdan büyük olmalıdır.");
        }

        if (normalized.Description.Length > 500)
        {
            throw new InvalidOperationException("Ödeme açıklaması 500 karakterden uzun olamaz.");
        }

        return normalized;
    }

    private static decimal GetInvoiceAmount(SqliteConnection connection, SqliteTransaction transaction, long invoiceId)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            SELECT amount
            FROM invoices
            WHERE id = $invoiceId;
            """;
        command.Parameters.AddWithValue("$invoiceId", invoiceId);

        var result = command.ExecuteScalar();
        if (result is null)
        {
            throw new InvalidOperationException("Ödeme eklenecek fatura bulunamadı.");
        }

        return ParseDecimal(Convert.ToString(result, CultureInfo.InvariantCulture) ?? "0");
    }

    private static void RefreshInvoiceStatus(
        SqliteConnection connection,
        SqliteTransaction transaction,
        long invoiceId,
        decimal invoiceAmount,
        string updatedAt)
    {
        var paidAmount = GetTotalPaid(connection, transaction, invoiceId);
        var status = paidAmount >= invoiceAmount && invoiceAmount > 0
            ? "paid"
            : "unpaid";

        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            UPDATE invoices
            SET status = CASE
                    WHEN status = 'canceled' THEN status
                    ELSE $status
                END,
                updated_at = $updatedAt
            WHERE id = $invoiceId;
            """;
        command.Parameters.AddWithValue("$status", status);
        command.Parameters.AddWithValue("$updatedAt", updatedAt);
        command.Parameters.AddWithValue("$invoiceId", invoiceId);
        command.ExecuteNonQuery();
    }

    private static decimal GetTotalPaid(SqliteConnection connection, SqliteTransaction transaction, long invoiceId)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            SELECT COALESCE(SUM(CAST(amount AS REAL)), 0)
            FROM payments
            WHERE invoice_id = $invoiceId;
            """;
        command.Parameters.AddWithValue("$invoiceId", invoiceId);

        return Convert.ToDecimal(command.ExecuteScalar(), CultureInfo.InvariantCulture);
    }

    private static Payment ReadPayment(SqliteDataReader reader)
    {
        return new Payment
        {
            Id = reader.GetInt64(0),
            InvoiceId = reader.GetInt64(1),
            PaymentDate = ParseDate(reader.GetString(2)),
            Amount = ParseDecimal(reader.GetString(3)),
            Description = reader.GetString(4),
            PdfFilePath = reader.GetString(5),
            PdfOriginalFileName = reader.GetString(6),
            PdfSha256Hash = reader.GetString(7),
            PdfAttachedAt = ParseNullableDateTimeOffset(reader.GetString(8)),
            CreatedAt = ParseDateTimeOffset(reader.GetString(9)),
            UpdatedAt = ParseDateTimeOffset(reader.GetString(10)),
        };
    }

    private string CopyPdfToAttachmentDirectory(Payment payment, string sourceFullPath, string hash)
    {
        var targetDirectory = Path.Combine(
            _paymentAttachmentDirectory,
            payment.PaymentDate.Year.ToString("D4", CultureInfo.InvariantCulture),
            payment.PaymentDate.Month.ToString("D2", CultureInfo.InvariantCulture));
        Directory.CreateDirectory(targetDirectory);

        var hashPrefix = hash[..Math.Min(12, hash.Length)];
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        var targetFileName = $"payment-{payment.Id}-{timestamp}-{hashPrefix}.pdf";
        var targetFullPath = Path.Combine(targetDirectory, targetFileName);

        if (File.Exists(targetFullPath))
        {
            var suffix = Guid.NewGuid().ToString("N")[..8];
            targetFileName = $"payment-{payment.Id}-{timestamp}-{hashPrefix}-{suffix}.pdf";
            targetFullPath = Path.Combine(targetDirectory, targetFileName);
        }

        File.Copy(sourceFullPath, targetFullPath, overwrite: false);
        return Path.GetRelativePath(_rootDirectory, targetFullPath);
    }

    private static string ValidatePdfSource(string sourcePdfPath)
    {
        if (string.IsNullOrWhiteSpace(sourcePdfPath))
        {
            throw new InvalidOperationException("PDF dosyası seçilmelidir.");
        }

        var fullPath = Path.GetFullPath(sourcePdfPath);
        if (!File.Exists(fullPath))
        {
            throw new InvalidOperationException("Seçilen PDF dosyası bulunamadı.");
        }

        if (!string.Equals(Path.GetExtension(fullPath), ".pdf", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Yalnızca PDF dosyası eklenebilir.");
        }

        var fileInfo = new FileInfo(fullPath);
        if (fileInfo.Length == 0)
        {
            throw new InvalidOperationException("Seçilen PDF dosyası boş.");
        }

        using var stream = File.OpenRead(fullPath);
        Span<byte> signature = stackalloc byte[4];
        if (stream.Read(signature) < signature.Length ||
            signature[0] != 0x25 ||
            signature[1] != 0x50 ||
            signature[2] != 0x44 ||
            signature[3] != 0x46)
        {
            throw new InvalidOperationException("Seçilen dosya geçerli bir PDF olarak görünmüyor.");
        }

        return fullPath;
    }

    private static string ComputeSha256Hash(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        return Convert.ToHexString(SHA256.HashData(stream)).ToLowerInvariant();
    }

    private static string FormatDate(DateTime date)
    {
        return date.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    private static DateTime ParseDate(string value)
    {
        return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    private static string FormatDecimal(decimal value)
    {
        return value.ToString("0.#############################", CultureInfo.InvariantCulture);
    }

    private static decimal ParseDecimal(string value)
    {
        return decimal.Parse(value, CultureInfo.InvariantCulture);
    }

    private static DateTimeOffset ParseDateTimeOffset(string value)
    {
        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed))
        {
            return parsed;
        }

        return DateTimeOffset.MinValue;
    }

    private static DateTimeOffset? ParseNullableDateTimeOffset(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : ParseDateTimeOffset(value);
    }

    private static string ResolveRootDirectory(string databasePath)
    {
        var databaseDirectory = Path.GetDirectoryName(databasePath);
        if (!string.IsNullOrWhiteSpace(databaseDirectory))
        {
            var parent = Directory.GetParent(databaseDirectory);
            if (parent is not null)
            {
                return parent.FullName;
            }
        }

        return AppContext.BaseDirectory;
    }

    private const string SelectSql = """
        SELECT
            id,
            invoice_id,
            payment_date,
            amount,
            description,
            pdf_file_path,
            pdf_original_file_name,
            pdf_sha256_hash,
            pdf_attached_at,
            created_at,
            updated_at
        FROM payments
        """;
}
