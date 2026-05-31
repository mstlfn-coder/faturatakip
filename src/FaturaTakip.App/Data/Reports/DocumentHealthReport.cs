namespace FaturaTakip.App.Data.Reports;

public sealed record DocumentHealthReport(
    IReadOnlyList<DocumentHealthIssue> Issues,
    int InvoiceNoPdfCount,
    int InvoiceMissingFileCount,
    int PaymentNoPdfCount,
    int PaymentMissingFileCount,
    int DuplicateInvoiceHashItemCount,
    int DuplicatePaymentHashItemCount)
{
    public static readonly DocumentHealthReport Empty = new(
        Array.Empty<DocumentHealthIssue>(),
        0,
        0,
        0,
        0,
        0,
        0);
}

