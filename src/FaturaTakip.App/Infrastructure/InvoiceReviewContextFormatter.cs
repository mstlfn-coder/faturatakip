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
            .DistinctBy(chip => (chip.Kind, chip.Text), ContextChipComparer.Instance)
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

    private sealed class ContextChipComparer : IEqualityComparer<(string Kind, string Text)>
    {
        public static ContextChipComparer Instance { get; } = new();

        public bool Equals((string Kind, string Text) x, (string Kind, string Text) y)
        {
            return string.Equals(x.Kind, y.Kind, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(x.Text, y.Text, StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode((string Kind, string Text) obj)
        {
            return HashCode.Combine(
                StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Kind),
                StringComparer.CurrentCultureIgnoreCase.GetHashCode(obj.Text));
        }
    }

    public sealed record ContextChip(string Text, string Kind, string Prefix);
}
