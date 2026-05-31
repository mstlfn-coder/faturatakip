using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Data.Reports;

public static class InvoiceTypeYearlyReportCalculator
{
    public static InvoiceTypeYearlyReport Calculate(
        IEnumerable<Invoice> invoices,
        long invoiceTypeId,
        string invoiceTypeName,
        int year,
        DateTime today,
        Func<Invoice, bool> isPdfMissing)
    {
        var todayDate = today.Date;
        var list = invoices
            .Where(item => item.InvoiceTypeId == invoiceTypeId && item.InvoiceYear == year)
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

        var distribution = list
            .GroupBy(item => new { item.SubscriptionId, item.SubscriptionName, item.InstitutionName })
            .Select(group =>
            {
                var groupItems = group.ToList();
                return new InvoiceTypeYearlyDistributionRow(
                    group.Key.SubscriptionId,
                    group.Key.SubscriptionName,
                    group.Key.InstitutionName,
                    groupItems.Count,
                    groupItems.Sum(item => item.Amount),
                    groupItems.Sum(item => item.PaidAmount),
                    groupItems.Sum(item => item.RemainingAmount),
                    groupItems.Count(item => isPdfMissing(item)));
            })
            .OrderByDescending(item => item.TotalAmount)
            .ThenBy(item => item.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

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

        return new InvoiceTypeYearlyReport(
            invoiceTypeId,
            invoiceTypeName,
            year,
            months,
            distribution,
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

