using System.IO;
using System.Text.Json;

namespace FaturaTakip.App.Infrastructure;

public sealed record ReportMetaConfig(
    string AppTitle,
    string InstitutionName)
{
    public static ReportMetaConfig Default { get; } = new(
        AppTitle: "KURUM FATURA TAKIP PROGRAMI",
        InstitutionName: string.Empty);

    public static ReportMetaConfig LoadOrDefault(string rootDirectory)
    {
        try
        {
            var path = Path.Combine(rootDirectory, "config", "report-meta.json");
            if (!File.Exists(path))
            {
                return Default;
            }

            var json = File.ReadAllText(path);
            var cfg = JsonSerializer.Deserialize<ReportMetaConfig>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (cfg is null)
            {
                return Default;
            }

            return cfg with
            {
                AppTitle = string.IsNullOrWhiteSpace(cfg.AppTitle) ? Default.AppTitle : cfg.AppTitle,
                InstitutionName = cfg.InstitutionName ?? string.Empty,
            };
        }
        catch
        {
            return Default;
        }
    }
}
