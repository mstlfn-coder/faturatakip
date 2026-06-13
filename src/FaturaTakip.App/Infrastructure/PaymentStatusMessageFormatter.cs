namespace FaturaTakip.App.Infrastructure;

internal static class PaymentStatusMessageFormatter
{
    public static string BuildActionSuccess(string message, string? lead)
    {
        return string.IsNullOrWhiteSpace(lead)
            ? message
            : $"{lead}: {message}";
    }

    public static string BuildActionError(string message, string? lead)
    {
        return string.IsNullOrWhiteSpace(lead)
            ? message
            : $"{lead}: {message}";
    }
}
