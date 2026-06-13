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
                    var actionKey = ResolveActionKey(contextLabel, sectionIndex, partIndex, kind, part);
                    chips.Add(new ContextChip(
                        part,
                        kind,
                        ResolvePrefix(kind),
                        ResolveToolTip(actionKey, part),
                        actionKey,
                        ResolveActionBadge(actionKey)));
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
        var parts = text.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        return parts.Length == 2 &&
               parts[0].Length == 4 &&
               parts[1].Length == 2 &&
               parts[0].All(char.IsDigit) &&
               parts[1].All(char.IsDigit);
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

    private static string ResolveActionKey(string contextLabel, int sectionIndex, int partIndex, string kind, string text)
    {
        if (string.Equals(kind, "period", StringComparison.OrdinalIgnoreCase))
        {
            return "apply_period";
        }

        if (sectionIndex == 0 && TryResolveSuggestedFilter(contextLabel, out _))
        {
            return "apply_filter";
        }

        if (TryResolveInvoiceTypeName(contextLabel, out var invoiceTypeName) &&
            string.Equals(text, invoiceTypeName, StringComparison.CurrentCultureIgnoreCase))
        {
            return "apply_type";
        }

        if (TryResolveInvoiceNumber(contextLabel, out var invoiceNumber) &&
            string.Equals(text, invoiceNumber, StringComparison.CurrentCultureIgnoreCase))
        {
            return "apply_invoice_no";
        }

        return "copy";
    }

    private static string ResolveToolTip(string actionKey, string text)
    {
        return actionKey switch
        {
            "apply_filter" => $"Tikla veya Enter/Space ile baglam filtresini uygula: {text} | Ctrl+C: kopyala | Shift+F10: menu",
            "apply_period" => $"Tikla veya Enter/Space ile donem filtresini uygula: {text} | Ctrl+C: kopyala | Shift+F10: menu",
            "apply_type" => $"Tikla veya Enter/Space ile tur filtresini uygula: {text} | Ctrl+C: kopyala | Shift+F10: menu",
            "apply_invoice_no" => $"Tikla veya Enter/Space ile fatura no aramasini uygula: {text} | Ctrl+C: kopyala | Shift+F10: menu",
            _ => $"Tikla veya Enter/Space ile bu baglam parcasini kopyala: {text} | Ctrl+C: kopyala | Shift+F10: menu"
        };
    }

    private static string ResolveActionBadge(string actionKey)
    {
        return actionKey == "copy" ? "KPY" : "UYG";
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
        if (!TryResolveActionableReportParts(contextLabel, out _, out var invoiceTypePart, out _))
        {
            return false;
        }

        invoiceTypeName = invoiceTypePart;
        return invoiceTypeName.Length > 0;
    }

    public static bool TryResolveInvoiceNumber(string? contextLabel, out string invoiceNumber)
    {
        invoiceNumber = string.Empty;
        if (!TryResolveActionableReportParts(contextLabel, out _, out _, out var invoiceNumberPart))
        {
            return false;
        }

        invoiceNumber = invoiceNumberPart;
        return invoiceNumber.Length > 0;
    }

    private static bool TryResolveActionableReportParts(string? contextLabel, out string reportLabel, out string firstPart, out string secondPart)
    {
        reportLabel = string.Empty;
        firstPart = string.Empty;
        secondPart = string.Empty;
        if (string.IsNullOrWhiteSpace(contextLabel))
        {
            return false;
        }

        var sections = contextLabel
            .Split('>', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (sections.Length < 2)
        {
            return false;
        }

        reportLabel = sections[0].Trim();
        if (!ContainsAny(reportLabel, "İncelenmedi", "Incelenmedi", "Gecikmiş", "Gecikmis"))
        {
            return false;
        }

        var parts = sections[1]
            .Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            return false;
        }

        firstPart = parts[0].Trim();
        secondPart = parts[1].Trim();
        return firstPart.Length > 0 && secondPart.Length > 0 && !LooksLikePeriod(secondPart);
    }

    private static bool ContainsAny(string text, params string[] values)
    {
        return values.Any(value => text.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0);
    }

    public sealed record ContextChip(string Text, string Kind, string Prefix, string ToolTip, string ActionKey, string ActionBadge, bool IsSelected = false);
}
