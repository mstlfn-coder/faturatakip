using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Data.Reports;

public static class DocumentHealthReportCalculator
{
    public static DocumentHealthReport Calculate(
        IEnumerable<Invoice> invoices,
        IEnumerable<Payment> payments,
        Func<Invoice, bool> invoicePdfFileExists,
        Func<Payment, bool> paymentPdfFileExists)
    {
        var invoiceList = invoices.ToList();
        var paymentList = payments.ToList();
        var invoiceById = invoiceList.ToDictionary(item => item.Id);

        var issues = new List<DocumentHealthIssue>();

        var invoiceNoPdfCount = 0;
        var invoiceMissingFileCount = 0;
        foreach (var invoice in invoiceList)
        {
            if (!invoice.HasPdf)
            {
                invoiceNoPdfCount++;
                issues.Add(DocumentHealthIssue.FromInvoice("PDF Yok", invoice, pdfState: "PDF Yok", note: string.Empty));
                continue;
            }

            if (!invoicePdfFileExists(invoice))
            {
                invoiceMissingFileCount++;
                issues.Add(DocumentHealthIssue.FromInvoice("PDF Kayıp", invoice, pdfState: "PDF Kayıp", note: string.Empty));
            }
        }

        var paymentNoPdfCount = 0;
        var paymentMissingFileCount = 0;
        foreach (var payment in paymentList)
        {
            if (!payment.HasPdf)
            {
                paymentNoPdfCount++;
                issues.Add(DocumentHealthIssue.FromPayment(
                    "PDF Yok",
                    payment,
                    invoiceById.TryGetValue(payment.InvoiceId, out var invoice) ? invoice : null,
                    pdfState: "PDF Yok",
                    note: $"FaturaId={payment.InvoiceId}"));
                continue;
            }

            if (!paymentPdfFileExists(payment))
            {
                paymentMissingFileCount++;
                issues.Add(DocumentHealthIssue.FromPayment(
                    "PDF Kayıp",
                    payment,
                    invoiceById.TryGetValue(payment.InvoiceId, out var invoice) ? invoice : null,
                    pdfState: "PDF Kayıp",
                    note: $"FaturaId={payment.InvoiceId}"));
            }
        }

        var duplicateInvoiceHashItemCount = AddDuplicateHashIssues(
            issues,
            entityType: "Fatura",
            invoiceList
                .Where(item => item.HasPdf && !string.IsNullOrWhiteSpace(item.PdfSha256Hash))
                .Select(item => new DuplicateHashItem(
                    item.PdfSha256Hash,
                    item.Id,
                    item.Period,
                    item.InvoiceTypeName,
                    item.SubscriptionName,
                    item.InstitutionName,
                    item.Amount,
                    item.PdfFilePath)));

        var duplicatePaymentHashItemCount = AddDuplicateHashIssues(
            issues,
            entityType: "Ödeme",
            paymentList
                .Where(item => item.HasPdf && !string.IsNullOrWhiteSpace(item.PdfSha256Hash))
                .Select(item =>
                {
                    invoiceById.TryGetValue(item.InvoiceId, out var invoice);
                    var period = invoice is null ? item.PaymentDate.ToString("yyyy-MM-dd") : $"{invoice.Period} / {item.PaymentDate:dd.MM.yyyy}";
                    return new DuplicateHashItem(
                        item.PdfSha256Hash,
                        item.Id,
                        period,
                        invoice?.InvoiceTypeName ?? string.Empty,
                        invoice?.SubscriptionName ?? string.Empty,
                        invoice?.InstitutionName ?? string.Empty,
                        item.Amount,
                        item.PdfFilePath);
                }));

        var ordered = issues
            .OrderBy(item => item.IssueType, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(item => item.EntityType, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(item => item.PeriodOrDate, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(item => item.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(item => item.EntityId)
            .ToList();

        return new DocumentHealthReport(
            ordered,
            invoiceNoPdfCount,
            invoiceMissingFileCount,
            paymentNoPdfCount,
            paymentMissingFileCount,
            duplicateInvoiceHashItemCount,
            duplicatePaymentHashItemCount);
    }

    private static int AddDuplicateHashIssues(
        List<DocumentHealthIssue> issues,
        string entityType,
        IEnumerable<DuplicateHashItem> items)
    {
        var groups = items
            .GroupBy(item => item.Hash, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Count() > 1)
            .ToList();

        var count = 0;
        foreach (var group in groups)
        {
            var groupSize = group.Count();
            foreach (var item in group)
            {
                count++;
                issues.Add(new DocumentHealthIssue(
                    "Aynı Hash",
                    entityType,
                    item.EntityId,
                    item.PeriodOrDate,
                    item.InvoiceTypeName,
                    item.SubscriptionName,
                    item.InstitutionName,
                    item.Amount,
                    PdfState: "PDF Var",
                    item.FilePath,
                    item.Hash,
                    Note: $"{groupSize} kayıt"));
            }
        }

        return count;
    }

    private sealed record DuplicateHashItem(
        string Hash,
        long EntityId,
        string PeriodOrDate,
        string InvoiceTypeName,
        string SubscriptionName,
        string InstitutionName,
        decimal Amount,
        string FilePath);
}
