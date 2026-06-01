using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Data.Reports;

public static class ConsistencyReportCalculator
{
    public static ConsistencyReport Calculate(
        IReadOnlyList<Invoice> invoices,
        IReadOnlyList<Payment> payments,
        Func<Invoice, bool>? invoicePdfExists = null,
        Func<Payment, bool>? paymentPdfExists = null)
    {
        var issues = new List<ConsistencyIssue>();

        var invoiceIdSet = invoices.Select(item => item.Id).ToHashSet();

        // Orphan payments
        foreach (var payment in payments)
        {
            if (!invoiceIdSet.Contains(payment.InvoiceId))
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "ERROR",
                    Code: "ORPHAN_PAYMENT",
                    Entity: "payment",
                    EntityId: payment.Id,
                    Message: $"Ödeme kaydı faturaya bağlı değil. invoice_id={payment.InvoiceId}"));
            }

            if (payment.Amount <= 0)
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "ERROR",
                    Code: "PAYMENT_AMOUNT_NON_POSITIVE",
                    Entity: "payment",
                    EntityId: payment.Id,
                    Message: $"Ödeme tutarı geçersiz: {payment.Amount:N2}"));
            }

            if (payment.HasPdf && paymentPdfExists is not null && !paymentPdfExists(payment))
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "WARN",
                    Code: "PAYMENT_PDF_MISSING",
                    Entity: "payment",
                    EntityId: payment.Id,
                    Message: "Ödeme PDF dosyası kayıtlı ama dosya bulunamadı."));
            }
        }

        // Invoice payment/status consistency
        const decimal epsilon = 0.01m;
        foreach (var invoice in invoices)
        {
            if (invoice.Amount < 0)
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "ERROR",
                    Code: "INVOICE_AMOUNT_NEGATIVE",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: $"Fatura tutarı negatif: {invoice.Amount:N2}"));
            }

            if (invoice.PaidAmount < -epsilon)
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "ERROR",
                    Code: "INVOICE_PAID_NEGATIVE",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: $"Ödenen tutar negatif: {invoice.PaidAmount:N2}"));
            }

            if (invoice.PaidAmount > invoice.Amount + epsilon && invoice.Amount > 0)
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "ERROR",
                    Code: "INVOICE_PAID_EXCEEDS_AMOUNT",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: $"Ödenen tutar fatura tutarını aşıyor. Ödenen={invoice.PaidAmount:N2}, Tutar={invoice.Amount:N2}"));
            }

            var status = invoice.Status?.Trim().ToLowerInvariant() ?? string.Empty;
            if (status != "unpaid" && status != "paid" && status != "canceled")
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "ERROR",
                    Code: "INVOICE_STATUS_INVALID",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: $"Fatura durumu geçersiz: {invoice.Status}"));
            }

            if (status == "paid" && invoice.Amount > 0 && invoice.PaidAmount + epsilon < invoice.Amount)
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "ERROR",
                    Code: "INVOICE_STATUS_PAID_BUT_REMAINING",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: $"Fatura 'ödendi' ama kalan var. Ödenen={invoice.PaidAmount:N2}, Tutar={invoice.Amount:N2}"));
            }

            if (status == "unpaid" && invoice.Amount > 0 && invoice.PaidAmount + epsilon >= invoice.Amount)
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "WARN",
                    Code: "INVOICE_STATUS_UNPAID_BUT_FULLY_PAID",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: $"Fatura 'ödenmedi' ama ödeme tamamlanmış görünüyor. Ödenen={invoice.PaidAmount:N2}, Tutar={invoice.Amount:N2}"));
            }

            if (status == "canceled" && invoice.PaidAmount > epsilon)
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "WARN",
                    Code: "INVOICE_CANCELED_WITH_PAYMENTS",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: $"Fatura iptal ama ödeme var. Ödenen={invoice.PaidAmount:N2}"));
            }

            if (invoice.HasPdf && invoicePdfExists is not null && !invoicePdfExists(invoice))
            {
                issues.Add(new ConsistencyIssue(
                    Severity: "WARN",
                    Code: "INVOICE_PDF_MISSING",
                    Entity: "invoice",
                    EntityId: invoice.Id,
                    Message: "Fatura PDF dosyası kayıtlı ama dosya bulunamadı."));
            }
        }

        return new ConsistencyReport(issues);
    }
}

