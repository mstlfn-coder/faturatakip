namespace FaturaTakip.App.Data.Invoices;

public sealed class Invoice
{
    public long Id { get; init; }

    public long SubscriptionId { get; init; }

    public long InvoiceTypeId { get; init; }

    public string InvoiceTypeName { get; init; } = string.Empty;

    public string SubscriptionName { get; init; } = string.Empty;

    public string InstitutionName { get; init; } = string.Empty;

    public int InvoiceYear { get; init; }

    public int InvoiceMonth { get; init; }

    public DateTime InvoiceDate { get; init; }

    public DateTime DueDate { get; init; }

    public string InvoiceNo { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public decimal PaidAmount { get; init; }

    public decimal RemainingAmount => Math.Max(Amount - PaidAmount, 0m);

    public decimal UsageAmount { get; init; }

    public string UsageUnit { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string ReviewNote { get; init; } = string.Empty;

    public DateTimeOffset? ReviewedAt { get; init; }

    public string PdfFilePath { get; init; } = string.Empty;

    public string PdfOriginalFileName { get; init; } = string.Empty;

    public string PdfSha256Hash { get; init; } = string.Empty;

    public DateTimeOffset? PdfAttachedAt { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }

    public string Period => $"{InvoiceYear:D4}/{InvoiceMonth:D2}";

    public string State => Status switch
    {
        "paid" => "Ödendi",
        "canceled" => "İptal",
        _ when PaidAmount > 0 => "Kısmi",
        _ => "Ödenmedi",
    };

    public string AmountText => Amount.ToString("N2");

    public string PaidAmountText => PaidAmount.ToString("N2");

    public string RemainingAmountText => RemainingAmount.ToString("N2");

    public string PaymentSummaryText => $"Ödenen {PaidAmountText} / Kalan {RemainingAmountText}";

    public string UsageText => $"{UsageAmount:N2} {UsageUnit}".Trim();

    public bool HasPdf => !string.IsNullOrWhiteSpace(PdfFilePath);

    public string PdfState => HasPdf ? "PDF Var" : "PDF Yok";
}
