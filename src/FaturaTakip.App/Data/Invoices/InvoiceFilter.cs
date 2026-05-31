namespace FaturaTakip.App.Data.Invoices;

public static class InvoiceFilter
{
    public static IReadOnlyList<Invoice> Apply(
        IEnumerable<Invoice> invoices,
        InvoiceFilterCriteria criteria,
        DateTime today,
        Func<Invoice, bool>? isPdfMissing = null)
    {
        var query = invoices;

        if (criteria.Year is not null)
        {
            query = query.Where(invoice => invoice.InvoiceYear == criteria.Year.Value);
        }

        if (criteria.Month is not null)
        {
            query = query.Where(invoice => invoice.InvoiceMonth == criteria.Month.Value);
        }

        if (criteria.InvoiceTypeId is not null)
        {
            query = query.Where(invoice => invoice.InvoiceTypeId == criteria.InvoiceTypeId.Value);
        }

        if (criteria.SubscriptionId is not null)
        {
            query = query.Where(invoice => invoice.SubscriptionId == criteria.SubscriptionId.Value);
        }

        query = criteria.PaymentStatus switch
        {
            InvoicePaymentStatusFilter.Unpaid => query.Where(invoice => invoice.Status == "unpaid"),
            InvoicePaymentStatusFilter.Paid => query.Where(invoice => invoice.Status == "paid"),
            InvoicePaymentStatusFilter.Canceled => query.Where(invoice => invoice.Status == "canceled"),
            InvoicePaymentStatusFilter.Overdue => query.Where(invoice => invoice.Status == "unpaid" && invoice.DueDate.Date < today.Date),
            _ => query,
        };

        query = criteria.PdfStatus switch
        {
            InvoicePdfStatusFilter.HasPdf => query.Where(invoice => invoice.HasPdf && !(isPdfMissing?.Invoke(invoice) ?? false)),
            InvoicePdfStatusFilter.MissingPdf => query.Where(invoice => isPdfMissing?.Invoke(invoice) ?? !invoice.HasPdf),
            _ => query,
        };

        var searchTerms = criteria.SearchText
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var term in searchTerms)
        {
            query = query.Where(invoice => MatchesSearch(invoice, term));
        }

        return query.ToList();
    }

    private static bool MatchesSearch(Invoice invoice, string term)
    {
        return Contains(invoice.InvoiceNo, term) ||
               Contains(invoice.InvoiceTypeName, term) ||
               Contains(invoice.SubscriptionName, term) ||
               Contains(invoice.InstitutionName, term) ||
               Contains(invoice.Description, term) ||
               Contains(invoice.PdfOriginalFileName, term);
    }

    private static bool Contains(string value, string term)
    {
        return value.Contains(term, StringComparison.CurrentCultureIgnoreCase);
    }
}
