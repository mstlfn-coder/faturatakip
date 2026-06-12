namespace FaturaTakip.App.Infrastructure;

public static class InvoiceReviewContextFormatter
{
    public static IReadOnlyList<string> BuildChips(string? contextLabel)
    {
        if (string.IsNullOrWhiteSpace(contextLabel))
        {
            return Array.Empty<string>();
        }

        var chips = new List<string>();
        var sections = contextLabel
            .Split('>', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        foreach (var section in sections)
        {
            var parts = section
                .Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (!string.IsNullOrWhiteSpace(part))
                {
                    chips.Add(part);
                }
            }
        }

        return chips;
    }
}
