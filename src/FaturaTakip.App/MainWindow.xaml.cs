using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        SetPaymentsRouteNote(PaymentsWorkspaceActiveColumnBadge, routeKey == "workspace");
        SetPaymentsRouteNote(PaymentsDocumentActiveColumnBadge, routeKey == "document");
        SetPaymentsRouteNote(PaymentsUnpaidReportActiveColumnBadge, routeKey == "unpaid");
        SetPaymentsFlowTextState(PaymentsWorkspaceTitleText, PaymentsWorkspaceDescriptionText, routeKey == "workspace", "#166534", "#166534");
        SetPaymentsFlowTextState(PaymentsDocumentTitleText, PaymentsDocumentDescriptionText, routeKey == "document", "#1D4ED8", "#1E40AF");
        SetPaymentsFlowTextState(PaymentsUnpaidReportTitleText, PaymentsUnpaidReportDescriptionText, routeKey == "unpaid", "#B45309", "#92400E");
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
