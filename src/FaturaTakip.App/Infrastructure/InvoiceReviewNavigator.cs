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
}
