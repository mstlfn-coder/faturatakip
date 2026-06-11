using System.Globalization;
using System.Windows;
using System.Windows.Media;
using FaturaTakip.App.Data.Dashboard;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.InvoiceTypes;
using FaturaTakip.App.Data.Payments;
using FaturaTakip.App.Data.Subscriptions;
using FaturaTakip.App.Infrastructure;

namespace FaturaTakip.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly InvoiceTypeRepository _invoiceTypeRepository;
    private readonly SubscriptionRepository _subscriptionRepository;
    private readonly InvoiceRepository _invoiceRepository;
    private readonly PaymentRepository _paymentRepository;
    private IReadOnlyList<InvoiceType> _invoiceTypes = Array.Empty<InvoiceType>();
    private InvoiceType? _selectedInvoiceType;

    public MainWindow(StartupStatus startupStatus)
    {
        InitializeComponent();

        _invoiceTypeRepository = new InvoiceTypeRepository(startupStatus.DatabasePath);
        _subscriptionRepository = new SubscriptionRepository(startupStatus.DatabasePath);
        _invoiceRepository = new InvoiceRepository(startupStatus.DatabasePath);
        _paymentRepository = new PaymentRepository(startupStatus.DatabasePath);
        SubscriptionsPanel.Initialize(startupStatus.DatabasePath);
        InvoicesPanel.Initialize(startupStatus.DatabasePath);
        ReportsPanel.Initialize(startupStatus.DatabasePath);
        ReportsPanel.UnreviewedInvoiceReviewRequested += ReportsPanel_UnreviewedInvoiceReviewRequested;
        ReportsPanel.OverdueInvoiceReviewRequested += ReportsPanel_OverdueInvoiceReviewRequested;
        ReportsPanel.MissingPdfInvoiceReviewRequested += ReportsPanel_MissingPdfInvoiceReviewRequested;
        ApplyStartupStatus(startupStatus);
        RefreshInvoiceTypes();
        RefreshDashboardSubscriptionCounts();
        RefreshDashboardMetrics();
        ShowDashboard();
    }

    private void ApplyStartupStatus(StartupStatus startupStatus)
    {
        InitializedAtText.Text = startupStatus.InitializedAt.ToLocalTime()
            .ToString("dd.MM.yyyy HH:mm", CultureInfo.GetCultureInfo("tr-TR"));
        DatabaseStateText.Text = startupStatus.DatabaseState;
        MigrationStateText.Text = startupStatus.MigrationState;
        RootPathText.Text = startupStatus.RootDirectory;
        DatabasePathText.Text = startupStatus.DatabasePath;
        DirectoryList.ItemsSource = startupStatus.Directories;
        MigrationList.ItemsSource = startupStatus.AppliedMigrations.Count == 0
            ? new[] { "Bekleyen migration yok." }
            : startupStatus.AppliedMigrations;
    }

    private void DashboardNavButton_Click(object sender, RoutedEventArgs e)
    {
        ShowDashboard();
    }

    private void InvoiceTypesNavButton_Click(object sender, RoutedEventArgs e)
    {
        ShowInvoiceTypes();
    }

    private void SubscriptionsNavButton_Click(object sender, RoutedEventArgs e)
    {
        ShowSubscriptions();
    }

    private void InvoicesNavButton_Click(object sender, RoutedEventArgs e)
    {
        ShowInvoices();
    }

    private void ReportsNavButton_Click(object sender, RoutedEventArgs e)
    {
        ShowReports();
    }

    private void BackupNavButton_Click(object sender, RoutedEventArgs e)
    {
        ShowBackup();
    }

    private void OpenUnpaidReportFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowReports();
        ReportsPanel.ShowUnpaidReport();
    }

    private void OpenOverdueReportFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowReports();
        ReportsPanel.ShowOverdueReport();
    }

    private void OpenUnreviewedInvoicesFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowInvoices();
        InvoicesPanel.ShowUnreviewedInvoices();
    }

    private void OpenMonthlyReportFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowReports();
        ReportsPanel.ShowMonthlyReport();
    }

    private void OpenDocumentHealthReportFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowReports();
        ReportsPanel.ShowDocumentHealthReport();
    }

    private void OpenInvoiceTypesFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowInvoiceTypes();
    }

    private void OpenSubscriptionsFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowSubscriptions();
    }

    private void OpenInvoicesFromDashboardButton_Click(object sender, RoutedEventArgs e)
    {
        ShowInvoices();
    }

    private void ReportsPanel_UnreviewedInvoiceReviewRequested(object? sender, Views.ReportsView.InvoiceReviewNavigationRequestEventArgs e)
    {
        ShowInvoices();
        InvoicesPanel.StartUnreviewedReviewMode(e.PreferredInvoiceId, e.ContextLabel);
    }

    private void ReportsPanel_OverdueInvoiceReviewRequested(object? sender, Views.ReportsView.InvoiceReviewNavigationRequestEventArgs e)
    {
        ShowInvoices();
        InvoicesPanel.StartOverdueReviewMode(e.PreferredInvoiceId, e.ContextLabel);
    }

    private void ReportsPanel_MissingPdfInvoiceReviewRequested(object? sender, Views.ReportsView.InvoiceReviewNavigationRequestEventArgs e)
    {
        ShowInvoices();
        InvoicesPanel.StartMissingPdfReviewMode(e.PreferredInvoiceId, e.ContextLabel);
    }

    private void ShowDashboard()
    {
        DashboardPanel.Visibility = Visibility.Visible;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        ReportsPanel.Visibility = Visibility.Collapsed;
        BackupPanel.Visibility = Visibility.Collapsed;
        DashboardNavButton.Foreground = Brushes.White;
        DashboardNavButton.FontWeight = FontWeights.SemiBold;
        InvoiceTypesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoiceTypesNavButton.FontWeight = FontWeights.Normal;
        SubscriptionsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        SubscriptionsNavButton.FontWeight = FontWeights.Normal;
        InvoicesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoicesNavButton.FontWeight = FontWeights.Normal;
        ReportsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        ReportsNavButton.FontWeight = FontWeights.Normal;
        BackupNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        BackupNavButton.FontWeight = FontWeights.Normal;
        RefreshDashboardSubscriptionCounts();
        RefreshDashboardMetrics();
    }

    private void ShowInvoiceTypes()
    {
        RefreshInvoiceTypes(_selectedInvoiceType?.Id);
        DashboardPanel.Visibility = Visibility.Collapsed;
        InvoiceTypesPanel.Visibility = Visibility.Visible;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        ReportsPanel.Visibility = Visibility.Collapsed;
        BackupPanel.Visibility = Visibility.Collapsed;
        DashboardNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        DashboardNavButton.FontWeight = FontWeights.Normal;
        InvoiceTypesNavButton.Foreground = Brushes.White;
        InvoiceTypesNavButton.FontWeight = FontWeights.SemiBold;
        SubscriptionsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        SubscriptionsNavButton.FontWeight = FontWeights.Normal;
        InvoicesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoicesNavButton.FontWeight = FontWeights.Normal;
        ReportsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        ReportsNavButton.FontWeight = FontWeights.Normal;
        BackupNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        BackupNavButton.FontWeight = FontWeights.Normal;
    }

    private void ShowSubscriptions()
    {
        SubscriptionsPanel.Refresh();
        DashboardPanel.Visibility = Visibility.Collapsed;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Visible;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        ReportsPanel.Visibility = Visibility.Collapsed;
        BackupPanel.Visibility = Visibility.Collapsed;
        DashboardNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        DashboardNavButton.FontWeight = FontWeights.Normal;
        InvoiceTypesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoiceTypesNavButton.FontWeight = FontWeights.Normal;
        SubscriptionsNavButton.Foreground = Brushes.White;
        SubscriptionsNavButton.FontWeight = FontWeights.SemiBold;
        InvoicesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoicesNavButton.FontWeight = FontWeights.Normal;
        ReportsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        ReportsNavButton.FontWeight = FontWeights.Normal;
        BackupNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        BackupNavButton.FontWeight = FontWeights.Normal;
    }

    private void ShowInvoices()
    {
        InvoicesPanel.Refresh();
        DashboardPanel.Visibility = Visibility.Collapsed;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Visible;
        ReportsPanel.Visibility = Visibility.Collapsed;
        BackupPanel.Visibility = Visibility.Collapsed;
        DashboardNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        DashboardNavButton.FontWeight = FontWeights.Normal;
        InvoiceTypesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoiceTypesNavButton.FontWeight = FontWeights.Normal;
        SubscriptionsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        SubscriptionsNavButton.FontWeight = FontWeights.Normal;
        InvoicesNavButton.Foreground = Brushes.White;
        InvoicesNavButton.FontWeight = FontWeights.SemiBold;
        ReportsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        ReportsNavButton.FontWeight = FontWeights.Normal;
        BackupNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        BackupNavButton.FontWeight = FontWeights.Normal;
    }

    private void ShowReports()
    {
        ReportsPanel.Refresh();
        DashboardPanel.Visibility = Visibility.Collapsed;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        ReportsPanel.Visibility = Visibility.Visible;
        BackupPanel.Visibility = Visibility.Collapsed;
        DashboardNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        DashboardNavButton.FontWeight = FontWeights.Normal;
        InvoiceTypesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoiceTypesNavButton.FontWeight = FontWeights.Normal;
        SubscriptionsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        SubscriptionsNavButton.FontWeight = FontWeights.Normal;
        InvoicesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoicesNavButton.FontWeight = FontWeights.Normal;
        ReportsNavButton.Foreground = Brushes.White;
        ReportsNavButton.FontWeight = FontWeights.SemiBold;
        BackupNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        BackupNavButton.FontWeight = FontWeights.Normal;
    }

    private void ShowBackup()
    {
        BackupPanel.Refresh();
        DashboardPanel.Visibility = Visibility.Collapsed;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        ReportsPanel.Visibility = Visibility.Collapsed;
        BackupPanel.Visibility = Visibility.Visible;

        DashboardNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        DashboardNavButton.FontWeight = FontWeights.Normal;
        InvoiceTypesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoiceTypesNavButton.FontWeight = FontWeights.Normal;
        SubscriptionsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        SubscriptionsNavButton.FontWeight = FontWeights.Normal;
        InvoicesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoicesNavButton.FontWeight = FontWeights.Normal;
        ReportsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        ReportsNavButton.FontWeight = FontWeights.Normal;
        BackupNavButton.Foreground = Brushes.White;
        BackupNavButton.FontWeight = FontWeights.SemiBold;
    }

    private void RefreshInvoiceTypes(long? selectedId = null)
    {
        _invoiceTypes = _invoiceTypeRepository.GetAll();
        InvoiceTypeGrid.ItemsSource = _invoiceTypes;

        var activeCount = _invoiceTypes.Count(item => item.IsActive);
        var passiveCount = _invoiceTypes.Count - activeCount;

        DashboardInvoiceTypeCountText.Text = _invoiceTypes.Count.ToString(CultureInfo.InvariantCulture);
        DashboardActiveInvoiceTypeCountText.Text = activeCount.ToString(CultureInfo.InvariantCulture);
        InvoiceTypeCountText.Text = _invoiceTypes.Count.ToString(CultureInfo.InvariantCulture);
        ActiveInvoiceTypeCountText.Text = activeCount.ToString(CultureInfo.InvariantCulture);
        PassiveInvoiceTypeCountText.Text = passiveCount.ToString(CultureInfo.InvariantCulture);

        var selected = selectedId is null
            ? null
            : _invoiceTypes.FirstOrDefault(item => item.Id == selectedId.Value);

        if (selected is null)
        {
            ClearInvoiceTypeForm();
            return;
        }

        InvoiceTypeGrid.SelectedItem = selected;
        ApplySelectedInvoiceType(selected);
    }

    private void RefreshDashboardSubscriptionCounts()
    {
        var activeSubscriptionCount = _subscriptionRepository.GetAll().Count(item => item.IsActive);
        DashboardActiveSubscriptionCountText.Text = activeSubscriptionCount.ToString(CultureInfo.InvariantCulture);
    }

    private void RefreshDashboardInvoiceCounts()
    {
        RefreshDashboardMetrics();
    }

    private void RefreshDashboardMetrics()
    {
        var invoices = _invoiceRepository.GetAll();
        var payments = _paymentRepository.GetAll();
        var summary = DashboardSummaryCalculator.Calculate(
            invoices,
            payments,
            DateTime.Today,
            invoice => _invoiceRepository.IsPdfMissing(invoice),
            payment => _paymentRepository.IsPdfMissing(payment));

        DashboardInvoiceCountText.Text = invoices.Count.ToString(CultureInfo.InvariantCulture);
        DashboardMonthlyInvoiceTotalText.Text = FormatMoney(summary.MonthlyInvoiceTotal);
        DashboardMonthlyInvoiceCountText.Text = $"{summary.MonthlyInvoiceCount} kayıt";
        DashboardMonthlyPaymentTotalText.Text = FormatMoney(summary.MonthlyPaymentTotal);
        DashboardMonthlyPaymentCountText.Text = $"{summary.MonthlyPaymentCount} kayıt";
        DashboardUnreviewedInvoiceCountText.Text = summary.UnreviewedInvoiceCount.ToString(CultureInfo.InvariantCulture);
        DashboardUnpaidInvoiceCountText.Text = summary.UnpaidInvoiceCount.ToString(CultureInfo.InvariantCulture);
        DashboardUnpaidRemainingText.Text = $"Kalan {FormatMoney(summary.UnpaidRemainingTotal)}";
        DashboardOverdueInvoiceCountText.Text = summary.OverdueInvoiceCount.ToString(CultureInfo.InvariantCulture);
        DashboardOverdueRemainingText.Text = $"Kalan {FormatMoney(summary.OverdueRemainingTotal)}";
        DashboardMissingInvoicePdfCountText.Text = summary.MissingInvoicePdfCount.ToString(CultureInfo.InvariantCulture);
        DashboardMissingPaymentPdfCountText.Text = summary.MissingPaymentPdfCount.ToString(CultureInfo.InvariantCulture);
    }

    private void SubscriptionsPanel_SubscriptionsChanged(object sender, EventArgs e)
    {
        RefreshDashboardSubscriptionCounts();
        InvoicesPanel.Refresh();
    }

    private void InvoicesPanel_InvoicesChanged(object sender, EventArgs e)
    {
        RefreshDashboardInvoiceCounts();
    }

    private static string FormatMoney(decimal value)
    {
        return value.ToString("N2", CultureInfo.GetCultureInfo("tr-TR"));
    }

    private void InvoiceTypeGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (InvoiceTypeGrid.SelectedItem is InvoiceType invoiceType)
        {
            ApplySelectedInvoiceType(invoiceType);
        }
    }

    private void ApplySelectedInvoiceType(InvoiceType invoiceType)
    {
        _selectedInvoiceType = invoiceType;
        InvoiceTypeFormTitleText.Text = "Fatura Türünü Düzenle";
        InvoiceTypeNameInput.Text = invoiceType.Name;
        DefaultUsageUnitInput.Text = invoiceType.DefaultUsageUnit;
        InvoiceTypeDescriptionInput.Text = invoiceType.Description;
        InvoiceTypeIsActiveInput.IsChecked = invoiceType.IsActive;
        ToggleInvoiceTypeButton.IsEnabled = true;
        ToggleInvoiceTypeButton.Content = invoiceType.IsActive ? "Pasife Al" : "Aktif Yap";
        SetInvoiceTypeStatus($"Seçili kayıt: {invoiceType.Name}", isError: false);
    }

    private void NewInvoiceTypeButton_Click(object sender, RoutedEventArgs e)
    {
        ClearInvoiceTypeForm();
        InvoiceTypeNameInput.Focus();
    }

    private void SaveInvoiceTypeButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var input = ReadInvoiceTypeInput();
            var saved = _selectedInvoiceType is null
                ? _invoiceTypeRepository.Add(input)
                : _invoiceTypeRepository.Update(_selectedInvoiceType.Id, input);

            RefreshInvoiceTypes(saved.Id);
            SetInvoiceTypeStatus("Fatura türü kaydedildi.", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
        {
            SetInvoiceTypeStatus(exception.Message, isError: true);
        }
    }

    private void ToggleInvoiceTypeButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedInvoiceType is null)
        {
            SetInvoiceTypeStatus("Önce bir fatura türü seçin.", isError: true);
            return;
        }

        try
        {
            var nextState = !_selectedInvoiceType.IsActive;
            _invoiceTypeRepository.SetActive(_selectedInvoiceType.Id, nextState);
            RefreshInvoiceTypes(_selectedInvoiceType.Id);
            SetInvoiceTypeStatus(nextState ? "Fatura türü aktif yapıldı." : "Fatura türü pasife alındı.", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
        {
            SetInvoiceTypeStatus(exception.Message, isError: true);
        }
    }

    private InvoiceTypeInput ReadInvoiceTypeInput()
    {
        return new InvoiceTypeInput(
            InvoiceTypeNameInput.Text,
            InvoiceTypeDescriptionInput.Text,
            DefaultUsageUnitInput.Text,
            InvoiceTypeIsActiveInput.IsChecked == true);
    }

    private void ClearInvoiceTypeForm()
    {
        _selectedInvoiceType = null;
        InvoiceTypeGrid.SelectedItem = null;
        InvoiceTypeFormTitleText.Text = "Yeni Fatura Türü";
        InvoiceTypeNameInput.Text = string.Empty;
        DefaultUsageUnitInput.Text = string.Empty;
        InvoiceTypeDescriptionInput.Text = string.Empty;
        InvoiceTypeIsActiveInput.IsChecked = true;
        ToggleInvoiceTypeButton.IsEnabled = false;
        ToggleInvoiceTypeButton.Content = "Pasife Al";
        SetInvoiceTypeStatus("Yeni kayıt için alanları doldurun.", isError: false);
    }

    private void SetInvoiceTypeStatus(string message, bool isError)
    {
        InvoiceTypeStatusText.Text = message;
        InvoiceTypeStatusText.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(185, 28, 28))
            : new SolidColorBrush(Color.FromRgb(95, 107, 122));
    }
}
