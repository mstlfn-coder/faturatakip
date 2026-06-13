namespace FaturaTakip.App.Infrastructure;

internal static class ReviewContextStatusMessageFormatter
{
    private static readonly string[] KnownLeads = ["Çip", "Klavye", "Menü"];

    public static string BuildCopySuccess(string chipText, string? lead)
    {
        return string.IsNullOrWhiteSpace(lead)
            ? $"Bağlam çipi panoya kopyalandı: {chipText}"
            : $"{lead}: {chipText} kopyalandı.";
    }

    public static string BuildCopyError(string errorMessage, string? lead)
    {
        return string.IsNullOrWhiteSpace(lead)
            ? $"Bağlam çipi panoya kopyalanamadı: {errorMessage}"
            : $"{lead}: Kopyalama başarısız - {errorMessage}";
    }

    public static string BuildActionSuccess(string actionLabel, string? detail, string? lead)
    {
        var suffix = string.IsNullOrWhiteSpace(detail)
            ? string.Empty
            : $" - {detail}";
        return string.IsNullOrWhiteSpace(lead)
            ? $"Bağlam: {actionLabel}{suffix}."
            : $"{lead}: {actionLabel}{suffix}.";
    }

    public static string BuildActionError(string errorMessage, string? lead)
    {
        return string.IsNullOrWhiteSpace(lead)
            ? errorMessage
            : $"{lead}: {errorMessage}";
    }

    public static bool TryResolveLead(string? message, out string lead)
    {
        lead = string.Empty;
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        foreach (var knownLead in KnownLeads)
        {
            if (message.StartsWith($"{knownLead}:", System.StringComparison.Ordinal))
            {
                lead = knownLead;
                return true;
            }
        }

        return false;
    }
}
