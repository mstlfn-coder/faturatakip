namespace FaturaTakip.App.Infrastructure;

public static class InvoiceReviewNavigator
{
    public static bool TryMove(int currentIndex, int itemCount, int offset, out int targetIndex)
    {
        targetIndex = currentIndex;

        if (itemCount <= 0 || currentIndex < 0 || currentIndex >= itemCount || offset == 0)
        {
            return false;
        }

        var candidate = currentIndex + offset;
        if (candidate < 0 || candidate >= itemCount)
        {
            return false;
        }

        targetIndex = candidate;
        return true;
    }

    public static string BuildHint(string? reviewModeLabel, int? currentIndex, int itemCount, bool includeShortcuts = false)
    {
        var prefix = string.IsNullOrWhiteSpace(reviewModeLabel)
            ? "Kontrol sirasi"
            : $"Kontrol modu: {reviewModeLabel}";

        if (itemCount <= 0)
        {
            return AppendShortcuts($"{prefix} - gorunur liste bos.", includeShortcuts);
        }

        if (currentIndex is null || currentIndex < 0 || currentIndex >= itemCount)
        {
            return AppendShortcuts($"{prefix} - once bir kayit secin.", includeShortcuts);
        }

        return AppendShortcuts($"{prefix} ({currentIndex.Value + 1}/{itemCount})", includeShortcuts);
    }

    private static string AppendShortcuts(string baseText, bool includeShortcuts)
    {
        if (!includeShortcuts)
        {
            return baseText;
        }

        return $"{baseText} | Kisayollar: Ctrl+Shift+Sol/Sag, Ctrl+Shift+O, Ctrl+Shift+K";
    }
}
