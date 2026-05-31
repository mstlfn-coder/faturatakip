namespace FaturaTakip.App.Data.Reports;

public sealed record InvoiceTypeYearlyDistributionRow(
    long SubscriptionId,
    string SubscriptionName,
    string InstitutionName,
    int InvoiceCount,
    decimal TotalAmount,
    decimal PaidTotal,
    decimal RemainingTotal,
    int MissingPdfCount);

