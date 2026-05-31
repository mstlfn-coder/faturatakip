using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public static class SubscriptionMonthlyComparisonCalculator
{
    public static SubscriptionMonthlyComparison Calculate(
        IEnumerable<Invoice> invoices,
        long subscriptionId,
        int year,
        int month,
        DateTime today,
        Func<Invoice, bool> isPdfMissing)
    {
        var filtered = invoices
            .Where(item => item.SubscriptionId == subscriptionId)
            .ToList();

        var current = MonthlyInvoiceReportCalculator.Calculate(
            filtered,
            year,
            month,
            invoiceTypeId: null,
            today,
            isPdfMissing);

        var (previousYear, previousMonth) = GetPreviousMonth(year, month);
        var previous = MonthlyInvoiceReportCalculator.Calculate(
            filtered,
            previousYear,
            previousMonth,
            invoiceTypeId: null,
            today,
            isPdfMissing);

        return new SubscriptionMonthlyComparison(
            subscriptionId,
            current,
            previous,
            current.TotalInvoiceCount - previous.TotalInvoiceCount,
            current.TotalAmount - previous.TotalAmount,
            current.PaidTotal - previous.PaidTotal,
            current.RemainingTotal - previous.RemainingTotal);
    }

    private static (int Year, int Month) GetPreviousMonth(int year, int month)
    {
        if (month <= 1)
        {
            return (year - 1, 12);
        }

        return (year, month - 1);
    }
}

