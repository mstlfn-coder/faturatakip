namespace FaturaTakip.App.Data.Reports;

public sealed record ActionableInvoiceReport(
    IReadOnlyList<ActionableInvoice> Unpaid,
    decimal UnpaidRemainingTotal,
    IReadOnlyList<ActionableInvoice> Overdue,
    decimal OverdueRemainingTotal,
    IReadOnlyList<ActionableInvoice> Unreviewed,
    decimal UnreviewedRemainingTotal,
    IReadOnlyList<ActionableInvoice> Upcoming,
    decimal UpcomingRemainingTotal)
{
    public static readonly ActionableInvoiceReport Empty = new(
        Array.Empty<ActionableInvoice>(),
        0m,
        Array.Empty<ActionableInvoice>(),
        0m,
        Array.Empty<ActionableInvoice>(),
        0m,
        Array.Empty<ActionableInvoice>(),
        0m);
}

