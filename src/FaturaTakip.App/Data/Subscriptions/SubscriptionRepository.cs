using System.Globalization;
using FaturaTakip.App.Data.AuditLogs;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Subscriptions;

public sealed class SubscriptionRepository
{
    private readonly string _databasePath;
    private readonly AuditLogRepository _auditLogRepository;

    public SubscriptionRepository(string databasePath)
    {
        _databasePath = databasePath;
        _auditLogRepository = new AuditLogRepository(databasePath);
    }

    public IReadOnlyList<Subscription> GetAll()
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT
                s.id,
                s.invoice_type_id,
                it.name AS invoice_type_name,
                s.subscription_name,
                s.institution_name,
                s.subscriber_no,
                s.installation_no,
                s.meter_no,
                s.provider_company,
                s.service_address,
                s.unit_name,
                s.default_usage_unit,
                s.is_active,
                s.start_date,
                s.end_date,
                s.description,
                s.created_at,
                s.updated_at
            FROM subscriptions s
            INNER JOIN invoice_types it ON it.id = s.invoice_type_id
            ORDER BY s.is_active DESC, it.name COLLATE NOCASE, s.subscription_name COLLATE NOCASE;
            """;

        using var reader = command.ExecuteReader();
        var subscriptions = new List<Subscription>();
        while (reader.Read())
        {
            subscriptions.Add(ReadSubscription(reader));
        }

        return subscriptions;
    }

    public Subscription Add(SubscriptionInput input)
    {
        var normalized = Normalize(input);
        EnsureInvoiceTypeExists(normalized.InvoiceTypeId);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        var now = DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture);
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO subscriptions (
                invoice_type_id,
                subscription_name,
                institution_name,
                subscriber_no,
                installation_no,
                meter_no,
                provider_company,
                service_address,
                unit_name,
                default_usage_unit,
                is_active,
                start_date,
                end_date,
                description,
                created_at,
                updated_at)
            VALUES (
                $invoiceTypeId,
                $subscriptionName,
                $institutionName,
                $subscriberNo,
                $installationNo,
                $meterNo,
                $providerCompany,
                $serviceAddress,
                $unitName,
                $defaultUsageUnit,
                $isActive,
                $startDate,
                $endDate,
                $description,
                $createdAt,
                $updatedAt)
            RETURNING id;
            """;
        BindInput(command, normalized);
        command.Parameters.AddWithValue("$createdAt", now);
        command.Parameters.AddWithValue("$updatedAt", now);

