using System.IO;
using System.Text.Json;

namespace FaturaTakip.App.Infrastructure;

public sealed record InvoiceReviewPreferences
{
    public bool ShowContext { get; init; } = true;

    public bool ShowContextDetails { get; init; } = true;

    public int PaymentShortcutReplaySeconds { get; init; } = 2;

    public string PaymentShortcutReplayEmphasis { get; init; } = "medium";

    public static InvoiceReviewPreferences Default { get; } = new();

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

            return cfg?.Sanitize() ?? Default;
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
        File.WriteAllText(path, JsonSerializer.Serialize(Sanitize(), new JsonSerializerOptions
        {
            WriteIndented = true
        }));
    }

    public InvoiceReviewPreferences Sanitize()
    {
        var seconds = PaymentShortcutReplaySeconds switch
        {
            < 1 => 1,
            > 4 => 4,
            _ => PaymentShortcutReplaySeconds
        };

        var emphasis = PaymentShortcutReplayEmphasis?.Trim().ToLowerInvariant() switch
        {
            "low" => "low",
            "high" => "high",
            _ => "medium"
        };

        return this with
        {
            PaymentShortcutReplaySeconds = seconds,
            PaymentShortcutReplayEmphasis = emphasis
        };
    }

    private static string GetPath(string rootDirectory)
    {
        return Path.Combine(rootDirectory, "config", "invoice-review-preferences.json");
    }
}
