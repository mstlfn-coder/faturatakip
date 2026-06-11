using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Data.Dashboard;

public static class DashboardSummaryCalculator
{
    public static DashboardSummary Calculate(
        IEnumerable<Invoice> invoices,
        IEnumerable<Payment> payments,
        DateTime today,
        Func<Invoice, bool> isInvoicePdfMissing,
        Func<Payment, bool> isPaymentPdfMissing)
    {
        var invoiceList = invoices.ToList();
        var paymentList = payments.ToList();
        var monthStart = new DateTime(today.Year, today.Month, 1);

        var monthlyInvoices = invoiceList
            .Where(item => item.InvoiceYear == monthStart.Year && item.InvoiceMonth == monthStart.Month)
            .ToList();
        var monthlyPayments = paymentList
            .Where(item => item.PaymentDate.Year == monthStart.Year && item.PaymentDate.Month == monthStart.Month)
            .ToList();
        var unpaidInvoices = invoiceList
            .Where(item => string.Equals(item.Status, "unpaid", StringComparison.OrdinalIgnoreCase))
            .ToList();
        var unreviewedInvoices = invoiceList
            .Where(item => item.ReviewedAt is null && string.IsNullOrWhiteSpace(item.ReviewNote))
            .ToList();
        var overdueInvoices = unpaidInvoices
            .Where(item => item.DueDate.Date < today.Date)
            .ToList();

        return new DashboardSummary(
            monthlyInvoices.Count,
            monthlyInvoices.Sum(item => item.Amount),
            monthlyPayments.Count,
            monthlyPayments.Sum(item => item.Amount),
            unreviewedInvoices.Count,
            unpaidInvoices.Count,
            unpaidInvoices.Sum(item => item.RemainingAmount),
            overdueInvoices.Count,
            overdueInvoices.Sum(item => item.RemainingAmount),
            invoiceList.Count(isInvoicePdfMissing),
            paymentList.Count(isPaymentPdfMissing));
    }
}
