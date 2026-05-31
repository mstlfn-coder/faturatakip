using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class InvoicesSchemaMigration : IDatabaseMigration
{
    public string Id => "0004";

    public string Name => "Create invoices";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS invoices (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                subscription_id INTEGER NOT NULL,
                invoice_type_id INTEGER NOT NULL,
                invoice_year INTEGER NOT NULL,
                invoice_month INTEGER NOT NULL,
                invoice_date TEXT NOT NULL,
                due_date TEXT NOT NULL,
                invoice_no TEXT NOT NULL,
                amount TEXT NOT NULL,
                usage_amount TEXT NOT NULL,
                usage_unit TEXT NOT NULL DEFAULT '',
                status TEXT NOT NULL DEFAULT 'unpaid',
                description TEXT NOT NULL DEFAULT '',
                created_at TEXT NOT NULL,
                updated_at TEXT NOT NULL,
                CHECK (invoice_month >= 1 AND invoice_month <= 12),
                CHECK (status IN ('unpaid', 'paid', 'canceled')),
                FOREIGN KEY (subscription_id) REFERENCES subscriptions(id) ON DELETE RESTRICT,
                FOREIGN KEY (invoice_type_id) REFERENCES invoice_types(id) ON DELETE RESTRICT
            );

            CREATE UNIQUE INDEX IF NOT EXISTS ux_invoices_subscription_invoice_no
                ON invoices (subscription_id, invoice_no COLLATE NOCASE);

            CREATE INDEX IF NOT EXISTS ix_invoices_subscription_id
                ON invoices (subscription_id);

            CREATE INDEX IF NOT EXISTS ix_invoices_invoice_type_id
                ON invoices (invoice_type_id);

            CREATE INDEX IF NOT EXISTS ix_invoices_period
                ON invoices (invoice_year, invoice_month);

            CREATE INDEX IF NOT EXISTS ix_invoices_due_date
                ON invoices (due_date);
            """;
        command.ExecuteNonQuery();
    }
}
