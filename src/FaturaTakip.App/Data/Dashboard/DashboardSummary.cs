namespace FaturaTakip.App.Data.Dashboard;

public sealed record DashboardSummary(
    int MonthlyInvoiceCount,
    decimal MonthlyInvoiceTotal,
    int MonthlyPaymentCount,
    decimal MonthlyPaymentTotal,
    int UnpaidInvoiceCount,
    decimal UnpaidRemainingTotal,
    int OverdueInvoiceCount,
    decimal OverdueRemainingTotal,
    int MissingInvoicePdfCount,
    int MissingPaymentPdfCount);
