using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public static class SubscriptionYearlyReportCalculator
{
    public static SubscriptionYearlyReport Calculate(
        IEnumerable<Invoice> invoices,
        long subscriptionId,
        int year,
        DateTime today,
        Func<Invoice, bool> isPdfMissing)
    {
        var todayDate = today.Date;
        var list = invoices
            .Where(item => item.SubscriptionId == subscriptionId && item.InvoiceYear == year)
            .ToList();

        var months = new List<SubscriptionYearlyMonthRow>(12);
        for (var month = 1; month <= 12; month++)
        {
            var monthItems = list.Where(item => item.InvoiceMonth == month).ToList();
            var unpaidCount = monthItems.Count(item => string.Equals(item.Status, "unpaid", StringComparison.OrdinalIgnoreCase));
            var overdueCount = monthItems.Count(item =>
                string.Equals(item.Status, "unpaid", StringComparison.OrdinalIgnoreCase) &&
                item.DueDate.Date < todayDate);
            var missingPdfCount = monthItems.Count(item => isPdfMissing(item));

            months.Add(new SubscriptionYearlyMonthRow(
                month,
                monthItems.Count,
                monthItems.Sum(item => item.Amount),
                monthItems.Sum(item => item.PaidAmount),
                monthItems.Sum(item => item.RemainingAmount),
                unpaidCount,
                overdueCount,
                missingPdfCount));
        }

        var totalInvoiceCount = months.Sum(item => item.InvoiceCount);
        var totalAmount = months.Sum(item => item.TotalAmount);
        var paidTotal = months.Sum(item => item.PaidTotal);
        var remainingTotal = months.Sum(item => item.RemainingTotal);
        var missingTotal = months.Sum(item => item.MissingPdfCount);

        var monthsWithData = months.Where(item => item.InvoiceCount > 0).ToList();
        var highest = monthsWithData.Count == 0
            ? months[0]
            : monthsWithData.OrderByDescending(item => item.TotalAmount).ThenBy(item => item.Month).First();
        var lowest = monthsWithData.Count == 0
            ? months[0]
            : monthsWithData.OrderBy(item => item.TotalAmount).ThenBy(item => item.Month).First();

        return new SubscriptionYearlyReport(
            subscriptionId,
            year,
            months,
            totalInvoiceCount,
            totalAmount,
            paidTotal,
            remainingTotal,
            missingTotal,
            highest.Month,
            highest.TotalAmount,
            lowest.Month,
            lowest.TotalAmount);
    }
}