        var id = Convert.ToInt64(command.ExecuteScalar(), CultureInfo.InvariantCulture);
        var created = GetRequired(id);
        _auditLogRepository.Add("subscription_created", "subscriptions", created.Id, null, created, "Abonelik olusturuldu.");
        return created;
    }

    public Subscription Update(long id, SubscriptionInput input)
    {
        var normalized = Normalize(input);
        EnsureInvoiceTypeExists(normalized.InvoiceTypeId);
        var previous = GetRequired(id);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE subscriptions
            SET invoice_type_id = $invoiceTypeId,
                subscription_name = $subscriptionName,
                institution_name = $institutionName,
                subscriber_no = $subscriberNo,
                installation_no = $installationNo,
                meter_no = $meterNo,
                provider_company = $providerCompany,
                service_address = $serviceAddress,
                unit_name = $unitName,
                default_usage_unit = $defaultUsageUnit,
                is_active = $isActive,
                start_date = $startDate,
                end_date = $endDate,
                description = $description,
                updated_at = $updatedAt
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);
        BindInput(command, normalized);
        command.Parameters.AddWithValue("$updatedAt", DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture));

        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Guncellenecek abonelik bulunamadi.");
        }

        var updated = GetRequired(id);
        _auditLogRepository.Add("subscription_updated", "subscriptions", updated.Id, previous, updated, "Abonelik guncellendi.");
        return updated;
    }

    public void SetActive(long id, bool isActive)
    {
        var previous = GetRequired(id);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE subscriptions
            SET is_active = $isActive,
                updated_at = $updatedAt
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$isActive", isActive ? 1 : 0);
        command.Parameters.AddWithValue("$updatedAt", DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture));

        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Guncellenecek abonelik bulunamadi.");
        }

        var updated = GetRequired(id);
        _auditLogRepository.Add(
            isActive ? "subscription_activated" : "subscription_deactivated",
            "subscriptions",
            updated.Id,
            previous,
            updated,
            isActive ? "Abonelik aktif yapildi." : "Abonelik pasif yapildi.");
    }

    private Subscription GetRequired(long id)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT
                s.id,
                s.invoice_type_id,
                it.name AS invoice_type_name,
                s.subscription_name,
                s.institution_name,
                s.subscriber_no,
                s.installation_no,
                s.meter_no,
                s.provider_company,
                s.service_address,
                s.unit_name,
                s.default_usage_unit,
                s.is_active,
                s.start_date,
                s.end_date,
                s.description,
                s.created_at,
                s.updated_at
            FROM subscriptions s
            INNER JOIN invoice_types it ON it.id = s.invoice_type_id
            WHERE s.id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Abonelik bulunamadi.");
        }

        return ReadSubscription(reader);
    }

    private void EnsureInvoiceTypeExists(long invoiceTypeId)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM invoice_types WHERE id = $id;";
        command.Parameters.AddWithValue("$id", invoiceTypeId);

        if (Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture) == 0)
        {
            throw new InvalidOperationException("Gecerli bir fatura turu secin.");
        }
    }

    private static void BindInput(SqliteCommand command, SubscriptionInput input)
    {
        command.Parameters.AddWithValue("$invoiceTypeId", input.InvoiceTypeId);
        command.Parameters.AddWithValue("$subscriptionName", input.SubscriptionName);
        command.Parameters.AddWithValue("$institutionName", input.InstitutionName);
        command.Parameters.AddWithValue("$subscriberNo", input.SubscriberNo);
        command.Parameters.AddWithValue("$installationNo", input.InstallationNo);
        command.Parameters.AddWithValue("$meterNo", input.MeterNo);
        command.Parameters.AddWithValue("$providerCompany", input.ProviderCompany);
        command.Parameters.AddWithValue("$serviceAddress", input.ServiceAddress);
        command.Parameters.AddWithValue("$unitName", input.UnitName);
        command.Parameters.AddWithValue("$defaultUsageUnit", input.DefaultUsageUnit);
        command.Parameters.AddWithValue("$isActive", input.IsActive ? 1 : 0);
        command.Parameters.AddWithValue("$startDate", FormatDate(input.StartDate));
        command.Parameters.AddWithValue("$endDate", FormatDate(input.EndDate));
        command.Parameters.AddWithValue("$description", input.Description);
    }

    private static SubscriptionInput Normalize(SubscriptionInput input)
    {
        var normalized = input with
        {
            SubscriptionName = input.SubscriptionName.Trim(),
            InstitutionName = input.InstitutionName.Trim(),
            SubscriberNo = input.SubscriberNo.Trim(),
            InstallationNo = input.InstallationNo.Trim(),
            MeterNo = input.MeterNo.Trim(),
            ProviderCompany = input.ProviderCompany.Trim(),
            ServiceAddress = input.ServiceAddress.Trim(),
            UnitName = input.UnitName.Trim(),
            DefaultUsageUnit = input.DefaultUsageUnit.Trim(),
            Description = input.Description.Trim(),
        };

        if (normalized.InvoiceTypeId <= 0)
        {
            throw new InvalidOperationException("Fatura turu secimi zorunludur.");
        }

        if (string.IsNullOrWhiteSpace(normalized.SubscriptionName))
        {
            throw new InvalidOperationException("Abonelik adi zorunludur.");
        }

        if (string.IsNullOrWhiteSpace(normalized.InstitutionName))
        {
            throw new InvalidOperationException("Kurum adi zorunludur.");
        }

        if (normalized.EndDate is not null &&
            normalized.StartDate is not null &&
            normalized.EndDate.Value.Date < normalized.StartDate.Value.Date)
        {
            throw new InvalidOperationException("Bitis tarihi baslangic tarihinden once olamaz.");
        }

        EnsureMaxLength(normalized.SubscriptionName, 150, "Abonelik adi");
        EnsureMaxLength(normalized.InstitutionName, 150, "Kurum adi");
        EnsureMaxLength(normalized.SubscriberNo, 80, "Abone numarasi");
        EnsureMaxLength(normalized.InstallationNo, 80, "Tesisat numarasi");
        EnsureMaxLength(normalized.MeterNo, 80, "Sayac numarasi");
        EnsureMaxLength(normalized.ProviderCompany, 150, "Saglayici firma");
        EnsureMaxLength(normalized.UnitName, 150, "Birim / bina adi");
        EnsureMaxLength(normalized.DefaultUsageUnit, 30, "Kullanim birimi");
        EnsureMaxLength(normalized.Description, 500, "Aciklama");

        return normalized;
    }

    private static void EnsureMaxLength(string value, int maxLength, string label)
    {
        if (value.Length > maxLength)
        {
            throw new InvalidOperationException($"{label} {maxLength} karakterden uzun olamaz.");
        }
    }

    private static Subscription ReadSubscription(SqliteDataReader reader)
    {
        return new Subscription
        {
            Id = reader.GetInt64(0),
            InvoiceTypeId = reader.GetInt64(1),
            InvoiceTypeName = reader.GetString(2),
            SubscriptionName = reader.GetString(3),
            InstitutionName = reader.GetString(4),
            SubscriberNo = reader.GetString(5),
            InstallationNo = reader.GetString(6),
            MeterNo = reader.GetString(7),
            ProviderCompany = reader.GetString(8),
            ServiceAddress = reader.GetString(9),
            UnitName = reader.GetString(10),
            DefaultUsageUnit = reader.GetString(11),
            IsActive = reader.GetInt32(12) == 1,
            StartDate = ReadOptionalDate(reader, 13),
            EndDate = ReadOptionalDate(reader, 14),
            Description = reader.GetString(15),
            CreatedAt = ParseDateTimeOffset(reader.GetString(16)),
            UpdatedAt = ParseDateTimeOffset(reader.GetString(17)),
        };
    }

    private static object FormatDate(DateTime? date)
    {
        return date is null
            ? DBNull.Value
            : date.Value.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    private static DateTime? ReadOptionalDate(SqliteDataReader reader, int ordinal)
    {
        if (reader.IsDBNull(ordinal))
        {
            return null;
        }

        return DateTime.ParseExact(reader.GetString(ordinal), "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    private static DateTimeOffset ParseDateTimeOffset(string value)
    {
        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed))
        {
            return parsed;
        }

        return DateTimeOffset.MinValue;
    }
}
