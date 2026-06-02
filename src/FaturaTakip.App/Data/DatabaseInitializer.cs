using System.IO;
using FaturaTakip.App.Data.Migrations;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data;

public sealed class DatabaseInitializer
{
    private readonly MigrationRunner _migrationRunner = new();

    public DatabaseInitializationResult Initialize(string databasePath)
    {
        var databaseDirectory = Path.GetDirectoryName(databasePath);
        if (!string.IsNullOrWhiteSpace(databaseDirectory))
        {
            Directory.CreateDirectory(databaseDirectory);
        }

        using var connection = SqliteConnectionFactory.Create(databasePath);
        connection.Open();

        EnablePragmas(connection);

        var appliedMigrations = _migrationRunner.ApplyPendingMigrations(
            connection,
            new IDatabaseMigration[]
            {
                new InitialSchemaMigration(),
                new InvoiceTypesSchemaMigration(),
                new SubscriptionsSchemaMigration(),
                new InvoicesSchemaMigration(),
                new InvoicePdfAttachmentMigration(),
                new PaymentsSchemaMigration(),
                new PaymentPdfAttachmentMigration(),
                new AuditLogsSchemaMigration(),
            });

        return new DatabaseInitializationResult(appliedMigrations);
    }

    private static void EnablePragmas(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            PRAGMA foreign_keys = ON;
            PRAGMA journal_mode = WAL;
            """;
        command.ExecuteNonQuery();
    }
}
