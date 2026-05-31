namespace FaturaTakip.App.Data.Invoices;

public sealed record InvoiceInput(
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
