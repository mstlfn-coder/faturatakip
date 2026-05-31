using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public static class MonthlyInvoiceReportCalculator
{
    public static MonthlyInvoiceReport Calculate(
        IEnumerable<Invoice> invoices,
        int year,
        int month,
        DateTime today,
        Func<Invoice, bool> isPdfMissing)
    {
        var list = invoices
            .Where(item => item.InvoiceYear == year && item.InvoiceMonth == month)
            .OrderBy(item => item.DueDate)
            .ThenBy(item => item.InvoiceTypeName, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(item => item.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(item => item.InvoiceNo, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        var rows = list
            .Select(item => new MonthlyInvoiceRow(item, GetPdfState(item, isPdfMissing)))
            .ToList();

        var unpaidCount = list.Count(item => string.Equals(item.Status, "unpaid", StringComparison.OrdinalIgnoreCase));
        var overdueCount = list.Count(item =>
            string.Equals(item.Status, "unpaid", StringComparison.OrdinalIgnoreCase) &&
            item.DueDate.Date < today.Date);
        var missingPdfCount = list.Count(item => isPdfMissing(item));

        return new MonthlyInvoiceReport(
            year,
            month,
            rows,
            list.Count,
            list.Sum(item => item.Amount),
            list.Sum(item => item.PaidAmount),
            list.Sum(item => item.RemainingAmount),
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

        return isPdfMissing(invoice) ? "PDF Kayıp" : "PDF Var";
    }
}

