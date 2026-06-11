using System.IO;
using System.Text.Json;

namespace FaturaTakip.App.Infrastructure;

public sealed record InvoiceReviewPreferences(bool ShowContext)
{
    public static InvoiceReviewPreferences Default { get; } = new(ShowContext: true);

    public static InvoiceReviewPreferences LoadOrDefault(string rootDirectory)
    {
        try
        {
            var path = GetPath(rootDirectory);
            if (!File.Exists(path))
            {
                return Default;
            }

            var json = File.ReadAllText(path);
            var cfg = JsonSerializer.Deserialize<InvoiceReviewPreferences>(json, new JsonSerializerOptions
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
        return Path.Combine(rootDirectory, "config", "invoice-review-preferences.json");
    }
}
