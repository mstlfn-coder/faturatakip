using System.Globalization;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Invoices;

public sealed class InvoiceRepository
{
    private readonly string _databasePath;

    public InvoiceRepository(string databasePath)
    {
        _databasePath = databasePath;
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
        return GetRequired(id);
    }

    public Invoice Update(long id, InvoiceInput input)
    {
        var normalized = Normalize(input);
        var subscription = GetSubscriptionSnapshot(normalized.SubscriptionId);
        EnsureUniqueInvoiceNo(normalized.SubscriptionId, normalized.InvoiceNo, id);

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

        return GetRequired(id);
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
            UsageAmount = ParseDecimal(reader.GetString(12)),
            UsageUnit = reader.GetString(13),
            Status = reader.GetString(14),
            Description = reader.GetString(15),
            CreatedAt = ParseDateTimeOffset(reader.GetString(16)),
            UpdatedAt = ParseDateTimeOffset(reader.GetString(17)),
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
            i.created_at,
            i.updated_at
        FROM invoices i
        INNER JOIN subscriptions s ON s.id = i.subscription_id
        INNER JOIN invoice_types it ON it.id = i.invoice_type_id
        """;

    private sealed record SubscriptionSnapshot(long Id, long InvoiceTypeId, string DefaultUsageUnit);
}
