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
                    chips.Add(new ContextChip(part, ResolveKind(sectionIndex, partIndex, part)));
                }
            }
        }

        return chips;
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

    public sealed record ContextChip(string Text, string Kind);
}
