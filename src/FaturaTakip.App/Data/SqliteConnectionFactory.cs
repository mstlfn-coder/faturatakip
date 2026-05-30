using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data;

public static class SqliteConnectionFactory
{
    public static SqliteConnection Create(string databasePath)
    {
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = databasePath,
            Pooling = false,
        }.ToString();

        return new SqliteConnection(connectionString);
    }
}
