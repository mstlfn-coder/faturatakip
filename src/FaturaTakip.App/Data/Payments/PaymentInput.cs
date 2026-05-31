namespace FaturaTakip.App.Data.Payments;

public sealed record PaymentInput(
    long InvoiceId,
    DateTime PaymentDate,
    decimal Amount,
    string Description);
