using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class SubscriptionsSchemaMigration : IDatabaseMigration
{
    public string Id => "0003";

    public string Name => "Create subscriptions";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS subscriptions (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                invoice_type_id INTEGER NOT NULL,
                subscription_name TEXT NOT NULL,
                institution_name TEXT NOT NULL,
                subscriber_no TEXT NOT NULL DEFAULT '',
                installation_no TEXT NOT NULL DEFAULT '',
                meter_no TEXT NOT NULL DEFAULT '',
                provider_company TEXT NOT NULL DEFAULT '',
                service_address TEXT NOT NULL DEFAULT '',
                unit_name TEXT NOT NULL DEFAULT '',
                default_usage_unit TEXT NOT NULL DEFAULT '',
                is_active INTEGER NOT NULL DEFAULT 1,
                start_date TEXT NULL,
                end_date TEXT NULL,
                description TEXT NOT NULL DEFAULT '',
                created_at TEXT NOT NULL,
                updated_at TEXT NOT NULL,
                FOREIGN KEY (invoice_type_id) REFERENCES invoice_types(id) ON DELETE RESTRICT
            );

            CREATE INDEX IF NOT EXISTS ix_subscriptions_invoice_type_id
                ON subscriptions (invoice_type_id);

            CREATE INDEX IF NOT EXISTS ix_subscriptions_is_active
                ON subscriptions (is_active);

            CREATE INDEX IF NOT EXISTS ix_subscriptions_subscription_name
                ON subscriptions (subscription_name COLLATE NOCASE);
            """;
        command.ExecuteNonQuery();
    }
}
