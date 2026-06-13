using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Infrastructure;

public static class PaymentPdfHelperSummaryBuilder
{
    public static IReadOnlyList<PaymentPdfHelperBadge> BuildBadges(
        Payment? selectedPayment,
        bool paymentPdfExists)
    {
        var badges = new List<PaymentPdfHelperBadge>();

        if (selectedPayment is null)
        {
            return badges;
        }

        badges.Add(new PaymentPdfHelperBadge(
            "SEC",
            "Secili Odeme",
            "detail",
            $"PDF islemleri icin secili odeme hazir: {selectedPayment.AmountText}"));

        if (!selectedPayment.HasPdf)
        {
            badges.Add(new PaymentPdfHelperBadge(
                "EKL",
                "PDF Bekleniyor",
                "period",
                "Bu odeme kaydina henuz PDF eklenmemis."));
            return badges;
        }

        badges.Add(new PaymentPdfHelperBadge(
            paymentPdfExists ? "VAR" : "KYP",
            paymentPdfExists ? "PDF Kayitli" : "PDF Kayip",
            paymentPdfExists ? "filter" : "context",
            paymentPdfExists
                ? $"PDF kaydi hazir: {selectedPayment.PdfOriginalFileName}"
                : $"PDF kaydi var ama dosya bulunamadi: {selectedPayment.PdfFilePath}"));

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

    public sealed record PaymentPdfHelperBadge(string Prefix, string Text, string Kind, string ToolTip);
}
