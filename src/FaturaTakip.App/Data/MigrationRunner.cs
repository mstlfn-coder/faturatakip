using FaturaTakip.App.Data.Migrations;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data;

public sealed class MigrationRunner
{
    public IReadOnlyList<string> ApplyPendingMigrations(
        SqliteConnection connection,
        IEnumerable<IDatabaseMigration> migrations)
    {
        EnsureMigrationTable(connection);

        var applied = new List<string>();
        foreach (var migration in migrations.OrderBy(migration => migration.Id, StringComparer.Ordinal))
        {
            if (IsApplied(connection, migration.Id))
            {
                continue;
            }

            using var transaction = connection.BeginTransaction();
            migration.Up(connection, transaction);
            RecordMigration(connection, transaction, migration);
            transaction.Commit();

            applied.Add($"{migration.Id} - {migration.Name}");
        }

        return applied;
    }

    private static void EnsureMigrationTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS schema_migrations (
                id TEXT PRIMARY KEY,
                name TEXT NOT NULL,
                applied_at TEXT NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }

    private static bool IsApplied(SqliteConnection connection, string migrationId)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM schema_migrations WHERE id = $id;";
        command.Parameters.AddWithValue("$id", migrationId);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    private static void RecordMigration(
        SqliteConnection connection,
        SqliteTransaction transaction,
        IDatabaseMigration migration)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            INSERT INTO schema_migrations (id, name, applied_at)
            VALUES ($id, $name, $appliedAt);
            """;
        command.Parameters.AddWithValue("$id", migration.Id);
        command.Parameters.AddWithValue("$name", migration.Name);
        command.Parameters.AddWithValue("$appliedAt", DateTimeOffset.Now.ToString("O"));
        command.ExecuteNonQuery();
    }
}
