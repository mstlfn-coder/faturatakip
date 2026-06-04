using System.IO;
using System.Text.Json;

namespace FaturaTakip.App.Infrastructure;

public sealed record AuditLogFilterPreferences(
    string? ActionType,
    string? EntityName,
    string? UserName,
    string? SearchText,
    DateTime? StartDate,
    DateTime? EndDate,
    bool ChangedOnly)
{
    public static AuditLogFilterPreferences Default { get; } = new(
        ActionType: null,
        EntityName: null,
        UserName: null,
        SearchText: null,
        StartDate: null,
        EndDate: null,
        ChangedOnly: false);

    public static AuditLogFilterPreferences LoadOrDefault(string rootDirectory)
    {
        try
        {
            var path = GetPath(rootDirectory);
            if (!File.Exists(path))
            {
                return Default;
            }

            var json = File.ReadAllText(path);
            var cfg = JsonSerializer.Deserialize<AuditLogFilterPreferences>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return cfg ?? Default;
        }
        catch
        {
            return Default;
        }
    }

    public void Save(string rootDirectory)
    {
        var path = GetPath(rootDirectory);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
    }

    private static string GetPath(string rootDirectory)
    {
        return Path.Combine(rootDirectory, "config", "audit-log-filter-preferences.json");
    }
}
