namespace FaturaTakip.App.Data.Reports;

public sealed record SubscriptionYearlyMonthRow(
    int Month,
    int InvoiceCount,
    decimal TotalAmount,
    decimal PaidTotal,
    decimal RemainingTotal,
    int UnpaidInvoiceCount,
    int OverdueInvoiceCount,
    int MissingPdfCount);

