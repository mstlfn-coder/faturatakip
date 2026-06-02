using System.Globalization;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace FaturaTakip.App.Data.AuditLogs;

public sealed class AuditLogRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = false,
    };

    private readonly string _databasePath;

    public AuditLogRepository(string databasePath)
    {
        _databasePath = databasePath;
    }

    public IReadOnlyList<AuditLog> GetAll()
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, action_type, table_name, record_id, old_value, new_value, description, created_at, user_name
            FROM audit_logs
            ORDER BY id ASC;
            """;

        using var reader = command.ExecuteReader();
        var logs = new List<AuditLog>();
        while (reader.Read())
        {
            logs.Add(ReadAuditLog(reader));
        }

        return logs;
    }

    public void Add(
        string actionType,
        string tableName,
        long recordId,
        object? oldValue,
        object? newValue,
        string description,
        string userName = "system")
    {
        using var connection = SqliteConnectionFactory.Create(_databasePath);
        connection.Open();
        Add(connection, transaction: null, actionType, tableName, recordId, oldValue, newValue, description, userName);
    }

    public void Add(
        SqliteConnection connection,
        SqliteTransaction? transaction,
        string actionType,
        string tableName,
        long recordId,
        object? oldValue,
        object? newValue,
        string description,
        string userName = "system")
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            INSERT INTO audit_logs (
                action_type,
                table_name,
                record_id,
                old_value,
                new_value,
                description,
                created_at,
                user_name)
            VALUES (
                $actionType,
                $tableName,
                $recordId,
                $oldValue,
                $newValue,
                $description,
                $createdAt,
                $userName);
            """;
        command.Parameters.AddWithValue("$actionType", actionType);
        command.Parameters.AddWithValue("$tableName", tableName);
        command.Parameters.AddWithValue("$recordId", recordId);
        command.Parameters.AddWithValue("$oldValue", SerializeValue(oldValue));
        command.Parameters.AddWithValue("$newValue", SerializeValue(newValue));
        command.Parameters.AddWithValue("$description", description.Trim());
        command.Parameters.AddWithValue("$createdAt", DateTimeOffset.Now.ToString("O", CultureInfo.InvariantCulture));
        command.Parameters.AddWithValue("$userName", string.IsNullOrWhiteSpace(userName) ? "system" : userName.Trim());
        command.ExecuteNonQuery();
    }

    private static string SerializeValue(object? value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        return JsonSerializer.Serialize(value, JsonOptions);
    }

    private static AuditLog ReadAuditLog(SqliteDataReader reader)
    {
        return new AuditLog
        {
            Id = reader.GetInt64(0),
            ActionType = reader.GetString(1),
            TableName = reader.GetString(2),
            RecordId = reader.GetInt64(3),
            OldValue = reader.GetString(4),
            NewValue = reader.GetString(5),
            Description = reader.GetString(6),
            CreatedAt = AuditLog.ParseDate(reader.GetString(7)),
            UserName = reader.GetString(8),
        };
    }
}
