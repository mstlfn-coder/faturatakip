using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class InitialSchemaMigration : IDatabaseMigration
{
    public string Id => "0001";

    public string Name => "Create application metadata";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS app_metadata (
                key TEXT PRIMARY KEY,
                value TEXT NOT NULL,
                updated_at TEXT NOT NULL
            );

            INSERT INTO app_metadata (key, value, updated_at)
            VALUES ('database_version', '0.1', datetime('now'))
            ON CONFLICT(key) DO UPDATE SET
                value = excluded.value,
                updated_at = excluded.updated_at;
            """;
        command.ExecuteNonQuery();
    }
}
