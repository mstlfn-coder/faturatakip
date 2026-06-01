namespace FaturaTakip.App.Data.Reports;

public sealed record YearlyInvoiceListReport(
    int Year,
    IReadOnlyList<MonthlyInvoiceRow> Rows,
    int TotalInvoiceCount,
    decimal TotalAmount,
    decimal PaidTotal,
    decimal RemainingTotal,
    int UnpaidInvoiceCount,
    int OverdueInvoiceCount,
    int MissingPdfCount)
{
    public static readonly YearlyInvoiceListReport Empty = new(
        0,
        Array.Empty<MonthlyInvoiceRow>(),
        0,
        0m,
        0m,
        0m,
        0,
        0,
        0);
}
