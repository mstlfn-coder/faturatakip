using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Infrastructure;

public static class PaymentPdfHelperSummaryBuilder
{
    public static string BuildLastActionText(string? selectedActionKey)
    {
        return selectedActionKey switch
        {
            "select_pdf" => "Son hizli yardim: PDF Sec uygulandi.",
            "open_pdf" => "Son hizli yardim: PDF Ac uygulandi.",
            _ => string.Empty,
        };
    }

    public static string BuildLastActionToolTip(string? selectedActionKey)
    {
        return selectedActionKey switch
        {
            "select_pdf" => "Tikla ve PDF Sec yardimini yeniden calistir.",
            "open_pdf" => "Tikla ve PDF Ac yardimini yeniden calistir.",
            _ => string.Empty,
        };
    }

    public static IReadOnlyList<PaymentPdfHelperBadge> BuildBadges(
        Payment? selectedPayment,
        bool paymentPdfExists,
        string? selectedActionKey = null)
    {
        var badges = new List<PaymentPdfHelperBadge>();

        if (selectedPayment is null)
        {
            return badges;
        }

        var primaryActionKey = !selectedPayment.HasPdf || !paymentPdfExists
            ? "select_pdf"
            : "open_pdf";
        var primaryActionLabel = primaryActionKey == "open_pdf"
            ? "Tikla veya Enter/Space ile PDF kaydini ac."
            : "Tikla veya Enter/Space ile bu odeme icin PDF sec.";

        badges.Add(new PaymentPdfHelperBadge(
            "SEC",
            "Secili Odeme",
            "detail",
            $"PDF islemleri icin secili odeme hazir: {selectedPayment.AmountText}. {primaryActionLabel}",
            primaryActionKey,
            IsSelected: string.Equals(selectedActionKey, primaryActionKey, StringComparison.Ordinal)));

        if (!selectedPayment.HasPdf)
        {
            badges.Add(new PaymentPdfHelperBadge(
                "EKL",
                "PDF Bekleniyor",
                "period",
                "Bu odeme kaydina henuz PDF eklenmemis. Tikla veya Enter/Space ile PDF sec.",
                "select_pdf",
                IsSelected: string.Equals(selectedActionKey, "select_pdf", StringComparison.Ordinal)));
            return badges;
        }

        badges.Add(new PaymentPdfHelperBadge(
            paymentPdfExists ? "VAR" : "KYP",
            paymentPdfExists ? "PDF Kayitli" : "PDF Kayip",
            paymentPdfExists ? "filter" : "context",
            paymentPdfExists
                ? $"PDF kaydi hazir: {selectedPayment.PdfOriginalFileName}. Tikla veya Enter/Space ile PDF ac."
                : $"PDF kaydi var ama dosya bulunamadi: {selectedPayment.PdfFilePath}. Tikla veya Enter/Space ile PDF sec.",
            paymentPdfExists ? "open_pdf" : "select_pdf",
            IsSelected: string.Equals(selectedActionKey, paymentPdfExists ? "open_pdf" : "select_pdf", StringComparison.Ordinal)));

        return badges;
    }

    public static string BuildSummaryText(Payment? selectedPayment, bool paymentPdfExists)
    {
        if (selectedPayment is null)
        {
            return "PDF islemleri icin secili odeme yok.";
        }

        if (!selectedPayment.HasPdf)
        {
            return "Secili odeme icin PDF eklenmesi bekleniyor.";
        }

        return paymentPdfExists
            ? "Secili odeme icin PDF kaydi hazir."
            : "Secili odeme icin PDF kaydi var ama dosya bulunamadi.";
    }

    public sealed record PaymentPdfHelperBadge(string Prefix, string Text, string Kind, string ToolTip, string ActionKey, bool IsSelected = false);
}
