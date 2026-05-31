using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Data.Reports;

public sealed record DocumentHealthIssue(
    string IssueType,
    string EntityType,
    long EntityId,
    string PeriodOrDate,
    string InvoiceTypeName,
    string SubscriptionName,
    string InstitutionName,
    decimal Amount,
    string PdfState,
    string PdfFilePath,
    string PdfSha256Hash,
    string Note)
{
    public static DocumentHealthIssue FromInvoice(string issueType, Invoice invoice, string pdfState, string note)
    {
        return new DocumentHealthIssue(
            issueType,
            EntityType: "Fatura",
            invoice.Id,
            invoice.Period,
            invoice.InvoiceTypeName,
            invoice.SubscriptionName,
            invoice.InstitutionName,
            invoice.Amount,
            pdfState,
            invoice.PdfFilePath,
            invoice.PdfSha256Hash,
            note);
    }

    public static DocumentHealthIssue FromPayment(string issueType, Payment payment, Invoice? invoice, string pdfState, string note)
    {
        return new DocumentHealthIssue(
            issueType,
            EntityType: "Ödeme",
            payment.Id,
            invoice is null ? payment.PaymentDate.ToString("yyyy-MM-dd") : $"{invoice.Period} / {payment.PaymentDate:dd.MM.yyyy}",
            invoice?.InvoiceTypeName ?? string.Empty,
            invoice?.SubscriptionName ?? string.Empty,
            invoice?.InstitutionName ?? string.Empty,
            payment.Amount,
            pdfState,
            payment.PdfFilePath,
            payment.PdfSha256Hash,
            note);
    }
}
