using System.Globalization;

namespace FaturaTakip.App.Data.AuditLogs;

public sealed class AuditLog
{
    public long Id { get; init; }

    public string ActionType { get; init; } = string.Empty;

    public string TableName { get; init; } = string.Empty;

    public long RecordId { get; init; }

    public string OldValue { get; init; } = string.Empty;

    public string NewValue { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public DateTimeOffset CreatedAt { get; init; }

    public string UserName { get; init; } = string.Empty;

    public static DateTimeOffset ParseDate(string value)
    {
        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed))
        {
            return parsed;
        }

        return DateTimeOffset.MinValue;
    }
}
