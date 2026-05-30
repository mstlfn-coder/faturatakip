namespace FaturaTakip.App.Data.InvoiceTypes;

public sealed record InvoiceTypeInput(
    string Name,
    string Description,
    string DefaultUsageUnit,
    bool IsActive);
