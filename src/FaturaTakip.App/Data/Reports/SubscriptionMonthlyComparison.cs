namespace FaturaTakip.App.Data.Reports;

public sealed record SubscriptionMonthlyComparison(
    long SubscriptionId,
    MonthlyInvoiceReport Current,
    MonthlyInvoiceReport Previous,
    int InvoiceCountDelta,
    decimal TotalAmountDelta,
    decimal PaidDelta,
    decimal RemainingDelta)
{
    public static readonly SubscriptionMonthlyComparison Empty = new(
        0,
        MonthlyInvoiceReport.Empty,
        MonthlyInvoiceReport.Empty,
        0,
        0m,
        0m,
        0m);
}

