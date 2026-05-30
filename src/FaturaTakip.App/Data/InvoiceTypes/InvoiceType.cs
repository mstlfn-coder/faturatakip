namespace FaturaTakip.App.Data.InvoiceTypes;

public sealed class InvoiceType
{
    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string DefaultUsageUnit { get; init; } = string.Empty;

    public bool IsActive { get; init; }

    public string State => IsActive ? "Aktif" : "Pasif";

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }
}
