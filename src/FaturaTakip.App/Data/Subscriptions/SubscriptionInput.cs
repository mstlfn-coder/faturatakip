namespace FaturaTakip.App.Data.Subscriptions;

public sealed record SubscriptionInput(
    long InvoiceTypeId,
    string SubscriptionName,
    string InstitutionName,
    string SubscriberNo,
    string InstallationNo,
    string MeterNo,
    string ProviderCompany,
    string ServiceAddress,
    string UnitName,
    string DefaultUsageUnit,
    bool IsActive,
    DateTime? StartDate,
    DateTime? EndDate,
    string Description);
