using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class AuditLogsSchemaMigration : IDatabaseMigration
{
    public string Id => "0008";

    public string Name => "Create audit logs";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS audit_logs (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                action_type TEXT NOT NULL,
                table_name TEXT NOT NULL,
                record_id INTEGER NOT NULL,
                old_value TEXT NOT NULL DEFAULT '',
                new_value TEXT NOT NULL DEFAULT '',
                description TEXT NOT NULL DEFAULT '',
                created_at TEXT NOT NULL,
                user_name TEXT NOT NULL DEFAULT 'system'
            );

            CREATE INDEX IF NOT EXISTS ix_audit_logs_table_record
                ON audit_logs (table_name, record_id);

            CREATE INDEX IF NOT EXISTS ix_audit_logs_created_at
                ON audit_logs (created_at DESC);
            """;
        command.ExecuteNonQuery();
    }
}
