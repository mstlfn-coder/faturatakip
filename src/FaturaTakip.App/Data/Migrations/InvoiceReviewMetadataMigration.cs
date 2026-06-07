using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class InvoiceReviewMetadataMigration : IDatabaseMigration
{
    public string Id => "0009";

    public string Name => "Add invoice review metadata";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        AddColumnIfMissing(connection, transaction, "review_note", "TEXT NOT NULL DEFAULT ''");
        AddColumnIfMissing(connection, transaction, "reviewed_at", "TEXT NOT NULL DEFAULT ''");

        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE INDEX IF NOT EXISTS ix_invoices_reviewed_at
                ON invoices (reviewed_at DESC);
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
        command.CommandText = $"ALTER TABLE invoices ADD COLUMN {columnName} {columnDefinition};";
        command.ExecuteNonQuery();
    }

    private static bool ColumnExists(SqliteConnection connection, SqliteTransaction transaction, string columnName)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "PRAGMA table_info(invoices);";

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
