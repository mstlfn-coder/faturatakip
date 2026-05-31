using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public sealed record ActionableInvoice(Invoice Invoice, string PdfState);

