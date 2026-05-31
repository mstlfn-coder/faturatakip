using System.Globalization;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Payments;

public sealed class PaymentRepository
{
    private readonly string _databasePath;

    public PaymentRepository(string databasePath)
    {
        _databasePath = databasePath;
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
            CreatedAt = ParseDateTimeOffset(reader.GetString(5)),
            UpdatedAt = ParseDateTimeOffset(reader.GetString(6)),
        };
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

    private const string SelectSql = """
        SELECT
            id,
            invoice_id,
            payment_date,
            amount,
            description,
            created_at,
            updated_at
        FROM payments
        """;
}
