using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class PaymentsSchemaMigration : IDatabaseMigration
{
    public string Id => "0006";

    public string Name => "Create invoice payments";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS payments (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                invoice_id INTEGER NOT NULL,
                payment_date TEXT NOT NULL,
                amount TEXT NOT NULL,
                description TEXT NOT NULL DEFAULT '',
                created_at TEXT NOT NULL,
                updated_at TEXT NOT NULL,
                FOREIGN KEY (invoice_id) REFERENCES invoices(id) ON DELETE RESTRICT
            );

            CREATE INDEX IF NOT EXISTS ix_payments_invoice_id
                ON payments (invoice_id);

            CREATE INDEX IF NOT EXISTS ix_payments_payment_date
                ON payments (payment_date);
            """;
        command.ExecuteNonQuery();
    }
}
