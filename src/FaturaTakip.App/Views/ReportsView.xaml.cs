using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Reports;

namespace FaturaTakip.App.Views;

public partial class ReportsView : UserControl
{
    private static readonly CultureInfo TurkishCulture = CultureInfo.GetCultureInfo("tr-TR");
    private InvoiceRepository? _invoiceRepository;
    private ActionableInvoiceReport _report = ActionableInvoiceReport.Empty;
    private ReportTab _activeTab = ReportTab.Unpaid;
    private bool _isInitialized;

    public ReportsView()
    {
        InitializeComponent();
    }

    public void Initialize(string databasePath)
    {
        _invoiceRepository = new InvoiceRepository(databasePath);
        _isInitialized = true;
        Refresh();
    }

    public void Refresh()
    {
        if (!_isInitialized || _invoiceRepository is null)
        {
            return;
        }

        var invoices = _invoiceRepository.GetAll();
        _report = ActionableInvoiceReportCalculator.Calculate(
            invoices,
            DateTime.Today,
            upcomingDays: 7,
            invoice => _invoiceRepository.IsPdfMissing(invoice));

        UnpaidCountText.Text = _report.Unpaid.Count.ToString(CultureInfo.InvariantCulture);
        UnpaidRemainingText.Text = $"Kalan {FormatMoney(_report.UnpaidRemainingTotal)}";
        OverdueCountText.Text = _report.Overdue.Count.ToString(CultureInfo.InvariantCulture);
        OverdueRemainingText.Text = $"Kalan {FormatMoney(_report.OverdueRemainingTotal)}";
        UpcomingCountText.Text = _report.Upcoming.Count.ToString(CultureInfo.InvariantCulture);
        UpcomingRemainingText.Text = $"Kalan {FormatMoney(_report.UpcomingRemainingTotal)}";

        ApplyTab(_activeTab);
    }

    private void UnpaidTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.Unpaid);
    }

    private void OverdueTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.Overdue);
    }

    private void UpcomingTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.Upcoming);
    }

    private void ApplyTab(ReportTab tab)
    {
        _activeTab = tab;

        SetTabActive(UnpaidTabButton, tab == ReportTab.Unpaid);
        SetTabActive(OverdueTabButton, tab == ReportTab.Overdue);
        SetTabActive(UpcomingTabButton, tab == ReportTab.Upcoming);

        var items = tab switch
        {
            ReportTab.Overdue => _report.Overdue,
            ReportTab.Upcoming => _report.Upcoming,
            _ => _report.Unpaid,
        };

        ReportGrid.ItemsSource = items.Select(ToRow).ToList();
    }

    private static void SetTabActive(Button button, bool isActive)
    {
        button.Foreground = isActive ? Brushes.White : new SolidColorBrush(Color.FromRgb(51, 65, 85));
        button.Background = isActive ? new SolidColorBrush(Color.FromRgb(37, 99, 235)) : new SolidColorBrush(Color.FromRgb(231, 238, 246));
        button.BorderThickness = isActive ? new Thickness(0) : new Thickness(1);
    }

    private static ReportRow ToRow(ActionableInvoice invoice)
    {
        return new ReportRow(
            invoice.Invoice.Period,
            invoice.Invoice.InvoiceTypeName,
            invoice.Invoice.SubscriptionName,
            invoice.Invoice.InstitutionName,
            invoice.Invoice.InvoiceNo,
            invoice.Invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
            invoice.Invoice.Amount.ToString("N2", TurkishCulture),
            invoice.Invoice.PaidAmount.ToString("N2", TurkishCulture),
            invoice.Invoice.RemainingAmount.ToString("N2", TurkishCulture),
            invoice.PdfState);
    }

    private static string FormatMoney(decimal value)
    {
        return value.ToString("N2", TurkishCulture);
    }

    private enum ReportTab
    {
        Unpaid,
        Overdue,
        Upcoming,
    }

    private sealed record ReportRow(
        string Period,
        string InvoiceTypeName,
        string SubscriptionName,
        string InstitutionName,
        string InvoiceNo,
        string DueDateText,
        string AmountText,
        string PaidAmountText,
        string RemainingAmountText,
        string PdfState);
}

