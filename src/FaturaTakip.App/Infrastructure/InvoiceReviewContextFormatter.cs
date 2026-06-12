namespace FaturaTakip.App.Infrastructure;

public static class InvoiceReviewContextFormatter
{
    public static IReadOnlyList<ContextChip> BuildChips(string? contextLabel)
    {
        if (string.IsNullOrWhiteSpace(contextLabel))
        {
            return Array.Empty<ContextChip>();
        }

        var chips = new List<ContextChip>();
        var sections = contextLabel
            .Split('>', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        for (var sectionIndex = 0; sectionIndex < sections.Length; sectionIndex++)
        {
            var section = sections[sectionIndex];
            var parts = section
                .Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            for (var partIndex = 0; partIndex < parts.Length; partIndex++)
            {
                var part = parts[partIndex];
                if (!string.IsNullOrWhiteSpace(part))
                {
                    var kind = ResolveKind(sectionIndex, partIndex, part);
                    chips.Add(new ContextChip(part, kind, ResolvePrefix(kind)));
                }
            }
        }

        return chips
            .GroupBy(
                chip => $"{chip.Kind}\u001F{chip.Text}",
                StringComparer.CurrentCultureIgnoreCase)
            .Select(group => group.First())
            .OrderBy(chip => GetKindOrder(chip.Kind))
            .ThenBy(chip => chip.Text, StringComparer.CurrentCultureIgnoreCase)
            .ToList();
    }

    private static string ResolveKind(int sectionIndex, int partIndex, string text)
    {
        if (sectionIndex == 0)
        {
            return "report";
        }

        if (LooksLikePeriod(text))
        {
            return "period";
        }

        return partIndex switch
        {
            0 => "issue",
            1 => "entity",
            _ => "detail",
        };
    }

    private static bool LooksLikePeriod(string text)
    {
        return text.Count(ch => ch == '-') == 1 && text.Any(char.IsDigit);
    }

    private static string ResolvePrefix(string kind)
    {
        return kind switch
        {
            "report" => "RPR",
            "issue" => "ISS",
            "entity" => "VAR",
            "period" => "DNM",
            _ => "DET",
        };
    }

    private static int GetKindOrder(string kind)
    {
        return kind switch
        {
            "report" => 0,
            "issue" => 1,
            "entity" => 2,
            "detail" => 3,
            "period" => 4,
            _ => 5,
        };
    }

    public sealed record ContextChip(string Text, string Kind, string Prefix);
}
