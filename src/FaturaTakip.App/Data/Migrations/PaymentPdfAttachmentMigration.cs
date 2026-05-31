using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class PaymentPdfAttachmentMigration : IDatabaseMigration
{
    public string Id => "0007";

    public string Name => "Add payment PDF attachment metadata";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        AddColumnIfMissing(connection, transaction, "pdf_file_path", "TEXT NOT NULL DEFAULT ''");
        AddColumnIfMissing(connection, transaction, "pdf_original_file_name", "TEXT NOT NULL DEFAULT ''");
        AddColumnIfMissing(connection, transaction, "pdf_sha256_hash", "TEXT NOT NULL DEFAULT ''");
        AddColumnIfMissing(connection, transaction, "pdf_attached_at", "TEXT NOT NULL DEFAULT ''");

        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE INDEX IF NOT EXISTS ix_payments_pdf_sha256_hash
                ON payments (pdf_sha256_hash);
            """;
        command.ExecuteNonQuery();
    }

    private static void AddColumnIfMissing(
        SqliteConnection connection,
        SqliteTransaction transaction,
        string columnName,
        string columnDefinition)
    {
        if (ColumnExists(connection, transaction, columnName))
        {
            return;
        }

        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = $"ALTER TABLE payments ADD COLUMN {columnName} {columnDefinition};";
        command.ExecuteNonQuery();
    }

    private static bool ColumnExists(SqliteConnection connection, SqliteTransaction transaction, string columnName)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "PRAGMA table_info(payments);";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            if (string.Equals(reader.GetString(1), columnName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
