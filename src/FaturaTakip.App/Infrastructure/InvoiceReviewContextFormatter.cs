namespace FaturaTakip.App.Infrastructure;

public static class InvoiceReviewContextFormatter
{
    public enum SuggestedFilter
    {
        Unreviewed,
        Overdue,
        MissingPdf,
    }

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

    public static bool TryResolveSuggestedFilter(string? contextLabel, out SuggestedFilter filter)
    {
        filter = default;
        if (string.IsNullOrWhiteSpace(contextLabel))
        {
            return false;
        }

        if (ContainsAny(contextLabel, "İncelenmedi", "Incelenmedi"))
        {
            filter = SuggestedFilter.Unreviewed;
            return true;
        }

        if (ContainsAny(contextLabel, "Gecikmiş", "Gecikmis"))
        {
            filter = SuggestedFilter.Overdue;
            return true;
        }

        if (ContainsAny(contextLabel, "Evrak Kontrol", "PDF Kayıp", "PDF Kayip", "PDF Eksik", "PDF Yok"))
        {
            filter = SuggestedFilter.MissingPdf;
            return true;
        }

        return false;
    }

    public static bool TryResolvePeriod(string? contextLabel, out int year, out int month)
    {
        year = 0;
        month = 0;
        if (string.IsNullOrWhiteSpace(contextLabel))
        {
            return false;
        }

        foreach (var chip in BuildChips(contextLabel))
        {
            if (!string.Equals(chip.Kind, "period", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var parts = chip.Text.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                continue;
            }

            if (int.TryParse(parts[0], out year) &&
                int.TryParse(parts[1], out month) &&
                month >= 1 &&
                month <= 12)
            {
                return true;
            }
        }

        year = 0;
        month = 0;
        return false;
    }

    public static bool TryResolveInvoiceTypeName(string? contextLabel, out string invoiceTypeName)
    {
        invoiceTypeName = string.Empty;
        if (string.IsNullOrWhiteSpace(contextLabel))
        {
            return false;
        }

        var chips = BuildChips(contextLabel);
        if (chips.Count < 2)
        {
            return false;
        }

        var reportChip = chips.FirstOrDefault(chip => string.Equals(chip.Kind, "report", StringComparison.OrdinalIgnoreCase));
        if (reportChip is null ||
            !ContainsAny(reportChip.Text, "İncelenmedi", "Incelenmedi", "Gecikmiş", "Gecikmis"))
        {
            return false;
        }

        var detailChip = chips.FirstOrDefault(chip => string.Equals(chip.Kind, "issue", StringComparison.OrdinalIgnoreCase));
        if (detailChip is null ||
            string.IsNullOrWhiteSpace(detailChip.Text) ||
            LooksLikePeriod(detailChip.Text))
        {
            return false;
        }

        invoiceTypeName = detailChip.Text.Trim();
        return invoiceTypeName.Length > 0;
    }

    private static bool ContainsAny(string text, params string[] values)
    {
        return values.Any(value => text.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0);
    }

    public sealed record ContextChip(string Text, string Kind, string Prefix);
}
