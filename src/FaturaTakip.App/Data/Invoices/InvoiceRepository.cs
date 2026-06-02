using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using FaturaTakip.App.Data.AuditLogs;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Invoices;

public sealed class InvoiceRepository
{
    private readonly string _databasePath;
    private readonly string _rootDirectory;
    private readonly string _invoiceAttachmentDirectory;
    private readonly AuditLogRepository _auditLogRepository;

    public InvoiceRepository(string databasePath)
    {
        _databasePath = Path.GetFullPath(databasePath);
        _rootDirectory = ResolveRootDirectory(_databasePath);
        _invoiceAttachmentDirectory = Path.Combine(_rootDirectory, "attachments", "invoices");
        _auditLogRepository = new AuditLogRepository(_databasePath);
    }

    public IReadOnlyList<Invoice> GetAll()
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = SelectSql + """

            ORDER BY i.invoice_year DESC, i.invoice_month DESC, i.due_date DESC, i.invoice_no COLLATE NOCASE;
            """;

        using var reader = command.ExecuteReader();
        var invoices = new List<Invoice>();
        while (reader.Read())
        {
            invoices.Add(ReadInvoice(reader));
        }

        return invoices;
    }

    public Invoice Add(InvoiceInput input)
    {
        var normalized = Normalize(input);
        var subscription = GetSubscriptionSnapshot(normalized.SubscriptionId);
        EnsureUniqueInvoiceNo(normalized.SubscriptionId, normalized.InvoiceNo, ignoredId: null);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        var now = DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture);
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO invoices (
                subscription_id,
                invoice_type_id,
                invoice_year,
                invoice_month,
                invoice_date,
                due_date,
                invoice_no,
                amount,
                usage_amount,
                usage_unit,
                status,
                description,
                created_at,
                updated_at)
            VALUES (
                $subscriptionId,
                $invoiceTypeId,
                $invoiceYear,
                $invoiceMonth,
                $invoiceDate,
                $dueDate,
                $invoiceNo,
                $amount,
                $usageAmount,
                $usageUnit,
                'unpaid',
                $description,
                $createdAt,
                $updatedAt)
            RETURNING id;
            """;
        BindInput(command, normalized, subscription.InvoiceTypeId);
        command.Parameters.AddWithValue("$createdAt", now);
        command.Parameters.AddWithValue("$updatedAt", now);

        var id = Convert.ToInt64(command.ExecuteScalar(), CultureInfo.InvariantCulture);
        var created = GetRequired(id);
        _auditLogRepository.Add("invoice_created", "invoices", created.Id, null, created, "Fatura olusturuldu.");
        return created;
    }

    public Invoice Update(long id, InvoiceInput input)
    {
        var normalized = Normalize(input);
        var subscription = GetSubscriptionSnapshot(normalized.SubscriptionId);
        EnsureUniqueInvoiceNo(normalized.SubscriptionId, normalized.InvoiceNo, id);
        var previous = GetRequired(id);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE invoices
            SET subscription_id = $subscriptionId,
                invoice_type_id = $invoiceTypeId,
                invoice_year = $invoiceYear,
                invoice_month = $invoiceMonth,
                invoice_date = $invoiceDate,
                due_date = $dueDate,
                invoice_no = $invoiceNo,
                amount = $amount,
                status = CASE
                    WHEN status = 'canceled' THEN status
                    WHEN (SELECT COALESCE(SUM(CAST(p.amount AS REAL)), 0) FROM payments p WHERE p.invoice_id = $id) >= CAST($amount AS REAL)
                         AND CAST($amount AS REAL) > 0 THEN 'paid'
                    ELSE 'unpaid'
                END,
                usage_amount = $usageAmount,
                usage_unit = $usageUnit,
                description = $description,
                updated_at = $updatedAt
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);
        BindInput(command, normalized, subscription.InvoiceTypeId);
        command.Parameters.AddWithValue("$updatedAt", DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture));

        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Güncellenecek fatura bulunamadı.");
        }

        var updated = GetRequired(id);
        _auditLogRepository.Add("invoice_updated", "invoices", updated.Id, previous, updated, "Fatura guncellendi.");
        return updated;
    }

    public Invoice AttachPdf(long id, string sourcePdfPath)
    {
        var previous = GetRequired(id);
        var sourceFullPath = ValidatePdfSource(sourcePdfPath);
        var hash = ComputeSha256Hash(sourceFullPath);
        var relativePath = CopyPdfToAttachmentDirectory(previous, sourceFullPath, hash);
        var originalFileName = Path.GetFileName(sourceFullPath);
        var now = DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE invoices
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
            throw new InvalidOperationException("PDF eklenecek fatura bulunamadı.");
        }

        var updated = GetRequired(id);
        _auditLogRepository.Add("invoice_pdf_attached", "invoices", updated.Id, previous, updated, "Fatura PDF evraki eklendi veya degistirildi.");
        return updated;
    }

    public string GetPdfAbsolutePath(Invoice invoice)
    {
        if (!invoice.HasPdf)
        {
            return string.Empty;
        }

        return Path.IsPathRooted(invoice.PdfFilePath)
            ? invoice.PdfFilePath
            : Path.GetFullPath(Path.Combine(_rootDirectory, invoice.PdfFilePath));
    }

    public bool PdfFileExists(Invoice invoice)
    {
        var path = GetPdfAbsolutePath(invoice);
        return !string.IsNullOrWhiteSpace(path) && File.Exists(path);
    }

    public bool IsPdfMissing(Invoice invoice)
    {
        return !invoice.HasPdf || !PdfFileExists(invoice);
    }

    public static string? GetDueDateWarning(InvoiceInput input)
    {
        return input.DueDate.Date < input.InvoiceDate.Date
            ? "Son ödeme tarihi fatura tarihinden önce."
            : null;
    }

    private Invoice GetRequired(long id)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = SelectSql + "\nWHERE i.id = $id;";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Fatura bulunamadı.");
        }

        return ReadInvoice(reader);
    }

    private SubscriptionSnapshot GetSubscriptionSnapshot(long subscriptionId)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, invoice_type_id, default_usage_unit
            FROM subscriptions
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$id", subscriptionId);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Geçerli bir abonelik seçin.");
        }

        return new SubscriptionSnapshot(
            reader.GetInt64(0),
            reader.GetInt64(1),
            reader.GetString(2));
    }

    private void EnsureUniqueInvoiceNo(long subscriptionId, string invoiceNo, long? ignoredId)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = ignoredId is null
            ? """
                SELECT COUNT(1)
                FROM invoices
                WHERE subscription_id = $subscriptionId
                  AND invoice_no = $invoiceNo COLLATE NOCASE;
                """
            : """
                SELECT COUNT(1)
                FROM invoices
                WHERE subscription_id = $subscriptionId
                  AND invoice_no = $invoiceNo COLLATE NOCASE
                  AND id <> $id;
                """;
        command.Parameters.AddWithValue("$subscriptionId", subscriptionId);
        command.Parameters.AddWithValue("$invoiceNo", invoiceNo);
        if (ignoredId is not null)
        {
            command.Parameters.AddWithValue("$id", ignoredId.Value);
        }

        if (Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture) > 0)
        {
            throw new InvalidOperationException("Bu abonelikte aynı fatura numarası zaten kayıtlı.");
        }
    }

    private static InvoiceInput Normalize(InvoiceInput input)
    {
        var normalized = input with
        {
            InvoiceNo = input.InvoiceNo.Trim(),
            UsageUnit = input.UsageUnit.Trim(),
            Description = input.Description.Trim(),
            InvoiceDate = input.InvoiceDate.Date,
            DueDate = input.DueDate.Date,
        };

        if (normalized.SubscriptionId <= 0)
        {
            throw new InvalidOperationException("Abonelik seçimi zorunludur.");
        }

        if (normalized.InvoiceYear < 2000 || normalized.InvoiceYear > 2100)
        {
            throw new InvalidOperationException("Dönem yılı 2000 ile 2100 arasında olmalıdır.");
        }

        if (normalized.InvoiceMonth < 1 || normalized.InvoiceMonth > 12)
        {
            throw new InvalidOperationException("Dönem ayı 1 ile 12 arasında olmalıdır.");
        }

        if (string.IsNullOrWhiteSpace(normalized.InvoiceNo))
        {
            throw new InvalidOperationException("Fatura numarası zorunludur.");
        }

        if (normalized.Amount < 0)
        {
            throw new InvalidOperationException("Fatura tutarı negatif olamaz.");
        }

        if (normalized.UsageAmount < 0)
        {
            throw new InvalidOperationException("Kullanım miktarı negatif olamaz.");
        }

        EnsureMaxLength(normalized.InvoiceNo, 100, "Fatura numarası");
        EnsureMaxLength(normalized.UsageUnit, 30, "Kullanım birimi");
        EnsureMaxLength(normalized.Description, 500, "Açıklama");

        return normalized;
    }

    private static void EnsureMaxLength(string value, int maxLength, string label)
    {
        if (value.Length > maxLength)
        {
            throw new InvalidOperationException($"{label} {maxLength} karakterden uzun olamaz.");
        }
    }

    private static void BindInput(SqliteCommand command, InvoiceInput input, long invoiceTypeId)
    {
        command.Parameters.AddWithValue("$subscriptionId", input.SubscriptionId);
        command.Parameters.AddWithValue("$invoiceTypeId", invoiceTypeId);
        command.Parameters.AddWithValue("$invoiceYear", input.InvoiceYear);
        command.Parameters.AddWithValue("$invoiceMonth", input.InvoiceMonth);
        command.Parameters.AddWithValue("$invoiceDate", FormatDate(input.InvoiceDate));
        command.Parameters.AddWithValue("$dueDate", FormatDate(input.DueDate));
        command.Parameters.AddWithValue("$invoiceNo", input.InvoiceNo);
        command.Parameters.AddWithValue("$amount", FormatDecimal(input.Amount));
        command.Parameters.AddWithValue("$usageAmount", FormatDecimal(input.UsageAmount));
        command.Parameters.AddWithValue("$usageUnit", input.UsageUnit);
        command.Parameters.AddWithValue("$description", input.Description);
    }

    private static Invoice ReadInvoice(SqliteDataReader reader)
    {
        return new Invoice
        {
            Id = reader.GetInt64(0),
            SubscriptionId = reader.GetInt64(1),
            InvoiceTypeId = reader.GetInt64(2),
            InvoiceTypeName = reader.GetString(3),
            SubscriptionName = reader.GetString(4),
            InstitutionName = reader.GetString(5),
            InvoiceYear = reader.GetInt32(6),
            InvoiceMonth = reader.GetInt32(7),
            InvoiceDate = ParseDate(reader.GetString(8)),
            DueDate = ParseDate(reader.GetString(9)),
            InvoiceNo = reader.GetString(10),
            Amount = ParseDecimal(reader.GetString(11)),
            PaidAmount = ParseReaderDecimal(reader, 22),
            UsageAmount = ParseDecimal(reader.GetString(12)),
            UsageUnit = reader.GetString(13),
            Status = reader.GetString(14),
            Description = reader.GetString(15),
            PdfFilePath = reader.GetString(16),
            PdfOriginalFileName = reader.GetString(17),
            PdfSha256Hash = reader.GetString(18),
            PdfAttachedAt = ParseNullableDateTimeOffset(reader.GetString(19)),
            CreatedAt = ParseDateTimeOffset(reader.GetString(20)),
            UpdatedAt = ParseDateTimeOffset(reader.GetString(21)),
        };
    }

    private string CopyPdfToAttachmentDirectory(Invoice invoice, string sourceFullPath, string hash)
    {
        var targetDirectory = Path.Combine(
            _invoiceAttachmentDirectory,
            invoice.InvoiceYear.ToString("D4", CultureInfo.InvariantCulture),
            invoice.InvoiceMonth.ToString("D2", CultureInfo.InvariantCulture));
        Directory.CreateDirectory(targetDirectory);

        var hashPrefix = hash[..Math.Min(12, hash.Length)];
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        var targetFileName = $"invoice-{invoice.Id}-{timestamp}-{hashPrefix}.pdf";
        var targetFullPath = Path.Combine(targetDirectory, targetFileName);

        if (File.Exists(targetFullPath))
        {
            var suffix = Guid.NewGuid().ToString("N")[..8];
            targetFileName = $"invoice-{invoice.Id}-{timestamp}-{hashPrefix}-{suffix}.pdf";
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

    private static decimal ParseReaderDecimal(SqliteDataReader reader, int ordinal)
    {
        return decimal.Parse(
            Convert.ToString(reader.GetValue(ordinal), CultureInfo.InvariantCulture) ?? "0",
            CultureInfo.InvariantCulture);
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
            i.id,
            i.subscription_id,
            i.invoice_type_id,
            it.name AS invoice_type_name,
            s.subscription_name,
            s.institution_name,
            i.invoice_year,
            i.invoice_month,
            i.invoice_date,
            i.due_date,
            i.invoice_no,
            i.amount,
            i.usage_amount,
            i.usage_unit,
            i.status,
            i.description,
            i.pdf_file_path,
            i.pdf_original_file_name,
            i.pdf_sha256_hash,
            i.pdf_attached_at,
            i.created_at,
            i.updated_at,
            COALESCE((SELECT SUM(CAST(p.amount AS REAL)) FROM payments p WHERE p.invoice_id = i.id), 0) AS paid_amount
        FROM invoices i
        INNER JOIN subscriptions s ON s.id = i.subscription_id
        INNER JOIN invoice_types it ON it.id = i.invoice_type_id
        """;

    private sealed record SubscriptionSnapshot(long Id, long InvoiceTypeId, string DefaultUsageUnit);
}
