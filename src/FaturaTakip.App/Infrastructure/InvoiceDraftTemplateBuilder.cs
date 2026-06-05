using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Infrastructure;

public static class InvoiceDraftTemplateBuilder
{
    public static InvoiceDraftTemplate FromInvoice(Invoice invoice)
    {
        var nextInvoiceDate = invoice.InvoiceDate.AddMonths(1);
        var nextDueDate = invoice.DueDate.AddMonths(1);

        return new InvoiceDraftTemplate(
            invoice.SubscriptionId,
            nextInvoiceDate.Year,
            nextInvoiceDate.Month,
            nextInvoiceDate.Date,
            nextDueDate.Date,
            InvoiceNo: string.Empty,
            invoice.Amount,
            invoice.UsageAmount,
            invoice.UsageUnit,
            invoice.Description);
    }
}

public sealed record InvoiceDraftTemplate(
    long SubscriptionId,
    int InvoiceYear,
    int InvoiceMonth,
    DateTime InvoiceDate,
    DateTime DueDate,
    string InvoiceNo,
    decimal Amount,
    decimal UsageAmount,
    string UsageUnit,
    string Description);
