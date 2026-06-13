using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Infrastructure;

public static class PaymentEntryHelperSummaryBuilder
{
    public static string BuildLastActionText(string? selectedActionKey)
    {
        return selectedActionKey switch
        {
            "fill_remaining" => "Son hizli yardim: Kalan Tutar uygulandi.",
            "use_last" => "Son hizli yardim: Son Aciklama uygulandi.",
            "use_selected" => "Son hizli yardim: Secili Odeme uygulandi.",
            _ => string.Empty,
        };
    }

    public static string BuildLastActionToolTip(string? selectedActionKey)
    {
        return selectedActionKey switch
        {
            "fill_remaining" => "Tikla ve Kalan Tutar yardimini yeniden calistir.",
            "use_last" => "Tikla ve Son Aciklama yardimini yeniden calistir.",
            "use_selected" => "Tikla ve Secili Odeme yardimini yeniden calistir.",
            _ => string.Empty,
        };
    }

    public static string BuildLastActionPrefix(string? selectedActionKey)
    {
        return selectedActionKey switch
        {
            "fill_remaining" => "KLN",
            "use_last" => "SON",
            "use_selected" => "SEC",
            _ => string.Empty,
        };
    }

    public static string BuildReplayFeedbackText(string? baseText, bool isReplayActive)
    {
        if (string.IsNullOrWhiteSpace(baseText))
        {
            return string.Empty;
        }

        return isReplayActive
            ? $"{baseText} (yeniden tetiklendi)"
            : baseText;
    }

    public static string BuildReplayPreferenceSummaryText(string? selectedActionKey, int seconds, string emphasis)
    {
        var actionLabel = selectedActionKey switch
        {
            "fill_remaining" => "Kalan Tutar",
            "use_last" => "Son Aciklama",
            "use_selected" => "Secili Odeme",
            _ => string.Empty
        };

        var emphasisLabel = emphasis switch
        {
            "low" => "dusuk",
            "high" => "guclu",
            _ => "orta"
        };

        return string.IsNullOrWhiteSpace(actionLabel)
            ? $"Replay ayari hazir: {seconds} sn, {emphasisLabel} vurgu."
            : $"{actionLabel} replay ayari: {seconds} sn, {emphasisLabel} vurgu.";
    }

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
                $"Tikla veya Enter/Space ile kalan tutari {invoice.RemainingAmountText} olarak doldur.",
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
                $"Tikla veya Enter/Space ile son odeme aciklamasini doldur: {recentDescription}",
                "use_last",
                IsSelected: string.Equals(selectedActionKey, "use_last", StringComparison.Ordinal)));
        }

        if (selectedPayment is not null)
        {
            badges.Add(new PaymentHelperBadge(
                "SEC",
                "Secili Odeme",
                "detail",
                $"Tikla veya Enter/Space ile secili odemeyi temel al: {selectedPayment.AmountText}",
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
