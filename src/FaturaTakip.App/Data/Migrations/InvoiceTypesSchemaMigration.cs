using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public sealed class InvoiceTypesSchemaMigration : IDatabaseMigration
{
    public string Id => "0002";

    public string Name => "Create invoice types";

    public void Up(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS invoice_types (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL COLLATE NOCASE UNIQUE,
                description TEXT NOT NULL DEFAULT '',
                default_usage_unit TEXT NOT NULL DEFAULT '',
                is_active INTEGER NOT NULL DEFAULT 1,
                created_at TEXT NOT NULL,
                updated_at TEXT NOT NULL
            );

            INSERT INTO invoice_types (name, description, default_usage_unit, is_active, created_at, updated_at)
            VALUES
                ('Elektrik', 'Elektrik tüketim faturaları', 'kWh', 1, datetime('now'), datetime('now')),
                ('Su', 'Su tüketim faturaları', 'm3', 1, datetime('now'), datetime('now')),
                ('Doğalgaz', 'Doğalgaz tüketim faturaları', 'm3', 1, datetime('now'), datetime('now')),
                ('Telefon', 'Telefon hizmet faturaları', 'Adet / Dakika', 1, datetime('now'), datetime('now')),
                ('İnternet', 'İnternet hizmet faturaları', 'GB / Sabit', 1, datetime('now'), datetime('now')),
                ('Diğer', 'Diğer kurum takip kalemleri', '', 1, datetime('now'), datetime('now'))
            ON CONFLICT(name) DO NOTHING;
            """;
        command.ExecuteNonQuery();
    }
}
