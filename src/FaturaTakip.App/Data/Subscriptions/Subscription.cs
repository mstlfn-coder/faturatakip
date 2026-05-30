namespace FaturaTakip.App.Data.Subscriptions;

public sealed class Subscription
{
    public long Id { get; init; }

    public long InvoiceTypeId { get; init; }

    public string InvoiceTypeName { get; init; } = string.Empty;

    public string SubscriptionName { get; init; } = string.Empty;

    public string InstitutionName { get; init; } = string.Empty;

    public string SubscriberNo { get; init; } = string.Empty;

    public string InstallationNo { get; init; } = string.Empty;

    public string MeterNo { get; init; } = string.Empty;

    public string ProviderCompany { get; init; } = string.Empty;

    public string ServiceAddress { get; init; } = string.Empty;

    public string UnitName { get; init; } = string.Empty;

    public string DefaultUsageUnit { get; init; } = string.Empty;

    public bool IsActive { get; init; }

    public DateTime? StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public string Description { get; init; } = string.Empty;

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset UpdatedAt { get; init; }

    public string State => IsActive ? "Aktif" : "Pasif";

    public string Period => (StartDate, EndDate) switch
    {
        (null, null) => string.Empty,
        ({ } start, null) => $"{start:dd.MM.yyyy} -",
        (null, { } end) => $"- {end:dd.MM.yyyy}",
        ({ } start, { } end) => $"{start:dd.MM.yyyy} - {end:dd.MM.yyyy}",
    };
}
