using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public static class YearlyInvoiceListReportCalculator
{
    public static YearlyInvoiceListReport Calculate(
        IEnumerable<Invoice> invoices,
        int year,
        long? invoiceTypeId,
        DateTime today,
        Func<Invoice, bool> isPdfMissing)
    {
        var list = invoices
            .Where(i => i.InvoiceYear == year)
            .Where(i => invoiceTypeId is null || i.InvoiceTypeId == invoiceTypeId.Value)
            .OrderBy(i => i.InvoiceMonth)
            .ThenBy(i => i.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(i => i.InvoiceDate)
            .ThenBy(i => i.InvoiceNo, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        var rows = list
            .Select(i => new MonthlyInvoiceRow(i, GetPdfState(i, isPdfMissing)))
            .ToList();

        var unpaidCount = list.Count(i => string.Equals(i.Status, "unpaid", StringComparison.OrdinalIgnoreCase));
        var overdueCount = list.Count(i =>
            string.Equals(i.Status, "unpaid", StringComparison.OrdinalIgnoreCase) &&
            i.DueDate.Date < today.Date);
        var missingPdfCount = list.Count(i => isPdfMissing(i));

        return new YearlyInvoiceListReport(
            year,
            rows,
            list.Count,
            list.Sum(i => i.Amount),
            list.Sum(i => i.PaidAmount),
            list.Sum(i => i.RemainingAmount),
            unpaidCount,
            overdueCount,
            missingPdfCount);
    }

    private static string GetPdfState(Invoice invoice, Func<Invoice, bool> isPdfMissing)
    {
        if (!invoice.HasPdf)
        {
            return "PDF Yok";
        }

        return isPdfMissing(invoice) ? "PDF KayÄ±p" : "PDF Var";
    }
}

