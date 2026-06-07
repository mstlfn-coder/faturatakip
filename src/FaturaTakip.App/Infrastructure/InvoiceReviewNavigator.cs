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

    public static string BuildHint(string? reviewModeLabel, int? currentIndex, int itemCount)
    {
        var prefix = string.IsNullOrWhiteSpace(reviewModeLabel)
            ? "Kontrol sirasi"
            : $"Kontrol modu: {reviewModeLabel}";

        if (itemCount <= 0)
        {
            return $"{prefix} - gorunur liste bos.";
        }

        if (currentIndex is null || currentIndex < 0 || currentIndex >= itemCount)
        {
            return $"{prefix} - once bir kayit secin.";
        }

        return $"{prefix} ({currentIndex.Value + 1}/{itemCount})";
    }
}
