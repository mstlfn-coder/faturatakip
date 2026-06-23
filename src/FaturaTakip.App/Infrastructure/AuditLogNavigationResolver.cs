namespace FaturaTakip.App.Infrastructure;

public static class AuditLogNavigationResolver
{
    public enum NavigationTarget
    {
        Invoice,
        InvoicePayment,
        Subscription,
    }

    public sealed record NavigationRequest(NavigationTarget Target, long RecordId, string Label);

    public sealed record Resolution(NavigationRequest? Request, string Message)
    {
        public bool IsSuccess => Request is not null;
    }

    public static Resolution Resolve(
        string? tableName,
        long recordId,
        Func<long, bool> invoiceExists,
        Func<long, long?> resolvePaymentInvoiceId,
        Func<long, bool> subscriptionExists)
    {
        if (recordId <= 0)
        {
            return new Resolution(null, "Audit kaydinda gecerli bir hedef kimligi yok.");
        }

        switch (tableName?.Trim().ToLowerInvariant())
        {
            case "invoices":
                return invoiceExists(recordId)
                    ? new Resolution(
                        new NavigationRequest(NavigationTarget.Invoice, recordId, $"Fatura #{recordId}"),
                        "Fatura kaydi aciliyor.")
                    : new Resolution(null, $"Fatura kaydi artik bulunamiyor: #{recordId}");

            case "payments":
                var invoiceId = resolvePaymentInvoiceId(recordId);
                return invoiceId is > 0
                    ? new Resolution(
                        new NavigationRequest(NavigationTarget.InvoicePayment, invoiceId.Value, $"Odeme #{recordId}"),
                        "Odemenin bagli oldugu fatura aciliyor.")
                    : new Resolution(null, $"Odeme kaydi artik bulunamiyor: #{recordId}");

            case "subscriptions":
                return subscriptionExists(recordId)
                    ? new Resolution(
                        new NavigationRequest(NavigationTarget.Subscription, recordId, $"Abonelik #{recordId}"),
                        "Abonelik kaydi aciliyor.")
                    : new Resolution(null, $"Abonelik kaydi artik bulunamiyor: #{recordId}");

            default:
                return new Resolution(
                    null,
                    $"Bu audit varligi icin ekran gecisi desteklenmiyor: {tableName ?? "-"}");
        }
    }
}
