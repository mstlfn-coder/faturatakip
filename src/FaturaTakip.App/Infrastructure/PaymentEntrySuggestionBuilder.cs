using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;

namespace FaturaTakip.App.Infrastructure;

public static class PaymentEntrySuggestionBuilder
{
    public static PaymentEntrySuggestion CreateDefault(Invoice invoice, DateTime today)
    {
        return new PaymentEntrySuggestion(
            PaymentDate: today.Date,
            Amount: invoice.RemainingAmount,
            Description: string.Empty);
    }

    public static PaymentEntrySuggestion CreateFromSelectedPayment(
        Invoice invoice,
        Payment payment,
        DateTime today)
    {
        return new PaymentEntrySuggestion(
            PaymentDate: today.Date,
            Amount: Math.Min(payment.Amount, invoice.RemainingAmount),
            Description: payment.Description?.Trim() ?? string.Empty);
    }

    public static PaymentEntrySuggestion CreateFromRecentPayment(
        Invoice invoice,
        IEnumerable<Payment> payments,
        DateTime today)
    {
        var recentDescription = payments
            .OrderByDescending(item => item.PaymentDate)
            .ThenByDescending(item => item.Id)
            .Select(item => item.Description?.Trim())
            .FirstOrDefault(item => !string.IsNullOrWhiteSpace(item))
            ?? string.Empty;

        return new PaymentEntrySuggestion(
            PaymentDate: today.Date,
            Amount: invoice.RemainingAmount,
            Description: recentDescription);
    }
}

public sealed record PaymentEntrySuggestion(
    DateTime PaymentDate,
    decimal Amount,
    string Description);
