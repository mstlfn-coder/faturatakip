namespace FaturaTakip.App.Data.Reports;

public sealed record SubscriptionYearlyReport(
    long SubscriptionId,
    int Year,
    IReadOnlyList<SubscriptionYearlyMonthRow> Months,
    int TotalInvoiceCount,
    decimal TotalAmount,
    decimal PaidTotal,
    decimal RemainingTotal,
    int MissingPdfCount,
    int HighestMonth,
    decimal HighestMonthTotal,
    int LowestMonth,
    decimal LowestMonthTotal)
{
    public static readonly SubscriptionYearlyReport Empty = new(
        0,
        0,
        Array.Empty<SubscriptionYearlyMonthRow>(),
        0,
        0m,
        0m,
        0m,
        0,
        0,
        0m,
        0,
        0m);
}

