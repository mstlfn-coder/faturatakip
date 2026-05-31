using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public static class ActionableInvoiceReportCalculator
{
    public static ActionableInvoiceReport Calculate(
        IEnumerable<Invoice> invoices,
        DateTime today,
        int upcomingDays,
        Func<Invoice, bool> isPdfMissing)
    {
        var invoiceList = invoices.ToList();
        var todayDate = today.Date;
        var upcomingEnd = todayDate.AddDays(Math.Max(upcomingDays, 0));

        var unpaid = invoiceList
            .Where(item => string.Equals(item.Status, "unpaid", StringComparison.OrdinalIgnoreCase))
            .ToList();

        var overdue = unpaid
            .Where(item => item.DueDate.Date < todayDate)
            .ToList();

        var upcoming = unpaid
            .Where(item => item.DueDate.Date >= todayDate && item.DueDate.Date <= upcomingEnd)
            .ToList();

        IReadOnlyList<ActionableInvoice> Map(IEnumerable<Invoice> items)
        {
            return items
                .OrderBy(item => item.DueDate)
                .ThenBy(item => item.InvoiceTypeName, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(item => item.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
                .Select(item => new ActionableInvoice(item, GetPdfState(item, isPdfMissing)))
                .ToList();
        }

        var unpaidRows = Map(unpaid);
        var overdueRows = Map(overdue);
        var upcomingRows = Map(upcoming);

        return new ActionableInvoiceReport(
            unpaidRows,
            unpaidRows.Sum(item => item.Invoice.RemainingAmount),
            overdueRows,
            overdueRows.Sum(item => item.Invoice.RemainingAmount),
            upcomingRows,
            upcomingRows.Sum(item => item.Invoice.RemainingAmount));
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

