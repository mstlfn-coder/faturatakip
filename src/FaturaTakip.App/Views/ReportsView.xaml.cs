using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.InvoiceTypes;
using FaturaTakip.App.Data.Reports;
using FaturaTakip.App.Data.Subscriptions;

namespace FaturaTakip.App.Views;

public partial class ReportsView : UserControl
{
    private static readonly CultureInfo TurkishCulture = CultureInfo.GetCultureInfo("tr-TR");
    private InvoiceRepository? _invoiceRepository;
    private InvoiceTypeRepository? _invoiceTypeRepository;
    private SubscriptionRepository? _subscriptionRepository;
    private ActionableInvoiceReport _report = ActionableInvoiceReport.Empty;
    private MonthlyInvoiceReport _monthlyReport = MonthlyInvoiceReport.Empty;
    private SubscriptionMonthlyComparison _subscriptionComparison = SubscriptionMonthlyComparison.Empty;
    private ReportTab _activeTab = ReportTab.Unpaid;
    private bool _isInitialized;
    private bool _isRefreshingMonthlyFilters;
    private bool _isRefreshingSubscriptionFilters;

    public ReportsView()
    {
        InitializeComponent();
    }

    public void Initialize(string databasePath)
    {
        _invoiceRepository = new InvoiceRepository(databasePath);
        _invoiceTypeRepository = new InvoiceTypeRepository(databasePath);
        _subscriptionRepository = new SubscriptionRepository(databasePath);
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

        EnsureMonthlyFiltersInitialized(invoices);
        _monthlyReport = MonthlyInvoiceReportCalculator.Calculate(
            invoices,
            GetSelectedYear(),
            GetSelectedMonth(),
            GetSelectedInvoiceTypeId(),
            DateTime.Today,
            invoice => _invoiceRepository.IsPdfMissing(invoice));

        EnsureSubscriptionFiltersInitialized(invoices);
        var selectedSubscriptionId = GetSelectedSubscriptionId();
        _subscriptionComparison = selectedSubscriptionId is null
            ? SubscriptionMonthlyComparison.Empty
            : SubscriptionMonthlyComparisonCalculator.Calculate(
                invoices,
                selectedSubscriptionId.Value,
                GetSelectedSubscriptionYear(),
                GetSelectedSubscriptionMonth(),
                DateTime.Today,
                invoice => _invoiceRepository.IsPdfMissing(invoice));

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

    private void MonthlyTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.Monthly);
    }

    private void SubscriptionTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.Subscription);
    }

    private void MonthlyFilter_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (_isRefreshingMonthlyFilters)
        {
            return;
        }

        if (_invoiceRepository is null)
        {
            return;
        }

        var invoices = _invoiceRepository.GetAll();
        _monthlyReport = MonthlyInvoiceReportCalculator.Calculate(
            invoices,
            GetSelectedYear(),
            GetSelectedMonth(),
            GetSelectedInvoiceTypeId(),
            DateTime.Today,
            invoice => _invoiceRepository.IsPdfMissing(invoice));

        if (_activeTab == ReportTab.Monthly)
        {
            ApplyTab(ReportTab.Monthly);
        }
    }

    private void SubscriptionFilter_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (_isRefreshingSubscriptionFilters)
        {
            return;
        }

        if (_invoiceRepository is null)
        {
            return;
        }

        var selectedSubscriptionId = GetSelectedSubscriptionId();
        if (selectedSubscriptionId is null)
        {
            _subscriptionComparison = SubscriptionMonthlyComparison.Empty;
        }
        else
        {
            var invoices = _invoiceRepository.GetAll();
            _subscriptionComparison = SubscriptionMonthlyComparisonCalculator.Calculate(
                invoices,
                selectedSubscriptionId.Value,
                GetSelectedSubscriptionYear(),
                GetSelectedSubscriptionMonth(),
                DateTime.Today,
                invoice => _invoiceRepository.IsPdfMissing(invoice));
        }

        if (_activeTab == ReportTab.Subscription)
        {
            ApplyTab(ReportTab.Subscription);
        }
    }

    private void ApplyTab(ReportTab tab)
    {
        _activeTab = tab;

        SetTabActive(UnpaidTabButton, tab == ReportTab.Unpaid);
        SetTabActive(OverdueTabButton, tab == ReportTab.Overdue);
        SetTabActive(UpcomingTabButton, tab == ReportTab.Upcoming);
        SetTabActive(MonthlyTabButton, tab == ReportTab.Monthly);
        SetTabActive(SubscriptionTabButton, tab == ReportTab.Subscription);

        MonthlyFilterPanel.Visibility = tab == ReportTab.Monthly ? Visibility.Visible : Visibility.Collapsed;
        SubscriptionFilterPanel.Visibility = tab == ReportTab.Subscription ? Visibility.Visible : Visibility.Collapsed;

        var items = tab switch
        {
            ReportTab.Overdue => _report.Overdue,
            ReportTab.Upcoming => _report.Upcoming,
            ReportTab.Monthly => Array.Empty<ActionableInvoice>(),
            ReportTab.Subscription => Array.Empty<ActionableInvoice>(),
            _ => _report.Unpaid,
        };

        if (tab == ReportTab.Monthly)
        {
            ApplyMonthlyTiles();
            ReportGrid.ItemsSource = _monthlyReport.Rows.Select(ToMonthlyRow).ToList();
        }
        else if (tab == ReportTab.Subscription)
        {
            ApplySubscriptionTiles();
            ReportGrid.ItemsSource = _subscriptionComparison.Current.Rows.Select(ToMonthlyRow).ToList();
        }
        else
        {
            ApplyActionableTiles();
            ReportGrid.ItemsSource = items.Select(ToActionableRow).ToList();
        }
    }

    private static void SetTabActive(Button button, bool isActive)
    {
        button.Foreground = isActive ? Brushes.White : new SolidColorBrush(Color.FromRgb(51, 65, 85));
        button.Background = isActive ? new SolidColorBrush(Color.FromRgb(37, 99, 235)) : new SolidColorBrush(Color.FromRgb(231, 238, 246));
        button.BorderThickness = isActive ? new Thickness(0) : new Thickness(1);
    }

    private void ApplyActionableTiles()
    {
        Tile1LabelText.Text = "Ödenmemiş";
        Tile1ValueText.Text = _report.Unpaid.Count.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Kalan {FormatMoney(_report.UnpaidRemainingTotal)}";

        Tile2LabelText.Text = "Gecikmiş";
        Tile2ValueText.Text = _report.Overdue.Count.ToString(CultureInfo.InvariantCulture);
        Tile2DetailText.Text = $"Kalan {FormatMoney(_report.OverdueRemainingTotal)}";

        Tile3LabelText.Text = "Yaklaşan (7 gün)";
        Tile3ValueText.Text = _report.Upcoming.Count.ToString(CultureInfo.InvariantCulture);
        Tile3DetailText.Text = $"Kalan {FormatMoney(_report.UpcomingRemainingTotal)}";

        MonthlyFilterHintText.Text = string.Empty;
    }

    private void ApplyMonthlyTiles()
    {
        Tile1LabelText.Text = "Toplam Fatura";
        Tile1ValueText.Text = _monthlyReport.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Toplam {FormatMoney(_monthlyReport.TotalAmount)}";

        Tile2LabelText.Text = "Ödenen";
        Tile2ValueText.Text = FormatMoney(_monthlyReport.PaidTotal);
        Tile2DetailText.Text = $"PDF eksik {_monthlyReport.MissingPdfCount}";

        Tile3LabelText.Text = "Kalan";
        Tile3ValueText.Text = FormatMoney(_monthlyReport.RemainingTotal);
        Tile3DetailText.Text = $"Ödenmemiş {_monthlyReport.UnpaidInvoiceCount}, Gecikmiş {_monthlyReport.OverdueInvoiceCount}";

        MonthlyFilterHintText.Text = $"{_monthlyReport.Year:D4}/{_monthlyReport.Month:D2} dönemi listeleniyor";
        SubscriptionHintText.Text = string.Empty;
    }

    private void ApplySubscriptionTiles()
    {
        var current = _subscriptionComparison.Current;
        var previous = _subscriptionComparison.Previous;

        Tile1LabelText.Text = "Toplam Fatura";
        Tile1ValueText.Text = current.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Toplam {FormatMoney(current.TotalAmount)} ({FormatDelta(_subscriptionComparison.TotalAmountDelta)})";

        Tile2LabelText.Text = "Ödenen";
        Tile2ValueText.Text = FormatMoney(current.PaidTotal);
        Tile2DetailText.Text = $"Önceki ay {FormatMoney(previous.PaidTotal)} ({FormatDelta(_subscriptionComparison.PaidDelta)})";

        Tile3LabelText.Text = "Kalan";
        Tile3ValueText.Text = FormatMoney(current.RemainingTotal);
        Tile3DetailText.Text = $"Önceki ay {FormatMoney(previous.RemainingTotal)} ({FormatDelta(_subscriptionComparison.RemainingDelta)})";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = $"{current.Year:D4}/{current.Month:D2} dönemi (önceki ay karşılaştırmalı)";
    }

    private static string FormatDelta(decimal value)
    {
        var formatted = FormatMoney(Math.Abs(value));
        return value >= 0 ? $"+{formatted}" : $"-{formatted}";
    }

    private static ReportRow ToActionableRow(ActionableInvoice invoice)
    {
        var statusText = invoice.Invoice.State;
        return new ReportRow(
            invoice.Invoice.Period,
            invoice.Invoice.InvoiceTypeName,
            invoice.Invoice.SubscriptionName,
            invoice.Invoice.InstitutionName,
            statusText,
            invoice.Invoice.InvoiceNo,
            invoice.Invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
            invoice.Invoice.Amount.ToString("N2", TurkishCulture),
            invoice.Invoice.PaidAmount.ToString("N2", TurkishCulture),
            invoice.Invoice.RemainingAmount.ToString("N2", TurkishCulture),
            invoice.PdfState);
    }

    private static ReportRow ToMonthlyRow(MonthlyInvoiceRow row)
    {
        var invoice = row.Invoice;
        return new ReportRow(
            invoice.Period,
            invoice.InvoiceTypeName,
            invoice.SubscriptionName,
            invoice.InstitutionName,
            invoice.State,
            invoice.InvoiceNo,
            invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
            invoice.Amount.ToString("N2", TurkishCulture),
            invoice.PaidAmount.ToString("N2", TurkishCulture),
            invoice.RemainingAmount.ToString("N2", TurkishCulture),
            row.PdfState);
    }

    private static string FormatMoney(decimal value)
    {
        return value.ToString("N2", TurkishCulture);
    }

    private void EnsureMonthlyFiltersInitialized(IReadOnlyList<Invoice> invoices)
    {
        _isRefreshingMonthlyFilters = true;
        try
        {
            if (MonthlyInvoiceTypeInput.Items.Count == 0)
            {
                var types = _invoiceTypeRepository?.GetAll() ?? Array.Empty<InvoiceType>();
                var typeOptions = new List<InvoiceTypeOption> { new("Tüm Türler", null) };
                typeOptions.AddRange(types.Select(item => new InvoiceTypeOption(item.Name, item.Id)));
                MonthlyInvoiceTypeInput.ItemsSource = typeOptions;
                MonthlyInvoiceTypeInput.DisplayMemberPath = nameof(InvoiceTypeOption.Label);
                MonthlyInvoiceTypeInput.SelectedItem = typeOptions[0];
            }

            var years = invoices
                .Select(item => item.InvoiceYear)
                .Distinct()
                .OrderByDescending(item => item)
                .ToList();

            if (years.Count == 0)
            {
                years.Add(DateTime.Today.Year);
            }

            if (MonthlyYearInput.Items.Count == 0)
            {
                MonthlyYearInput.ItemsSource = years;
                MonthlyYearInput.SelectedItem = years.Contains(DateTime.Today.Year) ? DateTime.Today.Year : years[0];
            }

            if (MonthlyMonthInput.Items.Count == 0)
            {
                MonthlyMonthInput.ItemsSource = Enumerable.Range(1, 12)
                    .Select(month => new MonthOption(month, TurkishCulture.DateTimeFormat.GetMonthName(month)))
                    .ToList();
                MonthlyMonthInput.DisplayMemberPath = nameof(MonthOption.Label);
                MonthlyMonthInput.SelectedValuePath = nameof(MonthOption.Value);
                MonthlyMonthInput.SelectedValue = DateTime.Today.Month;
            }
        }
        finally
        {
            _isRefreshingMonthlyFilters = false;
        }
    }

    private void EnsureSubscriptionFiltersInitialized(IReadOnlyList<Invoice> invoices)
    {
        _isRefreshingSubscriptionFilters = true;
        try
        {
            if (SubscriptionInput.Items.Count == 0)
            {
                var subscriptions = _subscriptionRepository?.GetAll() ?? Array.Empty<Subscription>();
                var options = subscriptions
                    .OrderByDescending(item => item.IsActive)
                    .ThenBy(item => item.InvoiceTypeName, StringComparer.CurrentCultureIgnoreCase)
                    .ThenBy(item => item.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
                    .Select(item => new SubscriptionOption($"{item.InvoiceTypeName} - {item.SubscriptionName}", item.Id))
                    .ToList();

                SubscriptionInput.ItemsSource = options;
                SubscriptionInput.DisplayMemberPath = nameof(SubscriptionOption.Label);
                SubscriptionInput.SelectedItem = options.FirstOrDefault();
            }

            if (SubscriptionYearInput.Items.Count == 0)
            {
                var years = invoices
                    .Select(item => item.InvoiceYear)
                    .Distinct()
                    .OrderByDescending(item => item)
                    .ToList();
                if (years.Count == 0)
                {
                    years.Add(DateTime.Today.Year);
                }

                SubscriptionYearInput.ItemsSource = years;
                SubscriptionYearInput.SelectedItem = years.Contains(DateTime.Today.Year) ? DateTime.Today.Year : years[0];
            }

            if (SubscriptionMonthInput.Items.Count == 0)
            {
                SubscriptionMonthInput.ItemsSource = Enumerable.Range(1, 12)
                    .Select(month => new MonthOption(month, TurkishCulture.DateTimeFormat.GetMonthName(month)))
                    .ToList();
                SubscriptionMonthInput.DisplayMemberPath = nameof(MonthOption.Label);
                SubscriptionMonthInput.SelectedValuePath = nameof(MonthOption.Value);
                SubscriptionMonthInput.SelectedValue = DateTime.Today.Month;
            }
        }
        finally
        {
            _isRefreshingSubscriptionFilters = false;
        }
    }

    private long? GetSelectedInvoiceTypeId()
    {
        if (MonthlyInvoiceTypeInput.SelectedItem is InvoiceTypeOption option)
        {
            return option.InvoiceTypeId;
        }

        return null;
    }

    private long? GetSelectedSubscriptionId()
    {
        if (SubscriptionInput.SelectedItem is SubscriptionOption option)
        {
            return option.SubscriptionId;
        }

        return null;
    }

    private int GetSelectedSubscriptionYear()
    {
        if (SubscriptionYearInput.SelectedItem is int year)
        {
            return year;
        }

        return DateTime.Today.Year;
    }

    private int GetSelectedSubscriptionMonth()
    {
        if (SubscriptionMonthInput.SelectedValue is int month)
        {
            return month;
        }

        if (SubscriptionMonthInput.SelectedItem is MonthOption option)
        {
            return option.Value;
        }

        return DateTime.Today.Month;
    }

    private int GetSelectedYear()
    {
        if (MonthlyYearInput.SelectedItem is int year)
        {
            return year;
        }

        return DateTime.Today.Year;
    }

    private int GetSelectedMonth()
    {
        if (MonthlyMonthInput.SelectedValue is int month)
        {
            return month;
        }

        if (MonthlyMonthInput.SelectedItem is MonthOption option)
        {
            return option.Value;
        }

        return DateTime.Today.Month;
    }

    private enum ReportTab
    {
        Unpaid,
        Overdue,
        Upcoming,
        Monthly,
        Subscription,
    }

    private sealed record ReportRow(
        string Period,
        string InvoiceTypeName,
        string SubscriptionName,
        string InstitutionName,
        string StatusText,
        string InvoiceNo,
        string DueDateText,
        string AmountText,
        string PaidAmountText,
        string RemainingAmountText,
        string PdfState);

    private sealed record MonthOption(int Value, string Label);

    private sealed record InvoiceTypeOption(string Label, long? InvoiceTypeId);

    private sealed record SubscriptionOption(string Label, long SubscriptionId);
}

