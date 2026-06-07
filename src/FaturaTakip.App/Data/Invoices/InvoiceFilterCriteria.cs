namespace FaturaTakip.App.Data.Invoices;

public sealed record InvoiceFilterCriteria(
    int? Year = null,
    int? Month = null,
    long? InvoiceTypeId = null,
    long? SubscriptionId = null,
    InvoicePaymentStatusFilter PaymentStatus = InvoicePaymentStatusFilter.All,
    InvoicePdfStatusFilter PdfStatus = InvoicePdfStatusFilter.All,
    InvoiceReviewStatusFilter ReviewStatus = InvoiceReviewStatusFilter.All,
    string SearchText = "");

public enum InvoicePaymentStatusFilter
{
    All,
    Unpaid,
    Paid,
    Canceled,
    Overdue,
}

public enum InvoicePdfStatusFilter
{
    All,
    HasPdf,
    MissingPdf,
}

public enum InvoiceReviewStatusFilter
{
    All,
    Reviewed,
    Unreviewed,
}
