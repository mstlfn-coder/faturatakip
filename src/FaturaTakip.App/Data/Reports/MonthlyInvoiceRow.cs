using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public sealed record MonthlyInvoiceRow(Invoice Invoice, string PdfState);

