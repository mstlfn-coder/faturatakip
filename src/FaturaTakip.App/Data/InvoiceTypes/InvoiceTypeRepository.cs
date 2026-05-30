using System.Globalization;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.InvoiceTypes;

public sealed class InvoiceTypeRepository
{
    private readonly string _databasePath;

    public InvoiceTypeRepository(string databasePath)
    {
        _databasePath = databasePath;
    }

    public IReadOnlyList<InvoiceType> GetAll()
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, name, description, default_usage_unit, is_active, created_at, updated_at
            FROM invoice_types
            ORDER BY is_active DESC, name COLLATE NOCASE;
            """;

        using var reader = command.ExecuteReader();
        var invoiceTypes = new List<InvoiceType>();
        while (reader.Read())
        {
            invoiceTypes.Add(ReadInvoiceType(reader));
        }

        return invoiceTypes;
    }

    public InvoiceType Add(InvoiceTypeInput input)
    {
        var normalized = Normalize(input);
        EnsureUniqueName(normalized.Name, ignoredId: null);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        var now = DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture);
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO invoice_types (name, description, default_usage_unit, is_active, created_at, updated_at)
            VALUES ($name, $description, $defaultUsageUnit, $isActive, $createdAt, $updatedAt)
            RETURNING id, name, description, default_usage_unit, is_active, created_at, updated_at;
            """;
        command.Parameters.AddWithValue("$name", normalized.Name);
        command.Parameters.AddWithValue("$description", normalized.Description);
        command.Parameters.AddWithValue("$defaultUsageUnit", normalized.DefaultUsageUnit);
        command.Parameters.AddWithValue("$isActive", normalized.IsActive ? 1 : 0);
        command.Parameters.AddWithValue("$createdAt", now);
        command.Parameters.AddWithValue("$updatedAt", now);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Fatura türü eklenemedi.");
        }

        return ReadInvoiceType(reader);
    }

    public InvoiceType Update(long id, InvoiceTypeInput input)
    {
        var normalized = Normalize(input);
        EnsureUniqueName(normalized.Name, id);

        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE invoice_types
            SET name = $name,
                description = $description,
                default_usage_unit = $defaultUsageUnit,
                is_active = $isActive,
                updated_at = $updatedAt
            WHERE id = $id
            RETURNING id, name, description, default_usage_unit, is_active, created_at, updated_at;
            """;
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$name", normalized.Name);
        command.Parameters.AddWithValue("$description", normalized.Description);
        command.Parameters.AddWithValue("$defaultUsageUnit", normalized.DefaultUsageUnit);
        command.Parameters.AddWithValue("$isActive", normalized.IsActive ? 1 : 0);
        command.Parameters.AddWithValue("$updatedAt", DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture));

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Güncellenecek fatura türü bulunamadı.");
        }

        return ReadInvoiceType(reader);
    }

    public void SetActive(long id, bool isActive)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE invoice_types
            SET is_active = $isActive,
                updated_at = $updatedAt
            WHERE id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$isActive", isActive ? 1 : 0);
        command.Parameters.AddWithValue("$updatedAt", DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture));

        if (command.ExecuteNonQuery() == 0)
        {
            throw new InvalidOperationException("Güncellenecek fatura türü bulunamadı.");
        }
    }

    private void EnsureUniqueName(string name, long? ignoredId)
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = ignoredId is null
            ? "SELECT COUNT(1) FROM invoice_types WHERE name = $name COLLATE NOCASE;"
            : "SELECT COUNT(1) FROM invoice_types WHERE name = $name COLLATE NOCASE AND id <> $id;";
        command.Parameters.AddWithValue("$name", name);
        if (ignoredId is not null)
        {
            command.Parameters.AddWithValue("$id", ignoredId.Value);
        }

        if (Convert.ToInt32(command.ExecuteScalar(), CultureInfo.InvariantCulture) > 0)
        {
            throw new InvalidOperationException("Bu isimde bir fatura türü zaten var.");
        }
    }

    private static InvoiceTypeInput Normalize(InvoiceTypeInput input)
    {
        var name = input.Name.Trim();
        var description = input.Description.Trim();
        var defaultUsageUnit = input.DefaultUsageUnit.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Fatura türü adı zorunludur.");
        }

        if (name.Length > 100)
        {
            throw new InvalidOperationException("Fatura türü adı 100 karakterden uzun olamaz.");
        }

        if (defaultUsageUnit.Length > 30)
        {
            throw new InvalidOperationException("Kullanım birimi 30 karakterden uzun olamaz.");
        }

        if (description.Length > 500)
        {
            throw new InvalidOperationException("Açıklama 500 karakterden uzun olamaz.");
        }

        return new InvoiceTypeInput(name, description, defaultUsageUnit, input.IsActive);
    }

    private static InvoiceType ReadInvoiceType(SqliteDataReader reader)
    {
        return new InvoiceType
        {
            Id = reader.GetInt64(0),
            Name = reader.GetString(1),
            Description = reader.GetString(2),
            DefaultUsageUnit = reader.GetString(3),
            IsActive = reader.GetInt32(4) == 1,
            CreatedAt = ParseDate(reader.GetString(5)),
            UpdatedAt = ParseDate(reader.GetString(6)),
        };
    }

    private static DateTimeOffset ParseDate(string value)
    {
        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed))
        {
            return parsed;
        }

        return DateTimeOffset.MinValue;
    }
}
