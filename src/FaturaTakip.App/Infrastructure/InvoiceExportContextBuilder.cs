using System.Globalization;
using System.Text;
using FaturaTakip.App.Data.Invoices;

namespace FaturaTakip.App.Infrastructure;

public static class InvoiceExportContextBuilder
{
    public sealed record ExportContext(string FileSlug, string SummaryLabel);

    public static ExportContext Build(
        InvoiceFilterCriteria criteria,
        DateTime today,
        string? invoiceTypeLabel = null,
        string? subscriptionLabel = null)
    {
        var slugParts = new List<string>();
        var labelParts = new List<string>();

        if (criteria.Year is not null)
        {
            slugParts.Add(criteria.Year.Value.ToString());
            labelParts.Add($"Yil {criteria.Year.Value}");
        }

        if (criteria.Month is not null)
        {
            slugParts.Add(criteria.Month.Value.ToString("00"));
            labelParts.Add($"Ay {criteria.Month.Value:00}");
        }

        if (criteria.Year == today.Year && criteria.Month == today.Month)
        {
            slugParts.Add("bu-ay");
            labelParts.Add("Bu Ay");
        }

        if (criteria.PaymentStatus != InvoicePaymentStatusFilter.All)
        {
            var paymentLabel = criteria.PaymentStatus switch
            {
                InvoicePaymentStatusFilter.Unpaid => "Odenmemis",
                InvoicePaymentStatusFilter.Paid => "Odendi",
                InvoicePaymentStatusFilter.Canceled => "Iptal",
                InvoicePaymentStatusFilter.Overdue => "Gecikmis",
                _ => "Odeme",
            };

            slugParts.Add(Slugify(paymentLabel));
            labelParts.Add(paymentLabel);
        }

        if (criteria.PdfStatus != InvoicePdfStatusFilter.All)
        {
            var pdfLabel = criteria.PdfStatus switch
            {
                InvoicePdfStatusFilter.HasPdf => "Pdf Var",
                InvoicePdfStatusFilter.MissingPdf => "Pdf Eksik",
                _ => "Pdf",
            };

            slugParts.Add(Slugify(pdfLabel));
            labelParts.Add(pdfLabel);
        }

        if (criteria.InvoiceTypeId is not null && !string.IsNullOrWhiteSpace(invoiceTypeLabel))
        {
            slugParts.Add($"tur-{Slugify(invoiceTypeLabel)}");
            labelParts.Add($"Tur {invoiceTypeLabel}");
        }

        if (criteria.SubscriptionId is not null && !string.IsNullOrWhiteSpace(subscriptionLabel))
        {
            slugParts.Add($"abonelik-{Slugify(subscriptionLabel)}");
            labelParts.Add($"Abonelik {subscriptionLabel}");
        }

        if (!string.IsNullOrWhiteSpace(criteria.SearchText))
        {
            slugParts.Add($"ara-{Slugify(criteria.SearchText)}");
            labelParts.Add("Arama");
        }

        var distinctParts = slugParts
            .Where(part => !string.IsNullOrWhiteSpace(part))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var fileSlug = distinctParts.Count == 0
            ? "tum-kayitlar"
            : string.Join("-", distinctParts);

        var summaryLabel = labelParts.Count == 0
            ? "Tum kayitlar"
            : string.Join(", ", labelParts.Distinct(StringComparer.CurrentCultureIgnoreCase));

        return new ExportContext(fileSlug, summaryLabel);
    }

    private static string Slugify(string value)
    {
        var normalized = value.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();
        var previousWasDash = false;

        foreach (var ch in normalized.Trim())
        {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) == UnicodeCategory.NonSpacingMark)
            {
                continue;
            }

            var mapped = ch switch
            {
                'ı' => 'i',
                'İ' => 'i',
                _ => char.ToLowerInvariant(ch),
            };

            if (char.IsLetterOrDigit(mapped))
            {
                builder.Append(mapped);
                previousWasDash = false;
                continue;
            }

            if (previousWasDash)
            {
                continue;
            }

            builder.Append('-');
            previousWasDash = true;
        }

        var slug = builder.ToString().Trim('-');
        if (slug.Length == 0)
        {
            return "secim";
        }

        return slug.Length > 32 ? slug[..32].Trim('-') : slug;
    }
}
