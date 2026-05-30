using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.Migrations;

public interface IDatabaseMigration
{
    string Id { get; }

    string Name { get; }

    void Up(SqliteConnection connection, SqliteTransaction transaction);
}
