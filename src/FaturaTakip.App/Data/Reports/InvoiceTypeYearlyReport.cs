namespace FaturaTakip.App.Data.Reports;

public sealed record InvoiceTypeYearlyReport(
    long InvoiceTypeId,
    string InvoiceTypeName,
    int Year,
    IReadOnlyList<SubscriptionYearlyMonthRow> Months,
    IReadOnlyList<InvoiceTypeYearlyDistributionRow> Distribution,
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
    public static readonly InvoiceTypeYearlyReport Empty = new(
        0,
        string.Empty,
        0,
        Array.Empty<SubscriptionYearlyMonthRow>(),
        Array.Empty<InvoiceTypeYearlyDistributionRow>(),
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

