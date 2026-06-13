using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Infrastructure;

public static class PaymentEntryHelperSummaryBuilder
{
    public static IReadOnlyList<PaymentHelperBadge> BuildBadges(
        Invoice? invoice,
        IEnumerable<Payment> payments,
        Payment? selectedPayment,
        string? selectedActionKey = null)
    {
        var badges = new List<PaymentHelperBadge>();
        var paymentList = payments?.ToList() ?? [];

        if (invoice is not null && invoice.RemainingAmount > 0)
        {
            badges.Add(new PaymentHelperBadge(
                "KLN",
                "Kalan Tutar",
                "filter",
                $"Kalani Doldur yardimi kalan tutari {invoice.RemainingAmountText} olarak hazirlar.",
                "fill_remaining",
                IsSelected: string.Equals(selectedActionKey, "fill_remaining", StringComparison.Ordinal)));
        }

        var recentPayment = paymentList
            .OrderByDescending(item => item.PaymentDate)
            .ThenByDescending(item => item.Id)
            .FirstOrDefault();
        var recentDescription = recentPayment?.Description?.Trim() ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(recentDescription))
        {
            badges.Add(new PaymentHelperBadge(
                "SON",
                "Son Aciklama",
                "context",
                $"Son Odemeden Doldur yardimi son aciklamayi getirir: {recentDescription}",
                "use_last",
                IsSelected: string.Equals(selectedActionKey, "use_last", StringComparison.Ordinal)));
        }

        if (selectedPayment is not null)
        {
            badges.Add(new PaymentHelperBadge(
                "SEC",
                "Secili Odeme",
                "detail",
                $"Secili Odemeden Doldur yardimi {selectedPayment.AmountText} tutarli odemeyi temel alir.",
                "use_selected",
                IsSelected: string.Equals(selectedActionKey, "use_selected", StringComparison.Ordinal)));
        }

        return badges;
    }

    public static string BuildSummaryText(
        Invoice? invoice,
        IEnumerable<Payment> payments,
        Payment? selectedPayment)
    {
        var badges = BuildBadges(invoice, payments, selectedPayment);
        if (badges.Count == 0)
        {
            return "Hazir odeme yardimi yok.";
        }

        return $"Hazir yardimlar: {string.Join(", ", badges.Select(item => item.Text))}.";
    }

    public sealed record PaymentHelperBadge(string Prefix, string Text, string Kind, string ToolTip, string ActionKey, bool IsSelected = false);
}
