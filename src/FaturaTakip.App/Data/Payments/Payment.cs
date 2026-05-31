namespace FaturaTakip.App.Data.Payments;

public sealed class Payment
{
    public long Id { get; init; }

    public long InvoiceId { get; init; }

    public DateTime PaymentDate { get; init; }

    public decimal Amount { get; init; }

    public string Description { get; init; } = string.Empty;

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }

    public string AmountText => Amount.ToString("N2");

    public string PaymentDateText => PaymentDate.ToString("dd.MM.yyyy");
}
