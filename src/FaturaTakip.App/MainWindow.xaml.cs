using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
    private const string DefaultPaymentsHeaderHint = "Soldaki yol calisma ekranina, sagdaki yol rapor ve kontrol ekranlarina goturur.";
    private const string DefaultPaymentsRouteBadge = "SON YOL: GENEL";
    private const string DefaultPaymentsFlowContext = "Aktif kolon secildiginde ilgili akis burada kisaca ozetlenir.";
    private readonly InvoiceTypeRepository _invoiceTypeRepository;
    private readonly SubscriptionRepository _subscriptionRepository;
    private readonly InvoiceRepository _invoiceRepository;
    private readonly PaymentRepository _paymentRepository;
    private IReadOnlyList<InvoiceType> _invoiceTypes = Array.Empty<InvoiceType>();
    private InvoiceType? _selectedInvoiceType;
    private string _activePaymentsHeaderHint = DefaultPaymentsHeaderHint;
    private string _activePaymentsRouteBadge = DefaultPaymentsRouteBadge;
    private string? _activePaymentsRouteKey;
    private string _activePaymentsFlowContext = DefaultPaymentsFlowContext;
    private string _paymentsQueueFilterKey = "all";
    private string _paymentsRecentFilterKey = "all";

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
        ReportsPanel.AuditEntityNavigationRequested += ReportsPanel_AuditEntityNavigationRequested;
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

    private void PaymentsNavButton_Click(object sender, RoutedEventArgs e)
    {
        ShowPayments();
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

    private void OpenInvoicesFromPaymentsButton_Click(object sender, RoutedEventArgs e)
    {
        PersistPaymentsPanelContext(
            "Fatura listesi acilir; genel kayit tarama ve secim icin kullanilir.",
            "SON YOL: LISTE",
            "#E2E8F0",
            "#CBD5E1",
            "#475569",
            routeKey: null);
        ShowInvoices();
    }

    private void OpenInvoicePaymentWorkspaceButton_Click(object sender, RoutedEventArgs e)
    {
        PersistPaymentsPanelContext(
            "Odeme calisma alani acilir; odenmemis kayitlar icin odeme girisine gecilir.",
            "SON YOL: ISLEM",
            "#DCFCE7",
            "#86EFAC",
            "#166534",
            routeKey: "workspace");
        ShowInvoices();
        InvoicesPanel.StartPaymentWorkspace();
    }

    private void OpenReportsFromPaymentsButton_Click(object sender, RoutedEventArgs e)
    {
        PersistPaymentsPanelContext(
            "Rapor merkezi acilir; aylik, evrak ve acik bakiye kontrolleri buradan izlenir.",
            "SON YOL: RAPOR",
            "#DBEAFE",
            "#93C5FD",
            "#1D4ED8",
            routeKey: null);
        ShowReports();
    }

    private void OpenMonthlyReportFromPaymentsButton_Click(object sender, RoutedEventArgs e)
    {
        PersistPaymentsPanelContext(
            "Aylik odeme raporu acilir; bu ay tahsil edilen toplam ve kayit sayisi listelenir.",
            "SON YOL: AYLIK",
            "#DBEAFE",
            "#93C5FD",
            "#1D4ED8",
            routeKey: "monthly");
        ShowReports();
        ReportsPanel.ShowMonthlyReport();
    }

    private void OpenDocumentHealthReportFromPaymentsButton_Click(object sender, RoutedEventArgs e)
    {
        PersistPaymentsPanelContext(
            "Evrak kontrol raporu acilir; eksik odeme PDF kayitlari buradan ayiklanir.",
            "SON YOL: EVRAK",
            "#F0F9FF",
            "#BAE6FD",
            "#0369A1",
            routeKey: "document");
        ShowReports();
        ReportsPanel.ShowDocumentHealthReport();
    }

    private void OpenUnpaidReportFromPaymentsButton_Click(object sender, RoutedEventArgs e)
    {
        PersistPaymentsPanelContext(
            "Odenmemisler raporu acilir; acik bakiye ve bekleyen faturalar burada izlenir.",
            "SON YOL: BAKIYE",
            "#FEF3C7",
            "#FCD34D",
            "#B45309",
            routeKey: "unpaid");
        ShowReports();
        ReportsPanel.ShowUnpaidReport();
    }

    private void OpenPaymentsQueueInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement { Tag: long invoiceId })
        {
            return;
        }

        PersistPaymentsPanelContext(
            "Odeme kuyrugundan secilen acik fatura odeme alaninda hedefli olarak acildi.",
            "SON YOL: KUYRUK",
            "#DCFCE7",
            "#86EFAC",
            "#166534",
            routeKey: "workspace");
        ShowInvoices();
        InvoicesPanel.StartPaymentWorkspaceForInvoice(invoiceId, preferUnpaidFilter: true);
    }

    private void OpenRecentPaymentInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement { Tag: long invoiceId })
        {
            return;
        }

        PersistPaymentsPanelContext(
            "Son odeme kaydinin ait oldugu fatura odeme alaninda yeniden acildi.",
            "SON YOL: SON ODEME",
            "#DBEAFE",
            "#93C5FD",
            "#1D4ED8",
            routeKey: "workspace");
        ShowInvoices();
        InvoicesPanel.StartPaymentWorkspaceForInvoice(invoiceId, preferUnpaidFilter: false);
    }

    private void OpenNextQueueInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (PaymentsOpenNextQueueButton.Tag is not long invoiceId)
        {
            return;
        }

        PersistPaymentsPanelContext(
            "Oncelikli odeme kuyrugu kaydi odeme alaninda dogrudan acildi.",
            "SON YOL: ONCELIK",
            "#DCFCE7",
            "#86EFAC",
            "#166534",
            routeKey: "workspace");
        ShowInvoices();
        InvoicesPanel.StartPaymentWorkspaceForInvoice(invoiceId, preferUnpaidFilter: true);
    }

    private void OpenLatestPaymentFromPaymentsButton_Click(object sender, RoutedEventArgs e)
    {
        if (PaymentsOpenLatestPaymentButton.Tag is not long invoiceId)
        {
            return;
        }

        PersistPaymentsPanelContext(
            "Son odeme kaydinin ait oldugu fatura hizli aksiyon ile yeniden acildi.",
            "SON YOL: HIZLI ODEME",
            "#DBEAFE",
            "#93C5FD",
            "#1D4ED8",
            routeKey: "workspace");
        ShowInvoices();
        InvoicesPanel.StartPaymentWorkspaceForInvoice(invoiceId, preferUnpaidFilter: false);
    }

    private void OpenFeaturedPaymentsInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (PaymentsOpenFeaturedButton.Tag is not long invoiceId)
        {
            return;
        }

        var preferUnpaidFilter = string.Equals(
            Convert.ToString(PaymentsOpenFeaturedButton.CommandParameter, CultureInfo.InvariantCulture),
            "queue",
            StringComparison.OrdinalIgnoreCase);

        PersistPaymentsPanelContext(
            preferUnpaidFilter
                ? "One cikan odeme kuyrugu kaydi odeme alaninda acildi."
                : "One cikan son odeme kaydi kendi faturasinda yeniden acildi.",
            preferUnpaidFilter ? "SON YOL: ODAK" : "SON YOL: SON ODAK",
            preferUnpaidFilter ? "#DCFCE7" : "#DBEAFE",
            preferUnpaidFilter ? "#86EFAC" : "#93C5FD",
            preferUnpaidFilter ? "#166534" : "#1D4ED8",
            routeKey: "workspace");
        ShowInvoices();
        InvoicesPanel.StartPaymentWorkspaceForInvoice(invoiceId, preferUnpaidFilter);
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

    private void ReportsPanel_AuditEntityNavigationRequested(object? sender, Views.ReportsView.AuditEntityNavigationRequestEventArgs e)
    {
        switch (e.Target)
        {
            case AuditLogNavigationResolver.NavigationTarget.Invoice:
                ShowInvoices();
                InvoicesPanel.FocusInvoiceFromAudit(e.RecordId);
                break;
            case AuditLogNavigationResolver.NavigationTarget.InvoicePayment:
                ShowInvoices();
                InvoicesPanel.StartPaymentWorkspaceForInvoice(e.RecordId, preferUnpaidFilter: false);
                break;
            case AuditLogNavigationResolver.NavigationTarget.Subscription:
                ShowSubscriptions();
                SubscriptionsPanel.FocusSubscriptionFromAudit(e.RecordId);
                break;
        }
    }

    private void ShowDashboard()
    {
        DashboardPanel.Visibility = Visibility.Visible;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        PaymentsPanel.Visibility = Visibility.Collapsed;
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
        PaymentsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        PaymentsNavButton.FontWeight = FontWeights.Normal;
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
        PaymentsPanel.Visibility = Visibility.Collapsed;
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
        PaymentsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        PaymentsNavButton.FontWeight = FontWeights.Normal;
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
        PaymentsPanel.Visibility = Visibility.Collapsed;
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
        PaymentsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        PaymentsNavButton.FontWeight = FontWeights.Normal;
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
        PaymentsPanel.Visibility = Visibility.Collapsed;
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
        PaymentsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        PaymentsNavButton.FontWeight = FontWeights.Normal;
        ReportsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        ReportsNavButton.FontWeight = FontWeights.Normal;
        BackupNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        BackupNavButton.FontWeight = FontWeights.Normal;
    }

    private void ShowPayments()
    {
        RefreshPaymentsOverview();
        ResetPaymentsHeaderHint();
        ApplyPaymentsRouteBadge("#E2E8F0", "#CBD5E1", "#475569");
        ApplyPaymentsRouteSelection(null);
        DashboardPanel.Visibility = Visibility.Collapsed;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        PaymentsPanel.Visibility = Visibility.Visible;
        ReportsPanel.Visibility = Visibility.Collapsed;
        BackupPanel.Visibility = Visibility.Collapsed;
        DashboardNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        DashboardNavButton.FontWeight = FontWeights.Normal;
        InvoiceTypesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoiceTypesNavButton.FontWeight = FontWeights.Normal;
        SubscriptionsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        SubscriptionsNavButton.FontWeight = FontWeights.Normal;
        InvoicesNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        InvoicesNavButton.FontWeight = FontWeights.Normal;
        PaymentsNavButton.Foreground = Brushes.White;
        PaymentsNavButton.FontWeight = FontWeights.SemiBold;
        ReportsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        ReportsNavButton.FontWeight = FontWeights.Normal;
        BackupNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        BackupNavButton.FontWeight = FontWeights.Normal;
    }

    private void PaymentsQueueFilterButton_Click(object sender, RoutedEventArgs e)
    {
        _paymentsQueueFilterKey = sender switch
        {
            Button button when button == PaymentsQueueFilterUrgentButton => "urgent",
            Button button when button == PaymentsQueueFilterMissingPdfButton => "missing-pdf",
            _ => "all",
        };

        RefreshPaymentsOverview();
    }

    private void PaymentsRecentFilterButton_Click(object sender, RoutedEventArgs e)
    {
        _paymentsRecentFilterKey = sender switch
        {
            Button button when button == PaymentsRecentFilterMissingPdfButton => "missing-pdf",
            _ => "all",
        };

        RefreshPaymentsOverview();
    }

    private void ResetPaymentsQueueFilterButton_Click(object sender, RoutedEventArgs e)
    {
        _paymentsQueueFilterKey = "all";
        RefreshPaymentsOverview();
    }

    private void ResetPaymentsRecentFilterButton_Click(object sender, RoutedEventArgs e)
    {
        _paymentsRecentFilterKey = "all";
        RefreshPaymentsOverview();
    }

    private void PaymentsHintSource_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not FrameworkElement { Tag: string hintKey })
        {
            return;
        }

        PaymentsHeaderHintText.Text = BuildPaymentsHeaderHint(hintKey);
    }

    private void PaymentsHintSource_MouseLeave(object sender, MouseEventArgs e)
    {
        ResetPaymentsHeaderHint();
    }

    private void PaymentsHeaderAction_MouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is not FrameworkElement { Tag: string hintKey })
        {
            return;
        }

        PaymentsHeaderHintText.Text = BuildPaymentsHeaderHint(hintKey);
    }

    private void PersistPaymentsPanelContext(string hint, string badgeText, string backgroundHex, string borderHex, string foregroundHex, string? routeKey)
    {
        _activePaymentsHeaderHint = hint;
        _activePaymentsRouteBadge = badgeText;
        _activePaymentsRouteKey = routeKey;
        PaymentsHeaderHintText.Text = hint;
        PaymentsLastRouteBadgeText.Text = badgeText;
        ApplyPaymentsRouteBadge(backgroundHex, borderHex, foregroundHex);
        ApplyPaymentsRouteSelection(routeKey);
    }

    private void ResetPaymentsHeaderHint()
    {
        PaymentsHeaderHintText.Text = _activePaymentsHeaderHint;
        PaymentsLastRouteBadgeText.Text = _activePaymentsRouteBadge;
        PaymentsFlowContextText.Text = _activePaymentsFlowContext;
    }

    private void ApplyPaymentsRouteBadge(string backgroundHex, string borderHex, string foregroundHex)
    {
        PaymentsLastRouteBadgeBorder.Background = (Brush)new BrushConverter().ConvertFromString(backgroundHex)!;
        PaymentsLastRouteBadgeBorder.BorderBrush = (Brush)new BrushConverter().ConvertFromString(borderHex)!;
        PaymentsLastRouteBadgeText.Foreground = (Brush)new BrushConverter().ConvertFromString(foregroundHex)!;
    }

    private void ApplyPaymentsRouteSelection(string? routeKey)
    {
        SetPaymentsCardSelection(PaymentsMonthlySummaryCard, isSelected: routeKey == "monthly", "#DBEAFE", "#60A5FA");
        SetPaymentsCardSelection(PaymentsMissingPdfSummaryCard, isSelected: routeKey == "document", "#F0F9FF", "#38BDF8");
        SetPaymentsCardSelection(PaymentsUnpaidSummaryCard, isSelected: routeKey == "workspace" || routeKey == "unpaid", "#ECFDF5", "#4ADE80");
        SetPaymentsCardSelection(PaymentsWorkspaceFlowCard, isSelected: routeKey == "workspace", "#DCFCE7", "#4ADE80");
        SetPaymentsCardSelection(PaymentsDocumentFlowCard, isSelected: routeKey == "document", "#DBEAFE", "#60A5FA");
        SetPaymentsCardSelection(PaymentsUnpaidReportFlowCard, isSelected: routeKey == "unpaid", "#FEF3C7", "#FBBF24");
        SetPaymentsRouteNote(PaymentsMonthlyActiveRouteNote, routeKey == "monthly");
        SetPaymentsRouteNote(PaymentsMissingPdfActiveRouteNote, routeKey == "document");
        SetPaymentsRouteNote(PaymentsUnpaidActiveRouteNote, routeKey == "workspace" || routeKey == "unpaid");
        SetPaymentsRouteNote(PaymentsWorkspaceActiveRouteNote, routeKey == "workspace");
        SetPaymentsRouteNote(PaymentsDocumentActiveRouteNote, routeKey == "document");
        SetPaymentsRouteNote(PaymentsUnpaidReportActiveRouteNote, routeKey == "unpaid");
        SetPaymentsRouteNote(PaymentsWorkspaceTargetText, routeKey == "workspace");
        SetPaymentsRouteNote(PaymentsDocumentTargetText, routeKey == "document");
        SetPaymentsRouteNote(PaymentsUnpaidReportTargetText, routeKey == "unpaid");
        SetPaymentsRouteNote(PaymentsWorkspaceNextStepText, routeKey == "workspace");
        SetPaymentsRouteNote(PaymentsDocumentNextStepText, routeKey == "document");
        SetPaymentsRouteNote(PaymentsUnpaidReportNextStepText, routeKey == "unpaid");
        SetPaymentsShortcutHintState(PaymentsWorkspaceShortcutHint, routeKey == "workspace");
        SetPaymentsShortcutHintState(PaymentsDocumentShortcutHint, routeKey == "document");
        SetPaymentsShortcutHintState(PaymentsUnpaidReportShortcutHint, routeKey == "unpaid");
        SetPaymentsActiveColumnBadgeState(PaymentsWorkspaceActiveColumnBadge, routeKey == "workspace");
        SetPaymentsActiveColumnBadgeState(PaymentsDocumentActiveColumnBadge, routeKey == "document");
        SetPaymentsActiveColumnBadgeState(PaymentsUnpaidReportActiveColumnBadge, routeKey == "unpaid");
        SetPaymentsFlowTextState(PaymentsWorkspaceTitleText, PaymentsWorkspaceDescriptionText, routeKey == "workspace", "#166534", "#4B6B57");
        SetPaymentsFlowTextState(PaymentsDocumentTitleText, PaymentsDocumentDescriptionText, routeKey == "document", "#1D4ED8", "#4A6486");
        SetPaymentsFlowTextState(PaymentsUnpaidReportTitleText, PaymentsUnpaidReportDescriptionText, routeKey == "unpaid", "#B45309", "#7A6240");
        SetPaymentsActionButtonState(PaymentsMonthlyActionButton, routeKey == "monthly", "#DBEAFE", "#93C5FD", "#1D4ED8");
        SetPaymentsActionButtonState(PaymentsMissingPdfActionButton, routeKey == "document", "#F0F9FF", "#7DD3FC", "#0369A1");
        SetPaymentsActionButtonState(PaymentsUnpaidActionButton, routeKey == "workspace" || routeKey == "unpaid", "#ECFDF5", "#86EFAC", "#166534");
        SetPaymentsActionButtonState(PaymentsWorkspaceActionButton, routeKey == "workspace", "#DCFCE7", "#4ADE80", "#166534");
        SetPaymentsActionButtonState(PaymentsDocumentActionButton, routeKey == "document", "#DBEAFE", "#60A5FA", "#1D4ED8");
        SetPaymentsActionButtonState(PaymentsUnpaidReportActionButton, routeKey == "unpaid", "#FEF3C7", "#FBBF24", "#B45309");
        _activePaymentsFlowContext = BuildPaymentsFlowContext(routeKey);
        PaymentsFlowContextText.Text = _activePaymentsFlowContext;
    }

    private static void SetPaymentsCardSelection(Border card, bool isSelected, string selectedBackgroundHex, string selectedBorderHex)
    {
        if (!isSelected)
        {
            card.ClearValue(Border.BackgroundProperty);
            card.ClearValue(Border.BorderBrushProperty);
            return;
        }

        card.Background = (Brush)new BrushConverter().ConvertFromString(selectedBackgroundHex)!;
        card.BorderBrush = (Brush)new BrushConverter().ConvertFromString(selectedBorderHex)!;
    }

    private static void SetPaymentsRouteNote(TextBlock note, bool isVisible)
    {
        note.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    private static void SetPaymentsRouteNote(Border badge, bool isVisible)
    {
        badge.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    private static void SetPaymentsShortcutHintState(Border badge, bool isVisible)
    {
        if (!isVisible)
        {
            badge.BeginAnimation(UIElement.OpacityProperty, null);
            badge.Opacity = 1;
            badge.Visibility = Visibility.Collapsed;
            return;
        }

        badge.Visibility = Visibility.Visible;
        badge.Opacity = 0.86;
        badge.BeginAnimation(
            UIElement.OpacityProperty,
            new DoubleAnimation
            {
                From = 0.86,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(180)
            });
    }

    private static void SetPaymentsActiveColumnBadgeState(Border badge, bool isVisible)
    {
        if (!isVisible)
        {
            badge.BeginAnimation(UIElement.OpacityProperty, null);
            badge.Opacity = 1;
            badge.Visibility = Visibility.Collapsed;
            return;
        }

        badge.Visibility = Visibility.Visible;
        badge.Opacity = 0.86;
        badge.BeginAnimation(
            UIElement.OpacityProperty,
            new DoubleAnimation
            {
                From = 0.86,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(180)
            });
    }

    private static void SetPaymentsFlowTextState(TextBlock title, TextBlock description, bool isSelected, string titleHex, string descriptionHex)
    {
        if (!isSelected)
        {
            title.ClearValue(TextBlock.ForegroundProperty);
            description.ClearValue(TextBlock.ForegroundProperty);
            return;
        }

        title.Foreground = (Brush)new BrushConverter().ConvertFromString(titleHex)!;
        description.Foreground = (Brush)new BrushConverter().ConvertFromString(descriptionHex)!;
    }

    private static void SetPaymentsActionButtonState(Button button, bool isSelected, string backgroundHex, string borderHex, string foregroundHex)
    {
        if (!isSelected)
        {
            button.ClearValue(Button.BackgroundProperty);
            button.ClearValue(Button.BorderBrushProperty);
            button.ClearValue(Button.ForegroundProperty);
            return;
        }

        button.Background = (Brush)new BrushConverter().ConvertFromString(backgroundHex)!;
        button.BorderBrush = (Brush)new BrushConverter().ConvertFromString(borderHex)!;
        button.Foreground = (Brush)new BrushConverter().ConvertFromString(foregroundHex)!;
    }

    private static string BuildPaymentsHeaderHint(string hintKey)
    {
        return hintKey switch
        {
            "header_invoices" => "Fatura listesi acilir; genel kayit tarama ve secim icin kullanilir.",
            "header_reports" => "Rapor merkezi acilir; aylik, evrak ve acik bakiye kontrolleri buradan izlenir.",
            "summary_monthly" => "Bu kart aylik odeme raporuna baglidir; ay icindeki toplam tahsilati ozetler.",
            "summary_missing_pdf" => "Bu kart odeme PDF kontrolune baglidir; eksik evrakli kayitlari rapora tasir.",
            "summary_unpaid" => "Bu kart odeme calisma alanina baglidir; acik bakiyeli faturalara hizli gecis verir.",
            "flow_workspace" => "Islem yolu odeme giris alanini acar; odenmemis kayitlarla calismayi hizlandirir.",
            "flow_document" => "Evrak yolu kontrol raporunu acar; eksik odeme PDF kayitlarini denetler.",
            "flow_unpaid_report" => "Rapor yolu odenmemisler sekmesini acar; acik bakiye takibini toplar.",
            _ => DefaultPaymentsHeaderHint,
        };
    }

    private static string BuildPaymentsFlowContext(string? routeKey)
    {
        return routeKey switch
        {
            "monthly" => "Aylik kolon aktif: ustteki tahsilat ozeti ile alttaki rapor akisina odaklaniliyor.",
            "document" => "Evrak kolonu aktif: PDF eksik ozetinden kontrol raporu akisina gecis izleniyor.",
            "workspace" => "Islem kolonu aktif: odenmemis ozetten odeme giris alanina gecis izleniyor.",
            "unpaid" => "Bakiye kolonu aktif: acik faturalar ozeti ile rapor akisinin iliskisi izleniyor.",
            _ => DefaultPaymentsFlowContext,
        };
    }

    private void ShowReports()
    {
        ReportsPanel.Refresh();
        DashboardPanel.Visibility = Visibility.Collapsed;
        InvoiceTypesPanel.Visibility = Visibility.Collapsed;
        SubscriptionsPanel.Visibility = Visibility.Collapsed;
        InvoicesPanel.Visibility = Visibility.Collapsed;
        PaymentsPanel.Visibility = Visibility.Collapsed;
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
        PaymentsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        PaymentsNavButton.FontWeight = FontWeights.Normal;
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
        PaymentsPanel.Visibility = Visibility.Collapsed;
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
        PaymentsNavButton.Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225));
        PaymentsNavButton.FontWeight = FontWeights.Normal;
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
        DashboardMonthlyInvoiceCountText.Text = $"{summary.MonthlyInvoiceCount} kayit";
        DashboardMonthlyPaymentTotalText.Text = FormatMoney(summary.MonthlyPaymentTotal);
        DashboardMonthlyPaymentCountText.Text = $"{summary.MonthlyPaymentCount} kayit";
        DashboardUnreviewedInvoiceCountText.Text = summary.UnreviewedInvoiceCount.ToString(CultureInfo.InvariantCulture);
        DashboardUnpaidInvoiceCountText.Text = summary.UnpaidInvoiceCount.ToString(CultureInfo.InvariantCulture);
        DashboardUnpaidRemainingText.Text = $"Kalan {FormatMoney(summary.UnpaidRemainingTotal)}";
        DashboardOverdueInvoiceCountText.Text = summary.OverdueInvoiceCount.ToString(CultureInfo.InvariantCulture);
        DashboardOverdueRemainingText.Text = $"Kalan {FormatMoney(summary.OverdueRemainingTotal)}";
        DashboardMissingInvoicePdfCountText.Text = summary.MissingInvoicePdfCount.ToString(CultureInfo.InvariantCulture);
        DashboardMissingPaymentPdfCountText.Text = summary.MissingPaymentPdfCount.ToString(CultureInfo.InvariantCulture);
    }

    private void RefreshPaymentsOverview()
    {
        var invoices = _invoiceRepository.GetAll();
        var payments = _paymentRepository.GetAll();
        var summary = DashboardSummaryCalculator.Calculate(
            invoices,
            payments,
            DateTime.Today,
            invoice => _invoiceRepository.IsPdfMissing(invoice),
            payment => _paymentRepository.IsPdfMissing(payment));

        PaymentsMonthlyTotalText.Text = FormatMoney(summary.MonthlyPaymentTotal);
        PaymentsMonthlyCountText.Text = $"{summary.MonthlyPaymentCount} kayit";
        PaymentsMissingPdfCountText.Text = summary.MissingPaymentPdfCount.ToString(CultureInfo.InvariantCulture);
        PaymentsUnpaidInvoiceCountText.Text = summary.UnpaidInvoiceCount.ToString(CultureInfo.InvariantCulture);
        PaymentsUnpaidRemainingText.Text = $"Kalan {FormatMoney(summary.UnpaidRemainingTotal)}";

        var unpaidQueueCandidates = invoices
            .Where(item => !string.Equals(item.Status, "canceled", StringComparison.OrdinalIgnoreCase) && item.RemainingAmount > 0)
            .OrderBy(item => item.DueDate)
            .ThenByDescending(item => item.RemainingAmount)
            .Take(5)
            .Select(item => new PaymentsQueueItem(
                item.Id,
                BuildInvoiceQueueTitle(item),
                $"{item.Period} - Vade {item.DueDate:dd.MM.yyyy}",
                $"Kalan {FormatMoney(item.RemainingAmount)}",
                item.State,
                GetQueueStatusBackground(item),
                GetQueueStatusBorder(item),
                GetQueueStatusForeground(item),
                item.DueDate.Date <= DateTime.Today,
                _invoiceRepository.IsPdfMissing(item)))
            .ToList();
        var unpaidQueueItems = unpaidQueueCandidates
            .Where(ShouldIncludeQueueItem)
            .ToList();

        PaymentsQueueFilterAllButton.Content = $"Hepsi ({unpaidQueueCandidates.Count})";
        PaymentsQueueFilterUrgentButton.Content = $"Acil ({unpaidQueueCandidates.Count(item => item.IsUrgent)})";
        PaymentsQueueFilterMissingPdfButton.Content = $"PDF Eksik ({unpaidQueueCandidates.Count(item => item.IsPdfMissing)})";
        ApplyPaymentsFilterButtonState(PaymentsQueueFilterAllButton, _paymentsQueueFilterKey == "all");
        ApplyPaymentsFilterButtonState(PaymentsQueueFilterUrgentButton, _paymentsQueueFilterKey == "urgent");
        ApplyPaymentsFilterButtonState(PaymentsQueueFilterMissingPdfButton, _paymentsQueueFilterKey == "missing-pdf");
        ApplyPaymentsActiveFilterBadgeState(
            PaymentsQueueActiveFilterBadge,
            PaymentsQueueActiveFilterText,
            _paymentsQueueFilterKey == "urgent" ? "#FFF7ED" : _paymentsQueueFilterKey == "missing-pdf" ? "#FEF2F2" : "#EFF6FF",
            _paymentsQueueFilterKey == "urgent" ? "#FED7AA" : _paymentsQueueFilterKey == "missing-pdf" ? "#FECACA" : "#BFDBFE",
            _paymentsQueueFilterKey == "urgent" ? "#C2410C" : _paymentsQueueFilterKey == "missing-pdf" ? "#B91C1C" : "#1D4ED8");
        PaymentsQueueActiveFilterText.Text = $"AKTIF KUYRUK: {BuildQueueFilterLabel().ToUpperInvariant()} - {unpaidQueueItems.Count} KAYIT";
        PaymentsQueueActiveFilterBadge.ToolTip = BuildQueueActiveFilterTooltip(unpaidQueueItems.Count);
        PaymentsQueueItemsControl.ItemsSource = unpaidQueueItems;
        PaymentsQueueSummaryText.Text = unpaidQueueItems.Count == 0
            ? $"{BuildQueueFilterLabel()} icin kayit bulunamadi. Gerekirse Hepsini Goster ile tum kuyruga donun."
            : $"{BuildQueueFilterSummaryPrefix(summary.UnpaidInvoiceCount)} en yakin {unpaidQueueItems.Count} kayit burada listeleniyor.";
        if (unpaidQueueItems.Count == 0)
        {
            PaymentsOpenNextQueueButton.IsEnabled = false;
            PaymentsOpenNextQueueButton.Tag = null;
            PaymentsQueueActionHintText.Text = $"{BuildQueueFilterLabel()} bos. Hepsini Goster ile tum kuyruga donun ya da farkli bir filtre deneyin.";
            PaymentsQueueResetFilterButton.Visibility = _paymentsQueueFilterKey == "all"
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        else
        {
            var topQueueItem = unpaidQueueItems[0];
            PaymentsOpenNextQueueButton.IsEnabled = true;
            PaymentsOpenNextQueueButton.Tag = topQueueItem.InvoiceId;
            PaymentsQueueActionHintText.Text = $"{topQueueItem.Title} - {topQueueItem.Amount}";
            PaymentsQueueResetFilterButton.Visibility = Visibility.Collapsed;
        }

        var invoicesById = invoices.ToDictionary(item => item.Id);
        var recentPaymentCandidates = payments
            .OrderByDescending(item => item.PaymentDate)
            .ThenByDescending(item => item.Id)
            .Take(5)
            .Select(item => new PaymentsRecentPaymentItem(
                item.InvoiceId,
                BuildRecentPaymentTitle(item, invoicesById),
                BuildRecentPaymentMeta(item, invoicesById),
                FormatMoney(item.Amount),
                _paymentRepository.IsPdfMissing(item) ? "PDF eksik" : "PDF hazir",
                _paymentRepository.IsPdfMissing(item) ? "#FEF2F2" : "#EFF6FF",
                _paymentRepository.IsPdfMissing(item) ? "#FECACA" : "#BFDBFE",
                _paymentRepository.IsPdfMissing(item) ? "#B91C1C" : "#1D4ED8",
                _paymentRepository.IsPdfMissing(item)))
            .ToList();
        var recentPaymentItems = recentPaymentCandidates
            .Where(ShouldIncludeRecentPaymentItem)
            .ToList();

        PaymentsRecentFilterAllButton.Content = $"Hepsi ({recentPaymentCandidates.Count})";
        PaymentsRecentFilterMissingPdfButton.Content = $"PDF Eksik ({recentPaymentCandidates.Count(item => item.IsPdfMissing)})";
        ApplyPaymentsFilterButtonState(PaymentsRecentFilterAllButton, _paymentsRecentFilterKey == "all");
        ApplyPaymentsFilterButtonState(PaymentsRecentFilterMissingPdfButton, _paymentsRecentFilterKey == "missing-pdf");
        ApplyPaymentsActiveFilterBadgeState(
            PaymentsRecentActiveFilterBadge,
            PaymentsRecentActiveFilterText,
            _paymentsRecentFilterKey == "missing-pdf" ? "#FEF2F2" : "#EFF6FF",
            _paymentsRecentFilterKey == "missing-pdf" ? "#FECACA" : "#BFDBFE",
            _paymentsRecentFilterKey == "missing-pdf" ? "#B91C1C" : "#1D4ED8");
        PaymentsRecentActiveFilterText.Text = $"AKTIF SON ODEME: {BuildRecentFilterLabel().ToUpperInvariant()} - {recentPaymentItems.Count} KAYIT";
        PaymentsRecentActiveFilterBadge.ToolTip = BuildRecentActiveFilterTooltip(recentPaymentItems.Count);
        PaymentsRecentPaymentsItemsControl.ItemsSource = recentPaymentItems;
        PaymentsRecentPaymentsSummaryText.Text = recentPaymentItems.Count == 0
            ? $"{BuildRecentFilterLabel()} icin kayit bulunamadi. Gerekirse Hepsini Goster ile tum son odemelere donun."
            : $"{BuildRecentFilterSummaryPrefix()} {recentPaymentItems.Count} odeme kaydi burada hizlica kontrol edilebilir.";
        if (recentPaymentItems.Count == 0)
        {
            PaymentsOpenLatestPaymentButton.IsEnabled = false;
            PaymentsOpenLatestPaymentButton.Tag = null;
            PaymentsRecentActionHintText.Text = $"{BuildRecentFilterLabel()} bos. Hepsini Goster ile tum son odemelere donun ya da farkli bir filtre deneyin.";
            PaymentsRecentResetFilterButton.Visibility = _paymentsRecentFilterKey == "all"
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
        else
        {
            var topRecentPaymentItem = recentPaymentItems[0];
            PaymentsOpenLatestPaymentButton.IsEnabled = true;
            PaymentsOpenLatestPaymentButton.Tag = topRecentPaymentItem.InvoiceId;
            PaymentsRecentActionHintText.Text = $"{topRecentPaymentItem.Title} - {topRecentPaymentItem.Amount}";
            PaymentsRecentResetFilterButton.Visibility = Visibility.Collapsed;
        }

        ApplyFeaturedPaymentsSummary(
            unpaidQueueItems.Count > 0 ? unpaidQueueItems[0] : null,
            recentPaymentItems.Count > 0 ? recentPaymentItems[0] : null);
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


    private bool ShouldIncludeQueueItem(PaymentsQueueItem item)
    {
        return _paymentsQueueFilterKey switch
        {
            "urgent" => item.IsUrgent,
            "missing-pdf" => item.IsPdfMissing,
            _ => true,
        };
    }

    private bool ShouldIncludeRecentPaymentItem(PaymentsRecentPaymentItem item)
    {
        return _paymentsRecentFilterKey switch
        {
            "missing-pdf" => item.IsPdfMissing,
            _ => true,
        };
    }

    private string BuildQueueFilterLabel()
    {
        return _paymentsQueueFilterKey switch
        {
            "urgent" => "Acil filtre",
            "missing-pdf" => "PDF eksik filtre",
            _ => "Tum kuyruk",
        };
    }

    private string BuildRecentFilterLabel()
    {
        return _paymentsRecentFilterKey switch
        {
            "missing-pdf" => "PDF eksik filtre",
            _ => "Tum son odemeler",
        };
    }

    private string BuildQueueFilterSummaryPrefix(int unpaidInvoiceCount)
    {
        return _paymentsQueueFilterKey switch
        {
            "urgent" => $"Acil odeme filtresinde {unpaidInvoiceCount} acik faturadan",
            "missing-pdf" => $"PDF eksik filtresinde {unpaidInvoiceCount} acik faturadan",
            _ => $"{unpaidInvoiceCount} acik faturadan",
        };
    }

    private string BuildRecentFilterSummaryPrefix()
    {
        return _paymentsRecentFilterKey switch
        {
            "missing-pdf" => "PDF eksik odemeler icinde",
            _ => "En son",
        };
    }

    private string BuildQueueActiveFilterTooltip(int visibleCount)
    {
        return _paymentsQueueFilterKey switch
        {
            "urgent" => $"Acil odeme gorunumu acik. En yakin bes kayit icinden {visibleCount} kayit gosteriliyor.",
            "missing-pdf" => $"PDF eksik kuyruk gorunumu acik. En yakin bes kayit icinden {visibleCount} kayit gosteriliyor.",
            _ => $"Tum kuyruk gorunumu acik. En yakin bes kayittan {visibleCount} kayit gosteriliyor.",
        };
    }

    private string BuildRecentActiveFilterTooltip(int visibleCount)
    {
        return _paymentsRecentFilterKey switch
        {
            "missing-pdf" => $"PDF eksik son odemeler gorunumu acik. En son bes kayit icinden {visibleCount} kayit gosteriliyor.",
            _ => $"Tum son odemeler gorunumu acik. En son bes kayittan {visibleCount} kayit gosteriliyor.",
        };
    }

    private static void ApplyPaymentsFilterButtonState(Button button, bool isSelected)
    {
        button.Background = (Brush)new BrushConverter().ConvertFromString(isSelected ? "#DBEAFE" : "#F8FAFC")!;
        button.BorderBrush = (Brush)new BrushConverter().ConvertFromString(isSelected ? "#93C5FD" : "#D8E2EC")!;
        button.Foreground = (Brush)new BrushConverter().ConvertFromString(isSelected ? "#1D4ED8" : "#475569")!;
        button.FontWeight = isSelected ? FontWeights.SemiBold : FontWeights.Normal;
    }

    private static void ApplyPaymentsActiveFilterBadgeState(Border badge, TextBlock text, string backgroundHex, string borderHex, string foregroundHex)
    {
        badge.Background = (Brush)new BrushConverter().ConvertFromString(backgroundHex)!;
        badge.BorderBrush = (Brush)new BrushConverter().ConvertFromString(borderHex)!;
        text.Foreground = (Brush)new BrushConverter().ConvertFromString(foregroundHex)!;
    }
    private static string FormatMoney(decimal value)
    {
        return value.ToString("N2", CultureInfo.GetCultureInfo("tr-TR"));
    }

    private static string BuildInvoiceQueueTitle(Invoice invoice)
    {
        var owner = string.IsNullOrWhiteSpace(invoice.InstitutionName)
            ? invoice.SubscriptionName
            : invoice.InstitutionName;
        var invoiceNo = string.IsNullOrWhiteSpace(invoice.InvoiceNo)
            ? "No yok"
            : invoice.InvoiceNo;
        return $"{owner} - {invoiceNo}";
    }

    private static string BuildRecentPaymentTitle(Payment payment, IReadOnlyDictionary<long, Invoice> invoicesById)
    {
        if (!invoicesById.TryGetValue(payment.InvoiceId, out var invoice))
        {
            return $"Fatura #{payment.InvoiceId}";
        }

        var owner = string.IsNullOrWhiteSpace(invoice.InstitutionName)
            ? invoice.SubscriptionName
            : invoice.InstitutionName;
        return owner;
    }

    private static string BuildRecentPaymentMeta(Payment payment, IReadOnlyDictionary<long, Invoice> invoicesById)
    {
        if (!invoicesById.TryGetValue(payment.InvoiceId, out var invoice))
        {
            return $"{payment.PaymentDate:dd.MM.yyyy} - Fatura kaydi bulunamadi";
        }

        var invoiceNo = string.IsNullOrWhiteSpace(invoice.InvoiceNo)
            ? invoice.Period
            : invoice.InvoiceNo;
        return $"{payment.PaymentDate:dd.MM.yyyy} - {invoiceNo}";
    }

    private static string GetQueueStatusBackground(Invoice invoice)
    {
        return GetQueueStatusKind(invoice) switch
        {
            "partial" => "#FFF7ED",
            "paid" => "#ECFDF5",
            _ => "#FEF2F2",
        };
    }

    private static string GetQueueStatusBorder(Invoice invoice)
    {
        return GetQueueStatusKind(invoice) switch
        {
            "partial" => "#FED7AA",
            "paid" => "#BBF7D0",
            _ => "#FECACA",
        };
    }

    private static string GetQueueStatusForeground(Invoice invoice)
    {
        return GetQueueStatusKind(invoice) switch
        {
            "partial" => "#C2410C",
            "paid" => "#166534",
            _ => "#B91C1C",
        };
    }

    private static string GetQueueStatusKind(Invoice invoice)
    {
        if (string.Equals(invoice.Status, "paid", StringComparison.OrdinalIgnoreCase))
        {
            return "paid";
        }

        if (invoice.PaidAmount > 0 && invoice.RemainingAmount > 0)
        {
            return "partial";
        }

        return "unpaid";
    }

    private void ApplyFeaturedPaymentsSummary(PaymentsQueueItem? queueItem, PaymentsRecentPaymentItem? recentPaymentItem)
    {
        if (queueItem is not null)
        {
            PaymentsFeaturedTitleText.Text = queueItem.Title;
            PaymentsFeaturedMetaText.Text = $"{queueItem.Meta} - Oncelikli odeme kuyrugu";
            PaymentsFeaturedAmountText.Text = queueItem.Amount;
            PaymentsFeaturedHintText.Text = "Bugun odeme isine bu kayitla devam etmek en hizli secim.";
            PaymentsFeaturedStatusText.Text = queueItem.Status.ToUpperInvariant();
            PaymentsFeaturedStatusBadge.Background = (Brush)new BrushConverter().ConvertFromString(queueItem.StatusBackground)!;
            PaymentsFeaturedStatusBadge.BorderBrush = (Brush)new BrushConverter().ConvertFromString(queueItem.StatusBorder)!;
            PaymentsFeaturedStatusText.Foreground = (Brush)new BrushConverter().ConvertFromString(queueItem.StatusForeground)!;
            PaymentsOpenFeaturedButton.IsEnabled = true;
            PaymentsOpenFeaturedButton.Tag = queueItem.InvoiceId;
            PaymentsOpenFeaturedButton.CommandParameter = "queue";
            return;
        }

        if (recentPaymentItem is not null)
        {
            PaymentsFeaturedTitleText.Text = recentPaymentItem.Title;
            PaymentsFeaturedMetaText.Text = $"{recentPaymentItem.Meta} - Son odeme kaydi";
            PaymentsFeaturedAmountText.Text = recentPaymentItem.Amount;
            PaymentsFeaturedHintText.Text = "Son odeme baglamina hizlica donup kaydi tekrar kontrol edebilirsiniz.";
            PaymentsFeaturedStatusText.Text = recentPaymentItem.Status.ToUpperInvariant();
            PaymentsFeaturedStatusBadge.Background = (Brush)new BrushConverter().ConvertFromString(recentPaymentItem.StatusBackground)!;
            PaymentsFeaturedStatusBadge.BorderBrush = (Brush)new BrushConverter().ConvertFromString(recentPaymentItem.StatusBorder)!;
            PaymentsFeaturedStatusText.Foreground = (Brush)new BrushConverter().ConvertFromString(recentPaymentItem.StatusForeground)!;
            PaymentsOpenFeaturedButton.IsEnabled = true;
            PaymentsOpenFeaturedButton.Tag = recentPaymentItem.InvoiceId;
            PaymentsOpenFeaturedButton.CommandParameter = "recent";
            return;
        }

        PaymentsFeaturedTitleText.Text = "Oncelikli odeme kaydi hazir degil.";
        PaymentsFeaturedMetaText.Text = "Bekleyen ya da son odeme baglami olustugunda burada gorunur.";
        PaymentsFeaturedAmountText.Text = "0,00";
        PaymentsFeaturedHintText.Text = "Oncelik olusunca hizli acis burada hazirlanir.";
        PaymentsFeaturedStatusText.Text = "HAZIR DEGIL";
        PaymentsFeaturedStatusBadge.Background = (Brush)new BrushConverter().ConvertFromString("#E2E8F0")!;
        PaymentsFeaturedStatusBadge.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#CBD5E1")!;
        PaymentsFeaturedStatusText.Foreground = (Brush)new BrushConverter().ConvertFromString("#475569")!;
        PaymentsOpenFeaturedButton.IsEnabled = false;
        PaymentsOpenFeaturedButton.Tag = null;
        PaymentsOpenFeaturedButton.CommandParameter = null;
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
        InvoiceTypeFormTitleText.Text = "Fatura Turunu Duzenle";
        InvoiceTypeNameInput.Text = invoiceType.Name;
        DefaultUsageUnitInput.Text = invoiceType.DefaultUsageUnit;
        InvoiceTypeDescriptionInput.Text = invoiceType.Description;
        InvoiceTypeIsActiveInput.IsChecked = invoiceType.IsActive;
        ToggleInvoiceTypeButton.IsEnabled = true;
        ToggleInvoiceTypeButton.Content = invoiceType.IsActive ? "Pasife Al" : "Aktif Yap";
        SetInvoiceTypeStatus($"Secili kayit: {invoiceType.Name}", isError: false);
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
            SetInvoiceTypeStatus("Fatura turu kaydedildi.", isError: false);
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
            SetInvoiceTypeStatus("Once bir fatura turu secin.", isError: true);
            return;
        }

        try
        {
            var nextState = !_selectedInvoiceType.IsActive;
            _invoiceTypeRepository.SetActive(_selectedInvoiceType.Id, nextState);
            RefreshInvoiceTypes(_selectedInvoiceType.Id);
            SetInvoiceTypeStatus(nextState ? "Fatura turu aktif yapildi." : "Fatura turu pasife alindi.", isError: false);
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
        InvoiceTypeFormTitleText.Text = "Yeni Fatura Turu";
        InvoiceTypeNameInput.Text = string.Empty;
        DefaultUsageUnitInput.Text = string.Empty;
        InvoiceTypeDescriptionInput.Text = string.Empty;
        InvoiceTypeIsActiveInput.IsChecked = true;
        ToggleInvoiceTypeButton.IsEnabled = false;
        ToggleInvoiceTypeButton.Content = "Pasife Al";
        SetInvoiceTypeStatus("Yeni kayit icin alanlari doldurun.", isError: false);
    }

    private void SetInvoiceTypeStatus(string message, bool isError)
    {
        InvoiceTypeStatusText.Text = message;
        InvoiceTypeStatusText.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(185, 28, 28))
            : new SolidColorBrush(Color.FromRgb(95, 107, 122));
    }

    private sealed record PaymentsQueueItem(
        long InvoiceId,
        string Title,
        string Meta,
        string Amount,
        string Status,
        string StatusBackground,
        string StatusBorder,
        string StatusForeground,
        bool IsUrgent,
        bool IsPdfMissing);

    private sealed record PaymentsRecentPaymentItem(
        long InvoiceId,
        string Title,
        string Meta,
        string Amount,
        string Status,
        string StatusBackground,
        string StatusBorder,
        string StatusForeground,
        bool IsPdfMissing);
}

