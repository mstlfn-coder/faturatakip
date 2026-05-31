namespace FaturaTakip.App.Data.Payments;

public sealed class Payment
{
    public long Id { get; init; }

    public long InvoiceId { get; init; }

    public DateTime PaymentDate { get; init; }

    public decimal Amount { get; init; }

    public string Description { get; init; } = string.Empty;

    public string PdfFilePath { get; init; } = string.Empty;

    public string PdfOriginalFileName { get; init; } = string.Empty;

    public string PdfSha256Hash { get; init; } = string.Empty;

    public DateTimeOffset? PdfAttachedAt { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }

    public string AmountText => Amount.ToString("N2");

    public string PaymentDateText => PaymentDate.ToString("dd.MM.yyyy");

    public bool HasPdf => !string.IsNullOrWhiteSpace(PdfFilePath);

    public string PdfState => HasPdf ? "PDF Var" : "PDF Yok";
}
