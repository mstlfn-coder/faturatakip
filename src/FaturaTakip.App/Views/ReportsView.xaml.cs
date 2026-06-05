using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FaturaTakip.App.Data.AuditLogs;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.InvoiceTypes;
using FaturaTakip.App.Data.Payments;
using FaturaTakip.App.Data.Reports;
using FaturaTakip.App.Data.Subscriptions;
using FaturaTakip.App.Infrastructure;

namespace FaturaTakip.App.Views;

public partial class ReportsView : UserControl
{
    private static readonly CultureInfo TurkishCulture = CultureInfo.GetCultureInfo("tr-TR");
    private InvoiceRepository? _invoiceRepository;
    private PaymentRepository? _paymentRepository;
    private InvoiceTypeRepository? _invoiceTypeRepository;
    private SubscriptionRepository? _subscriptionRepository;
    private AuditLogRepository? _auditLogRepository;
    private ActionableInvoiceReport _report = ActionableInvoiceReport.Empty;
    private MonthlyInvoiceReport _monthlyReport = MonthlyInvoiceReport.Empty;
    private YearlyInvoiceListReport _yearlyAllReport = YearlyInvoiceListReport.Empty;
    private SubscriptionMonthlyComparison _subscriptionComparison = SubscriptionMonthlyComparison.Empty;
    private SubscriptionYearlyReport _subscriptionYearly = SubscriptionYearlyReport.Empty;
    private InvoiceTypeYearlyReport _typeYearly = InvoiceTypeYearlyReport.Empty;
    private DocumentHealthReport _documentHealth = DocumentHealthReport.Empty;
    private ConsistencyReport _consistency = ConsistencyReport.Empty;
    private IReadOnlyList<AuditLog> _auditLogs = Array.Empty<AuditLog>();
    private AuditLog? _selectedAuditLog;
    private AuditLogFilterPreferences _auditLogFilterPreferences = AuditLogFilterPreferences.Default;
    private string? _lastAuditLogExportPath;
    private List<AuditLogExportItem> _recentAuditLogExports = [];
    private ReportTab _activeTab = ReportTab.Unpaid;
    private Dictionary<long, List<Payment>> _paymentsByInvoice = new();
    private string _rootDirectory = string.Empty;
    private bool _isInitialized;
    private bool _isRefreshingMonthlyFilters;
    private bool _isRefreshingSubscriptionFilters;
    private bool _isRefreshingTypeYearlyFilters;
    private bool _isRefreshingAuditLogFilters;

    public ReportsView()
    {
        InitializeComponent();
    }

    public void Initialize(string databasePath)
    {
        _rootDirectory = AppPaths.Resolve().RootDirectory;
        _auditLogFilterPreferences = AuditLogFilterPreferences.LoadOrDefault(_rootDirectory);
        _invoiceRepository = new InvoiceRepository(databasePath);
        _paymentRepository = new PaymentRepository(databasePath);
        _invoiceTypeRepository = new InvoiceTypeRepository(databasePath);
        _subscriptionRepository = new SubscriptionRepository(databasePath);
        _auditLogRepository = new AuditLogRepository(databasePath);
        _isInitialized = true;
        Refresh();
    }

    public void Refresh()
    {
        if (!_isInitialized || _invoiceRepository is null || _paymentRepository is null)
        {
            return;
        }

        var invoices = _invoiceRepository.GetAll();
        var payments = _paymentRepository.GetAll();
        _paymentsByInvoice = payments
            .GroupBy(p => p.InvoiceId)
            .ToDictionary(g => g.Key, g => g.OrderByDescending(x => x.PaymentDate).ThenByDescending(x => x.Id).ToList());
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
        _yearlyAllReport = YearlyInvoiceListReportCalculator.Calculate(
            invoices,
            GetSelectedYear(),
            GetSelectedInvoiceTypeId(),
            DateTime.Today,
            invoice => _invoiceRepository.IsPdfMissing(invoice));

        EnsureSubscriptionFiltersInitialized(invoices);
        EnsureTypeYearlyFiltersInitialized(invoices);
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

        _subscriptionYearly = selectedSubscriptionId is null
            ? SubscriptionYearlyReport.Empty
            : SubscriptionYearlyReportCalculator.Calculate(
                invoices,
                selectedSubscriptionId.Value,
                GetSelectedSubscriptionYear(),
                DateTime.Today,
                invoice => _invoiceRepository.IsPdfMissing(invoice));

        var selectedType = GetSelectedTypeYearlyInvoiceType();
        _typeYearly = selectedType is null || selectedType.InvoiceTypeId is null
            ? InvoiceTypeYearlyReport.Empty
            : InvoiceTypeYearlyReportCalculator.Calculate(
                invoices,
                selectedType.InvoiceTypeId.Value,
                selectedType.Label,
                GetSelectedTypeYearlyYear(),
                DateTime.Today,
                invoice => _invoiceRepository.IsPdfMissing(invoice));

        _documentHealth = DocumentHealthReportCalculator.Calculate(
            invoices,
            payments,
            invoice => _invoiceRepository.PdfFileExists(invoice),
            payment => _paymentRepository.PdfFileExists(payment));

        _consistency = ConsistencyReportCalculator.Calculate(
            invoices,
            payments,
            invoice => _invoiceRepository.PdfFileExists(invoice),
            payment => _paymentRepository.PdfFileExists(payment));

        _auditLogs = (_auditLogRepository?.GetAll() ?? Array.Empty<AuditLog>())
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .ToList();
        EnsureAuditLogFiltersInitialized();

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

    private void YearlyTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.YearlyAll);
    }

    private void SubscriptionTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.Subscription);
    }

    private void SubscriptionYearlyTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.SubscriptionYearly);
    }

    private void TypeYearlyTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.TypeYearly);
    }

    private void DocumentHealthTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.DocumentHealth);
    }

    private void ConsistencyTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.Consistency);
    }

    private void AuditLogTabButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTab(ReportTab.AuditLog);
    }

    private void AuditLogFilter_Changed(object? sender, SelectionChangedEventArgs e)
    {
        if (_isRefreshingAuditLogFilters)
        {
            return;
        }

        if (AuditLogStartDateInput.SelectedDate is DateTime start &&
            AuditLogEndDateInput.SelectedDate is DateTime end &&
            start.Date > end.Date)
        {
            _isRefreshingAuditLogFilters = true;
            AuditLogEndDateInput.SelectedDate = start.Date;
            _isRefreshingAuditLogFilters = false;
        }

        if (_activeTab == ReportTab.AuditLog)
        {
            ApplyTab(ReportTab.AuditLog);
        }

        SaveAuditLogFilterPreferences();
    }

    private void AuditLogSearchInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isRefreshingAuditLogFilters)
        {
            return;
        }

        if (_activeTab == ReportTab.AuditLog)
        {
            ApplyTab(ReportTab.AuditLog);
        }

        SaveAuditLogFilterPreferences();
    }

    private void ResetAuditLogFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        _auditLogFilterPreferences = AuditLogFilterPreferences.Default;
        ResetAuditLogFilterInputs();

        if (_activeTab == ReportTab.AuditLog)
        {
            ApplyTab(ReportTab.AuditLog);
        }

        SaveAuditLogFilterPreferences();
        AuditLogHintText.Text = "Audit log filtreleri sifirlandi.";
    }

    private void FocusSelectedAuditLogButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedAuditLog is null)
        {
            AuditLogHintText.Text = "Odaklanacak secili kayit yok.";
            return;
        }

        if (TryFocusSelectedAuditLog())
        {
            AuditLogHintText.Text = $"Kayit gorunur alana getirildi: {_selectedAuditLog.RecordId}";
            return;
        }

        AuditLogHintText.Text = "Secili kayit mevcut filtrelerde gorunmuyor.";
    }

    private void AuditLogGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (AuditLogGrid.SelectedItem is AuditLogRow row)
        {
            _selectedAuditLog = row.Source;
            ApplyAuditLogDetail(row.Source);
            return;
        }

        _selectedAuditLog = null;
        ApplyAuditLogDetail(null);
    }

    private void AuditLogDiffFilter_Changed(object sender, RoutedEventArgs e)
    {
        if (_isRefreshingAuditLogFilters)
        {
            return;
        }

        ApplyAuditLogDetail(_selectedAuditLog);
        SaveAuditLogFilterPreferences();
    }

    private void CopyOldAuditLogButton_Click(object sender, RoutedEventArgs e)
    {
        CopyAuditLogText(AuditLogOldValueTextBox.Text, "Eski deger kopyalandi.");
    }

    private void CopyNewAuditLogButton_Click(object sender, RoutedEventArgs e)
    {
        CopyAuditLogText(AuditLogNewValueTextBox.Text, "Yeni deger kopyalandi.");
    }

    private void CopyAuditLogDiffButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedAuditLog is null)
        {
            AuditLogHintText.Text = "Kopyalanacak diff kaydi yok.";
            return;
        }

        var diffRows = (AuditLogDiffGrid.ItemsSource as IEnumerable<AuditLogDiffRow>)?.ToList() ?? new List<AuditLogDiffRow>();
        if (diffRows.Count == 0)
        {
            AuditLogHintText.Text = "Kopyalanacak diff kaydi yok.";
            return;
        }

        var clipboardText = BuildAuditLogDiffClipboardText(_selectedAuditLog, diffRows);
        CopyAuditLogText(clipboardText, "Diff panoya kopyalandi.");
    }

    private void ExportAuditLogTxtButton_Click(object sender, RoutedEventArgs e)
    {
        ExportAuditLogDetail("txt");
    }

    private void ExportAuditLogJsonButton_Click(object sender, RoutedEventArgs e)
    {
        ExportAuditLogDetail("json");
    }

    private void OpenAuditLogExportsButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);

            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"\"{exportsDir}\"",
                UseShellExecute = true
            });

            AuditLogHintText.Text = "Exports klasoru acildi.";
        }
        catch (Exception exception) when (exception is Win32Exception or InvalidOperationException or IOException or UnauthorizedAccessException)
        {
            AuditLogHintText.Text = $"Klasor acilamadi: {exception.Message}";
        }
    }

    private void OpenLastAuditLogExportButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);
            RefreshRecentAuditLogExports(exportsDir);

            var targetPath = ResolveLastAuditLogExportPath(exportsDir);
            if (string.IsNullOrWhiteSpace(targetPath) || !File.Exists(targetPath))
            {
                AuditLogHintText.Text = "Acilacak audit log dosyasi bulunamadi.";
                return;
            }

            _lastAuditLogExportPath = targetPath;
            RefreshRecentAuditLogExports(exportsDir);

            Process.Start(new ProcessStartInfo
            {
                FileName = targetPath,
                UseShellExecute = true
            });

            AuditLogHintText.Text = $"Dosya acildi: {Path.GetFileName(targetPath)}";
        }
        catch (Exception exception) when (exception is Win32Exception or InvalidOperationException or IOException or UnauthorizedAccessException)
        {
            AuditLogHintText.Text = $"Dosya acilamadi: {exception.Message}";
        }
    }

    private void OpenSelectedAuditLogExportButton_Click(object sender, RoutedEventArgs e)
    {
        if (AuditLogRecentExportsInput.SelectedItem is not AuditLogExportItem item || !File.Exists(item.FilePath))
        {
            AuditLogHintText.Text = "Acilacak export dosyasi secili degil.";
            return;
        }

        _lastAuditLogExportPath = item.FilePath;
        RefreshRecentAuditLogExports(Path.Combine(AppPaths.Resolve().RootDirectory, "exports"));
        TryOpenAuditLogPath(item.FilePath, $"Dosya acildi: {item.DisplayName}");
    }

    private void RefreshAuditLogExportsButton_Click(object sender, RoutedEventArgs e)
    {
        var exportsDir = Path.Combine(AppPaths.Resolve().RootDirectory, "exports");
        Directory.CreateDirectory(exportsDir);
        RefreshRecentAuditLogExports(exportsDir);
        AuditLogHintText.Text = _recentAuditLogExports.Count == 0
            ? "Audit log export listesi guncellendi, dosya bulunmadi."
            : $"Audit log export listesi guncellendi ({_recentAuditLogExports.Count} dosya).";
    }

    private void AuditLogExportTypeFilterInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_isInitialized)
        {
            return;
        }

        var exportsDir = Path.Combine(AppPaths.Resolve().RootDirectory, "exports");
        Directory.CreateDirectory(exportsDir);
        RefreshRecentAuditLogExports(exportsDir);
        AuditLogHintText.Text = _recentAuditLogExports.Count == 0
            ? "Secili export tipi icin dosya bulunmadi."
            : $"Export listesi filtrelendi: {GetSelectedAuditLogExportTypeFilter()} ({_recentAuditLogExports.Count} dosya).";
    }

    private void ClearAuditLogExportsButton_Click(object sender, RoutedEventArgs e)
    {
        var exportsDir = Path.Combine(AppPaths.Resolve().RootDirectory, "exports");
        Directory.CreateDirectory(exportsDir);

        var files = Directory.GetFiles(exportsDir, "audit-log-*.*", SearchOption.TopDirectoryOnly);
        if (files.Length == 0)
        {
            RefreshRecentAuditLogExports(exportsDir);
            AuditLogHintText.Text = "Temizlenecek audit log export dosyasi bulunmadi.";
            return;
        }

        var deletedCount = 0;
        foreach (var file in files)
        {
            try
            {
                File.Delete(file);
                deletedCount++;
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                RefreshRecentAuditLogExports(exportsDir);
                AuditLogHintText.Text = $"Liste kismen temizlendi ({deletedCount}/{files.Length}). Son hata: {exception.Message}";
                return;
            }
        }

        _lastAuditLogExportPath = null;
        RefreshRecentAuditLogExports(exportsDir);
        AuditLogHintText.Text = $"Audit log export listesi temizlendi ({deletedCount} dosya silindi).";
    }

    private void DeleteSelectedAuditLogExportButton_Click(object sender, RoutedEventArgs e)
    {
        var exportsDir = Path.Combine(AppPaths.Resolve().RootDirectory, "exports");
        Directory.CreateDirectory(exportsDir);

        if (AuditLogRecentExportsInput.SelectedItem is not AuditLogExportItem item)
        {
            AuditLogHintText.Text = "Silinecek export dosyasi secili degil.";
            return;
        }

        if (!File.Exists(item.FilePath))
        {
            RefreshRecentAuditLogExports(exportsDir);
            AuditLogHintText.Text = "Secilen export dosyasi artik bulunamiyor.";
            return;
        }

        try
        {
            File.Delete(item.FilePath);
            if (string.Equals(_lastAuditLogExportPath, item.FilePath, StringComparison.OrdinalIgnoreCase))
            {
                _lastAuditLogExportPath = null;
            }

            RefreshRecentAuditLogExports(exportsDir);
            AuditLogHintText.Text = $"Secilen export silindi: {item.DisplayName}";
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
        {
            AuditLogHintText.Text = $"Secilen export silinemedi: {exception.Message}";
        }
    }

    private void TrimAuditLogExportsButton_Click(object sender, RoutedEventArgs e)
    {
        var exportsDir = Path.Combine(AppPaths.Resolve().RootDirectory, "exports");
        Directory.CreateDirectory(exportsDir);

        var filesToDelete = Directory.GetFiles(exportsDir, "audit-log-*.*", SearchOption.TopDirectoryOnly)
            .OrderByDescending(path => File.GetLastWriteTimeUtc(path))
            .Skip(5)
            .ToList();

        if (filesToDelete.Count == 0)
        {
            RefreshRecentAuditLogExports(exportsDir);
            AuditLogHintText.Text = "Temizlenecek eski audit log export dosyasi bulunmadi.";
            return;
        }

        var deletedCount = 0;
        foreach (var file in filesToDelete)
        {
            try
            {
                File.Delete(file);
                deletedCount++;
                if (string.Equals(_lastAuditLogExportPath, file, StringComparison.OrdinalIgnoreCase))
                {
                    _lastAuditLogExportPath = null;
                }
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                RefreshRecentAuditLogExports(exportsDir);
                AuditLogHintText.Text = $"Eski export temizligi kismen tamamlandi ({deletedCount}/{filesToDelete.Count}). Son hata: {exception.Message}";
                return;
            }
        }

        RefreshRecentAuditLogExports(exportsDir);
        AuditLogHintText.Text = $"Eski audit log export dosyalari temizlendi ({deletedCount} dosya silindi, son 5 korundu).";
    }

    private void ExportReportButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);

            var tabKey = _activeTab.ToString().ToLowerInvariant();
            var fileName = $"raporlar-{tabKey}-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx";
            var filePath = Path.Combine(exportsDir, fileName);

            var cfg = ReportMetaConfig.LoadOrDefault(paths.RootDirectory);
            var meta = new ExcelExportWriter.ReportMeta(
                AppTitle: cfg.AppTitle,
                InstitutionName: cfg.InstitutionName,
                ReportTitle: GetPdfReportTitle(),
                ReportPeriod: GetPdfReportPeriod(),
                ReportDate: DateTime.Today,
                CreatedBy: Environment.UserName,
                FilterText: GetPdfReportFilterText());

            var (pdfSummary, primary, _secondaryTitle, secondTable) = BuildPdfContent();
            var summary = pdfSummary.Select(s => new ExcelExportWriter.SummaryItem(s.Label, s.Value, s.Detail)).ToList();
            var primaryRows = primary.Rows.Select(r => (IReadOnlyList<object?>)r.Cast<object?>().ToArray()).ToList();

            // For some tabs, match the provided Excel templates on the main sheet,
            // and keep the existing export table on a second sheet to avoid losing information.
            if (_activeTab is ReportTab.Monthly or ReportTab.YearlyAll or ReportTab.Subscription or ReportTab.SubscriptionYearly or ReportTab.TypeYearly)
            {
                var (tplHeaders, tplRows) = _activeTab switch
                {
                    ReportTab.Monthly => BuildExcelMonthlyTemplateRows(),
                    ReportTab.YearlyAll => BuildExcelYearlyAllTemplateRows(),
                    ReportTab.Subscription => BuildExcelSubscriptionMonthlyTemplateRows(),
                    ReportTab.SubscriptionYearly => BuildExcelSubscriptionYearlyTemplateRows(),
                    _ => BuildExcelTypeYearlyTemplateRows(),
                };
                ExcelExportWriter.WriteReportWithTwoSheets(
                    filePath,
                    mainSheetName: "Rapor",
                    meta: meta,
                    summary: summary,
                    headers: tplHeaders,
                    rows: tplRows,
                    secondSheetName: "Detay",
                    secondHeaders: primary.Headers,
                    secondRows: primaryRows,
                    notes: GetExcelReportNotes());

                var templateHint = $"Excel yazÄ±ldÄ±: exports/{fileName}";
                MonthlyFilterHintText.Text = templateHint;
                TypeYearlyHintText.Text = templateHint;
                SubscriptionHintText.Text = templateHint;
                return;
            }

            if (secondTable is null)
            {
                ExcelExportWriter.WriteReportWithHeader(
                    filePath,
                    sheetName: "Rapor",
                    meta: meta,
                    summary: summary,
                    headers: primary.Headers,
                    rows: primaryRows,
                    notes: GetExcelReportNotes());
            }
            else
            {
                var secondRows = secondTable.Value.Rows.Select(r => (IReadOnlyList<object?>)r.Cast<object?>().ToArray()).ToList();
                ExcelExportWriter.WriteReportWithTwoSheets(
                    filePath,
                    mainSheetName: "Rapor",
                    meta: meta,
                    summary: summary,
                    headers: primary.Headers,
                    rows: primaryRows,
                    secondSheetName: secondTable.Value.Title,
                    secondHeaders: secondTable.Value.Headers,
                    secondRows: secondRows,
                    notes: GetExcelReportNotes());
            }

            var hint = $"Excel yazÄ±ldÄ±: exports/{fileName}";
            switch (_activeTab)
            {
                case ReportTab.Monthly:
                    MonthlyFilterHintText.Text = hint;
                    break;
                case ReportTab.Subscription:
                case ReportTab.SubscriptionYearly:
                    SubscriptionHintText.Text = hint;
                    break;
                case ReportTab.TypeYearly:
                case ReportTab.DocumentHealth:
                    TypeYearlyHintText.Text = hint;
                    break;
                default:
                    MonthlyFilterHintText.Text = hint;
                    break;
            }
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            // Raporlar ekranÄ±nda ortak bir status bandÄ± yok; ipucu alanÄ±nÄ± hata mesajÄ± iÃ§in kullanÄ±yoruz.
            var message = exception.Message;
            TypeYearlyHintText.Text = message;
            MonthlyFilterHintText.Text = message;
            SubscriptionHintText.Text = message;
        }
    }

    private void ExportReportPdfButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);

            var tabKey = _activeTab.ToString().ToLowerInvariant();
            var fileName = $"raporlar-{tabKey}-{DateTime.Now:yyyyMMdd-HHmmss}.pdf";
            var filePath = Path.Combine(exportsDir, fileName);

            var createdBy = Environment.UserName;
            var cfg = ReportMetaConfig.LoadOrDefault(paths.RootDirectory);
            var meta = new PdfReportWriter.ReportMeta(
                AppTitle: cfg.AppTitle,
                InstitutionName: cfg.InstitutionName,
                ReportTitle: GetPdfReportTitle(),
                ReportPeriod: GetPdfReportPeriod(),
                ReportDate: DateTime.Today,
                CreatedBy: createdBy,
                FilterText: string.Empty);

            // For template-aligned tabs, export the compact 7-column template view into PDF,
            // with a single GENEL TOPLAM row at the bottom.
            if (_activeTab is ReportTab.Monthly or ReportTab.YearlyAll or ReportTab.Subscription or ReportTab.SubscriptionYearly or ReportTab.TypeYearly)
            {
                // Reuse the existing PDF content builder for secondary title (when applicable).
                var (_, _, secondaryTitle, _) = BuildPdfContent();

                var (tplHeaders, tplRows) = _activeTab switch
                {
                    ReportTab.Monthly => BuildExcelMonthlyTemplateRows(),
                    ReportTab.YearlyAll => BuildExcelYearlyAllTemplateRows(),
                    ReportTab.Subscription => BuildExcelSubscriptionMonthlyTemplateRows(),
                    ReportTab.SubscriptionYearly => BuildExcelSubscriptionYearlyTemplateRows(),
                    _ => BuildExcelTypeYearlyTemplateRows(),
                };

                static string FormatCell(object? value)
                {
                    if (value is null)
                        return string.Empty;

                    var tr = CultureInfo.GetCultureInfo("tr-TR");
                    return value switch
                    {
                        DateTime dt => dt.ToString("dd.MM.yyyy", tr),
                        decimal d => d.ToString("0.00", tr),
                        double dbl => dbl.ToString("0.00", tr),
                        float f => f.ToString("0.00", tr),
                        int i => i.ToString(CultureInfo.InvariantCulture),
                        long l => l.ToString(CultureInfo.InvariantCulture),
                        _ => value.ToString() ?? string.Empty,
                    };
                }

                // Compute totals from the last two numeric columns (usage + amount) when present.
                decimal usageTotal = 0m;
                decimal amountTotal = 0m;
                foreach (var row in tplRows)
                {
                    if (row.Count >= 7)
                    {
                        if (row[5] is decimal u)
                            usageTotal += u;
                        else if (row[5] is double ud)
                            usageTotal += (decimal)ud;

                        if (row[6] is decimal a)
                            amountTotal += a;
                        else if (row[6] is double ad)
                            amountTotal += (decimal)ad;
                    }
                }

                var tr = CultureInfo.GetCultureInfo("tr-TR");
                var footerCells = new[]
                {
                    new PdfReportWriter.TableFooterCell("GENEL TOPLAM", ColumnSpan: Math.Max(1, tplHeaders.Count - 2), Bold: true, AlignRight: true),
                    new PdfReportWriter.TableFooterCell(usageTotal.ToString("0.000", tr), Bold: true, AlignRight: true),
                    new PdfReportWriter.TableFooterCell(amountTotal.ToString("0.00", tr), Bold: true, AlignRight: true),
                };

                // Column widths tuned for the 7-column template: year, month, subscription, date, invoice no, usage, amount.
                var weights = tplHeaders.Count == 7 ? new float[] { 1.0f, 1.4f, 2.4f, 1.6f, 3.2f, 1.6f, 1.8f } : null;
                var styles = tplHeaders.Count == 7
                    ? new[]
                    {
                        new PdfReportWriter.TableColumnStyle(AlignCenter: true),
                        new PdfReportWriter.TableColumnStyle(),
                        new PdfReportWriter.TableColumnStyle(),
                        new PdfReportWriter.TableColumnStyle(),
                        new PdfReportWriter.TableColumnStyle(),
                        new PdfReportWriter.TableColumnStyle(AlignRight: true),
                        new PdfReportWriter.TableColumnStyle(AlignRight: true),
                    }
                    : null;

                var rows = tplRows
                    .Select(r => (IReadOnlyList<string>)r.Select(FormatCell).ToArray())
                    .ToList();

                PdfReportWriter.WriteSimpleTableReport(
                    filePath,
                    meta,
                    summary: Array.Empty<PdfReportWriter.SummaryItem>(),
                    headers: tplHeaders.ToList(),
                    rows: rows,
                    notes: GetPdfReportDescription(cfg.InstitutionName),
                    secondaryTitle: secondaryTitle,
                    footerCells: footerCells,
                    columnWeights: weights,
                    columnStyles: styles);
            }
            else
            {
                var (summary, primary, secondaryTitle, secondTable) = BuildPdfContent();

                PdfReportWriter.WriteSimpleTableReport(
                    filePath,
                    meta,
                    Array.Empty<PdfReportWriter.SummaryItem>(),
                    primary.Headers,
                    primary.Rows,
                    notes: GetPdfReportDescription(cfg.InstitutionName),
                    secondaryTitle: secondaryTitle,
                    secondTable: secondTable);
            }

            SetPdfHint($"PDF yazÄ±ldÄ±: exports/{fileName}");
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            SetPdfHint(exception.Message);
        }
    }

    private void SetPdfHint(string message)
    {
        // Raporlar ekranÄ±nda ortak bir status bandÄ± yok; mevcut ipucu alanlarÄ±nÄ± kullanÄ±yoruz.
        TypeYearlyHintText.Text = message;
        MonthlyFilterHintText.Text = message;
        SubscriptionHintText.Text = message;
    }

    private string GetPdfReportTitle()
    {
        return _activeTab switch
        {
            ReportTab.Unpaid => "Ã–DENMEMÄ°Å FATURALAR RAPORU",
            ReportTab.Overdue => "GECÄ°KMÄ°Å FATURALAR RAPORU",
            ReportTab.Upcoming => "YAKLAÅAN Ã–DEMELER RAPORU",
            ReportTab.Monthly => "AYLIK FATURA RAPORU",
            ReportTab.YearlyAll => "YILLIK FATURA RAPORU",
            ReportTab.Subscription => "ABONELÄ°K AYLIK FATURA RAPORU",
            ReportTab.SubscriptionYearly => "ABONELÄ°K YILLIK FATURA RAPORU",
            ReportTab.TypeYearly => "TÃœR YILLIK FATURA RAPORU",
            ReportTab.DocumentHealth => "EVRAK KONTROL RAPORU",
            ReportTab.AuditLog => "ISLEM GECMISI RAPORU",
            _ => "RAPOR",
        };
    }

    private string GetPdfReportPeriod()
    {
        return _activeTab switch
        {
            ReportTab.Monthly => $"{_monthlyReport.Year:D4}/{_monthlyReport.Month:D2}",
            ReportTab.YearlyAll => $"{GetSelectedYear():D4}",
            ReportTab.Subscription => $"{_subscriptionComparison.Current.Year:D4}/{_subscriptionComparison.Current.Month:D2}",
            ReportTab.SubscriptionYearly => _subscriptionYearly.Year == 0 ? string.Empty : $"{_subscriptionYearly.Year}",
            ReportTab.TypeYearly => _typeYearly.Year == 0 ? string.Empty : $"{_typeYearly.Year}",
            ReportTab.AuditLog => "Sistem islem gecmisi",
            _ => string.Empty,
        };
    }

    private string? GetExcelReportNotes()
    {
        // Use a short human hint similar to the example Excel templates.
        var title = GetPdfReportTitle();
        var period = GetPdfReportPeriod();
        var filter = GetPdfReportFilterText();
        if (string.IsNullOrWhiteSpace(period))
        {
            return string.IsNullOrWhiteSpace(filter) ? $"{title}" : $"{title} ({filter})";
        }

        return string.IsNullOrWhiteSpace(filter) ? $"{title} ({period})" : $"{title} ({period} / {filter})";
    }

    private (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<object?>> Rows) BuildExcelMonthlyTemplateRows()
    {
        // Template reference: docs/references/fatura-excel-ornekler.xlsx (sheet: aylikfaturalar)
        // Keep column names aligned with the template.
        var headers = (IReadOnlyList<string>)new[]
        {
            "YÄ±l",
            "Ay",
            "Abone Bilgisi",
            "F. Tarihi",
            "Fatura NumarasÄ±",
            "KullanÄ±m M.",
            "Fatura Tutar",
        };

        var rows = _monthlyReport.Rows.Select(r => (IReadOnlyList<object?>)new object?[]
        {
            r.Invoice.InvoiceYear,
            TurkishCulture.DateTimeFormat.GetMonthName(r.Invoice.InvoiceMonth),
            r.Invoice.SubscriptionName,
            r.Invoice.InvoiceDate,
            r.Invoice.InvoiceNo,
            r.Invoice.UsageAmount,
            r.Invoice.Amount,
        }).ToList();

        return (headers, rows);
    }

    private (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<object?>> Rows) BuildExcelYearlyAllTemplateRows()
    {
        // Template reference: docs/references/fatura-excel-ornekler.xlsx (sheet: yillikfaturaraporu)
        var headers = (IReadOnlyList<string>)new[]
        {
            "YÄ±l",
            "Ay",
            "Abone Bilgisi",
            "F. Tarihi",
            "Fatura SayÄ±sÄ±",
            "KullanÄ±m M.",
            "Fatura Tutar",
        };

        if (_yearlyAllReport.Rows.Count == 0)
        {
            return (headers, Array.Empty<IReadOnlyList<object?>>());
        }

        var rows = _yearlyAllReport.Rows.Select(r =>
        {
            var i = r.Invoice;
            return (IReadOnlyList<object?>)new object?[]
            {
                i.InvoiceYear,
                TurkishCulture.DateTimeFormat.GetMonthName(i.InvoiceMonth),
                string.IsNullOrWhiteSpace(i.SubscriptionName) ? i.SubscriptionId.ToString(CultureInfo.InvariantCulture) : i.SubscriptionName,
                i.InvoiceDate,
                i.InvoiceNo,
                i.UsageAmount,
                i.Amount,
            };
        }).ToList();

        return (headers, rows);
    }

    private (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<object?>> Rows) BuildExcelTypeYearlyTemplateRows()
    {
        // Template reference: docs/references/fatura-excel-ornekler.xlsx (sheet: abonelikturuyillikfaturalar)
        var headers = (IReadOnlyList<string>)new[]
        {
            "YÄ±l",
            "Ay",
            "Abone Bilgisi",
            "F. Tarihi",
            "Fatura SayÄ±sÄ±",
            "KullanÄ±m M.",
            "Fatura Tutar",
        };

        if (_invoiceRepository is null)
        {
            return (headers, Array.Empty<IReadOnlyList<object?>>());
        }

        var year = GetSelectedTypeYearlyYear();
        var type = GetSelectedTypeYearlyInvoiceType();
        var typeId = type?.InvoiceTypeId;

        var list = _invoiceRepository.GetAll()
            .Where(i => i.InvoiceYear == year)
            .Where(i => typeId is null || i.InvoiceTypeId == typeId.Value)
            .OrderBy(i => i.InvoiceMonth)
            .ThenBy(i => i.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(i => i.InvoiceDate)
            .ThenBy(i => i.InvoiceNo, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        var rows = list.Select(i => (IReadOnlyList<object?>)new object?[]
        {
            i.InvoiceYear,
            TurkishCulture.DateTimeFormat.GetMonthName(i.InvoiceMonth),
            string.IsNullOrWhiteSpace(i.SubscriptionName) ? i.SubscriptionId.ToString(CultureInfo.InvariantCulture) : i.SubscriptionName,
            i.InvoiceDate,
            i.InvoiceNo,
            i.UsageAmount,
            i.Amount,
        }).ToList();

        return (headers, rows);
    }

    private (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<object?>> Rows) BuildExcelSubscriptionMonthlyTemplateRows()
    {
        // Template reference: docs/references/fatura-excel-ornekler.xlsx (sheet: abonelikaylikfaturalar)
        var headers = (IReadOnlyList<string>)new[]
        {
            "YÄ±l",
            "Ay",
            "Abone Bilgisi",
            "F. Tarihi",
            "Fatura NumarasÄ±",
            "KullanÄ±m M.",
            "Fatura Tutar",
        };

        if (_invoiceRepository is null)
        {
            return (headers, Array.Empty<IReadOnlyList<object?>>());
        }

        var subscriptionId = GetSelectedSubscriptionId();
        var year = GetSelectedSubscriptionYear();
        var month = GetSelectedSubscriptionMonth();

        var list = _invoiceRepository.GetAll()
            .Where(i => subscriptionId is null || i.SubscriptionId == subscriptionId.Value)
            .Where(i => i.InvoiceYear == year && i.InvoiceMonth == month)
            .OrderBy(i => i.InvoiceDate)
            .ThenBy(i => i.InvoiceNo, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        var rows = list.Select(i => (IReadOnlyList<object?>)new object?[]
        {
            i.InvoiceYear,
            TurkishCulture.DateTimeFormat.GetMonthName(i.InvoiceMonth),
            string.IsNullOrWhiteSpace(i.SubscriptionName) ? i.SubscriptionId.ToString(CultureInfo.InvariantCulture) : i.SubscriptionName,
            i.InvoiceDate,
            i.InvoiceNo,
            i.UsageAmount,
            i.Amount,
        }).ToList();

        return (headers, rows);
    }

    private (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<object?>> Rows) BuildExcelSubscriptionYearlyTemplateRows()
    {
        // Template reference: docs/references/fatura-excel-ornekler.xlsx (sheet: abonelikyillikfaturalar)
        var headers = (IReadOnlyList<string>)new[]
        {
            "YÄ±l",
            "Ay",
            "Abone Bilgisi",
            "F. Tarihi",
            "Fatura SayÄ±sÄ±",
            "KullanÄ±m M.",
            "Fatura Tutar",
        };

        if (_invoiceRepository is null)
        {
            return (headers, Array.Empty<IReadOnlyList<object?>>());
        }

        var subscriptionId = GetSelectedSubscriptionId();
        var year = GetSelectedSubscriptionYear();

        var list = _invoiceRepository.GetAll()
            .Where(i => subscriptionId is null || i.SubscriptionId == subscriptionId.Value)
            .Where(i => i.InvoiceYear == year)
            .OrderBy(i => i.InvoiceMonth)
            .ThenBy(i => i.InvoiceDate)
            .ThenBy(i => i.InvoiceNo, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        // Despite the "Fatura SayÄ±sÄ±" label in the template, the example data matches invoice no.
        var rows = list.Select(i => (IReadOnlyList<object?>)new object?[]
        {
            i.InvoiceYear,
            TurkishCulture.DateTimeFormat.GetMonthName(i.InvoiceMonth),
            string.IsNullOrWhiteSpace(i.SubscriptionName) ? i.SubscriptionId.ToString(CultureInfo.InvariantCulture) : i.SubscriptionName,
            i.InvoiceDate,
            i.InvoiceNo,
            i.UsageAmount,
            i.Amount,
        }).ToList();

        return (headers, rows);
    }

    private string GetPdfReportFilterText()
    {
        return _activeTab switch
        {
            ReportTab.Monthly => $"Tur: {(GetSelectedInvoiceTypeLabel() ?? "Tum Turler")}",
            ReportTab.YearlyAll => $"Tur: {(GetSelectedInvoiceTypeLabel() ?? "Tum Turler")}",
            ReportTab.Subscription => $"Abonelik: {(GetSelectedSubscriptionLabel() ?? string.Empty)}",
            ReportTab.SubscriptionYearly => $"Abonelik: {(GetSelectedSubscriptionLabel() ?? string.Empty)}",
            ReportTab.TypeYearly => $"Tur: {(GetSelectedTypeYearlyInvoiceType()?.Label ?? string.Empty)}",
            ReportTab.DocumentHealth => "Fatura + Odeme evrak kontrolu",
            ReportTab.AuditLog => GetAuditLogFilterText(),
            _ => string.Empty,
        };
    }

    private string GetPdfReportDescription(string institutionName)
    {
        var inst = string.IsNullOrWhiteSpace(institutionName) ? "Kurum" : institutionName.Trim();
        var tr = TurkishCulture;

        string? type = _activeTab switch
        {
            ReportTab.Monthly or ReportTab.YearlyAll => GetSelectedInvoiceTypeLabel(),
            ReportTab.TypeYearly => GetSelectedTypeYearlyInvoiceType()?.Label,
            _ => null,
        };

        var typeSuffix = string.IsNullOrWhiteSpace(type) || string.Equals(type, "TÃ¼m TÃ¼rler", StringComparison.OrdinalIgnoreCase)
            ? "tÃ¼m faturalarÄ±n"
            : $"tÃ¼m {type} faturalarÄ±nÄ±n";

        return _activeTab switch
        {
            ReportTab.Monthly => $"{inst} iÃ§in {GetSelectedYear():D4} {tr.DateTimeFormat.GetMonthName(GetSelectedMonth())} ayÄ±nda gelen {typeSuffix} listesidir.",
            ReportTab.YearlyAll => $"{inst} iÃ§in {GetSelectedYear():D4} yÄ±lÄ±nda gelen {typeSuffix} listesidir.",
            ReportTab.Subscription => $"{inst} iÃ§in {GetSelectedSubscriptionYear():D4} {tr.DateTimeFormat.GetMonthName(GetSelectedSubscriptionMonth())} ayÄ±nda {(GetSelectedSubscriptionLabel() ?? "seÃ§ili abonelik")} iÃ§in gelen tÃ¼m faturalarÄ±n listesidir.",
            ReportTab.SubscriptionYearly => $"{inst} iÃ§in {GetSelectedSubscriptionYear():D4} yÄ±lÄ±nda {(GetSelectedSubscriptionLabel() ?? "seÃ§ili abonelik")} iÃ§in gelen tÃ¼m faturalarÄ±n listesidir.",
            ReportTab.TypeYearly => $"{inst} iÃ§in {GetSelectedTypeYearlyYear():D4} yÄ±lÄ±nda gelen {typeSuffix} listesidir.",
            ReportTab.AuditLog => $"{inst} sisteminde kaydedilen islem gecmisi kayitlarinin listesidir.",
            _ => GetPdfReportFilterText(),
        };
    }

    private string? GetSelectedInvoiceTypeLabel()
    {
        return (MonthlyInvoiceTypeInput.SelectedItem as InvoiceTypeOption)?.Label;
    }

    private string? GetSelectedSubscriptionLabel()
    {
        return (SubscriptionInput.SelectedItem as SubscriptionOption)?.Label;
    }

    private string? GetSelectedAuditLogActionType()
    {
        return (AuditLogActionInput.SelectedItem as AuditLogActionOption)?.ActionType;
    }

    private string? GetSelectedAuditLogEntity()
    {
        return (AuditLogEntityInput.SelectedItem as AuditLogEntityOption)?.TableName;
    }

    private string? GetSelectedAuditLogUser()
    {
        return (AuditLogUserInput.SelectedItem as AuditLogUserOption)?.UserName;
    }

    private IReadOnlyList<AuditLog> GetFilteredAuditLogs()
    {
        var selectedAction = GetSelectedAuditLogActionType();
        var selectedEntity = GetSelectedAuditLogEntity();
        var selectedUser = GetSelectedAuditLogUser();
        var start = AuditLogStartDateInput.SelectedDate?.Date;
        var end = AuditLogEndDateInput.SelectedDate?.Date;
        var search = AuditLogSearchInput.Text?.Trim();

        return _auditLogs
            .Where(log => string.IsNullOrWhiteSpace(selectedAction) || string.Equals(log.ActionType, selectedAction, StringComparison.OrdinalIgnoreCase))
            .Where(log => string.IsNullOrWhiteSpace(selectedEntity) || string.Equals(log.TableName, selectedEntity, StringComparison.OrdinalIgnoreCase))
            .Where(log => string.IsNullOrWhiteSpace(selectedUser) || string.Equals(log.UserName, selectedUser, StringComparison.OrdinalIgnoreCase))
            .Where(log => start is null || log.CreatedAt.LocalDateTime.Date >= start.Value)
            .Where(log => end is null || log.CreatedAt.LocalDateTime.Date <= end.Value)
            .Where(log => string.IsNullOrWhiteSpace(search)
                || log.Description.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                || log.ActionType.Contains(search, StringComparison.OrdinalIgnoreCase)
                || log.TableName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || log.RecordId.ToString(CultureInfo.InvariantCulture).Contains(search, StringComparison.OrdinalIgnoreCase)
                || log.UserName.Contains(search, StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }

    private string GetAuditLogFilterText()
    {
        var parts = new List<string>();
        var action = AuditLogActionInput.SelectedItem as AuditLogActionOption;
        if (action is not null && !string.IsNullOrWhiteSpace(action.ActionType))
        {
            parts.Add($"Islem: {action.Label}");
        }

        var entity = AuditLogEntityInput.SelectedItem as AuditLogEntityOption;
        if (entity is not null && !string.IsNullOrWhiteSpace(entity.TableName))
        {
            parts.Add($"Varlik: {entity.Label}");
        }

        var user = AuditLogUserInput.SelectedItem as AuditLogUserOption;
        if (user is not null && !string.IsNullOrWhiteSpace(user.UserName))
        {
            parts.Add($"Kullanici: {user.Label}");
        }

        if (AuditLogStartDateInput.SelectedDate is DateTime start)
        {
            parts.Add($"Baslangic: {start:dd.MM.yyyy}");
        }

        if (AuditLogEndDateInput.SelectedDate is DateTime end)
        {
            parts.Add($"Bitis: {end:dd.MM.yyyy}");
        }

        if (!string.IsNullOrWhiteSpace(AuditLogSearchInput.Text))
        {
            parts.Add($"Ara: {AuditLogSearchInput.Text.Trim()}");
        }

        return parts.Count == 0 ? "Tum audit log kayitlari" : string.Join(" | ", parts);
    }

    private (IReadOnlyList<PdfReportWriter.SummaryItem> Summary,
        (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<string>> Rows) Primary,
        string? SecondaryTitle,
        (IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<string>> Rows, string Title)? SecondTable) BuildPdfContent()
    {
        if (_activeTab == ReportTab.Monthly)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam Fatura", _monthlyReport.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture), $"Toplam {FormatMoney(_monthlyReport.TotalAmount)}"),
                new("Ã–denen", FormatMoney(_monthlyReport.PaidTotal), $"PDF eksik {_monthlyReport.MissingPdfCount}"),
                new("Kalan", FormatMoney(_monthlyReport.RemainingTotal), $"Ã–denmemiÅŸ {_monthlyReport.UnpaidInvoiceCount}, GecikmiÅŸ {_monthlyReport.OverdueInvoiceCount}"),
            };

            var headers = new[]
            {
                "DÃ¶nem",
                "TÃ¼r",
                "Abonelik",
                "Kurum",
                "Durum",
                "Fatura Tarihi",
                "Son Ã–deme",
                "Fatura No",
                "Tutar",
                "KullanÄ±m",
                "Birim",
                "Ã–denen",
                "Kalan",
                "Fatura PDF",
                "Ã–deme Tarihi",
                "Ã–deme PDF",
                "AÃ§Ä±klama",
            };
            var rows = _monthlyReport.Rows.Select(r =>
            {
                var (paymentDate, paymentPdfState) = GetPaymentInfo(r.Invoice.Id);
                return (IReadOnlyList<string>)new[]
                {
                    r.Invoice.Period,
                    r.Invoice.InvoiceTypeName,
                    r.Invoice.SubscriptionName,
                    r.Invoice.InstitutionName,
                    r.Invoice.State,
                    r.Invoice.InvoiceDate.ToString("dd.MM.yyyy", TurkishCulture),
                    r.Invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
                    r.Invoice.InvoiceNo,
                    FormatMoney(r.Invoice.Amount),
                    r.Invoice.UsageAmount.ToString("N2", TurkishCulture),
                    r.Invoice.UsageUnit,
                    FormatMoney(r.Invoice.PaidAmount),
                    FormatMoney(r.Invoice.RemainingAmount),
                    r.PdfState,
                    paymentDate,
                    paymentPdfState,
                    r.Invoice.Description,
                };
            }).ToList();

            return (summary, (headers, rows), SecondaryTitle: null, SecondTable: null);
        }

        if (_activeTab == ReportTab.YearlyAll)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam (YÃ„Â±l)", _yearlyAllReport.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture), $"Toplam {FormatMoney(_yearlyAllReport.TotalAmount)}"),
                new("Ãƒâ€“denen", FormatMoney(_yearlyAllReport.PaidTotal), $"PDF eksik {_yearlyAllReport.MissingPdfCount}"),
                new("Kalan", FormatMoney(_yearlyAllReport.RemainingTotal), $"Ãƒâ€“denmemiÃ…Å¸ {_yearlyAllReport.UnpaidInvoiceCount}, GecikmiÃ…Å¸ {_yearlyAllReport.OverdueInvoiceCount}"),
            };

            var headers = new[]
            {
                "DÃƒÂ¶nem",
                "TÃƒÂ¼r",
                "Abonelik",
                "Kurum",
                "Durum",
                "Fatura Tarihi",
                "Son Ãƒâ€“deme",
                "Fatura No",
                "Tutar",
                "KullanÃ„Â±m",
                "Birim",
                "Ãƒâ€“denen",
                "Kalan",
                "Fatura PDF",
                "Ãƒâ€“deme Tarihi",
                "Ãƒâ€“deme PDF",
                "AÃƒÂ§Ã„Â±klama",
            };

            var rows = _yearlyAllReport.Rows.Select(r =>
            {
                var (paymentDate, paymentPdfState) = GetPaymentInfo(r.Invoice.Id);
                return (IReadOnlyList<string>)new[]
                {
                    r.Invoice.Period,
                    r.Invoice.InvoiceTypeName,
                    r.Invoice.SubscriptionName,
                    r.Invoice.InstitutionName,
                    r.Invoice.State,
                    r.Invoice.InvoiceDate.ToString("dd.MM.yyyy", TurkishCulture),
                    r.Invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
                    r.Invoice.InvoiceNo,
                    FormatMoney(r.Invoice.Amount),
                    r.Invoice.UsageAmount.ToString("N2", TurkishCulture),
                    r.Invoice.UsageUnit,
                    FormatMoney(r.Invoice.PaidAmount),
                    FormatMoney(r.Invoice.RemainingAmount),
                    r.PdfState,
                    paymentDate,
                    paymentPdfState,
                    r.Invoice.Description,
                };
            }).ToList();

            return (summary, (headers, rows), SecondaryTitle: null, SecondTable: null);
        }

        if (_activeTab == ReportTab.TypeYearly)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam (YÄ±l)", FormatMoney(_typeYearly.TotalAmount), $"{_typeYearly.TotalInvoiceCount} fatura, Ã–denen {FormatMoney(_typeYearly.PaidTotal)}, Kalan {FormatMoney(_typeYearly.RemainingTotal)}"),
                new("En YÃ¼ksek Ay", _typeYearly.HighestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_typeYearly.HighestMonth), _typeYearly.HighestMonth == 0 ? "-" : $"Toplam {FormatMoney(_typeYearly.HighestMonthTotal)}"),
                new("En DÃ¼ÅŸÃ¼k Ay", _typeYearly.LowestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_typeYearly.LowestMonth), _typeYearly.LowestMonth == 0 ? "-" : $"Toplam {FormatMoney(_typeYearly.LowestMonthTotal)}"),
            };

            var headers = new[] { "Ay", "Fatura", "Toplam", "Ã–denen", "Kalan", "Ã–denmemiÅŸ", "GecikmiÅŸ", "PDF Eksik" };
            var rows = _typeYearly.Months.Select(m => (IReadOnlyList<string>)new[]
            {
                TurkishCulture.DateTimeFormat.GetMonthName(m.Month),
                m.InvoiceCount.ToString(CultureInfo.InvariantCulture),
                FormatMoney(m.TotalAmount),
                FormatMoney(m.PaidTotal),
                FormatMoney(m.RemainingTotal),
                m.UnpaidInvoiceCount.ToString(CultureInfo.InvariantCulture),
                m.OverdueInvoiceCount.ToString(CultureInfo.InvariantCulture),
                m.MissingPdfCount.ToString(CultureInfo.InvariantCulture),
            }).ToList();

            var distHeaders = new[] { "Abonelik", "Kurum", "Fatura", "Toplam", "Ã–denen", "Kalan", "PDF Eksik" };
            var distRows = _typeYearly.Distribution.Select(d => (IReadOnlyList<string>)new[]
            {
                d.SubscriptionName,
                d.InstitutionName,
                d.InvoiceCount.ToString(CultureInfo.InvariantCulture),
                FormatMoney(d.TotalAmount),
                FormatMoney(d.PaidTotal),
                FormatMoney(d.RemainingTotal),
                d.MissingPdfCount.ToString(CultureInfo.InvariantCulture),
            }).ToList();

            var second = (Headers: (IReadOnlyList<string>)distHeaders, Rows: (IReadOnlyList<IReadOnlyList<string>>)distRows, Title: "Abonelik DaÄŸÄ±lÄ±mÄ±");
            var secondaryTitle = _typeYearly.Year == 0 ? null : $"{_typeYearly.InvoiceTypeName} / {_typeYearly.Year}";
            return (summary, (headers, rows), secondaryTitle, second);
        }

        if (_activeTab == ReportTab.SubscriptionYearly)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam (YÄ±l)", FormatMoney(_subscriptionYearly.TotalAmount), $"{_subscriptionYearly.TotalInvoiceCount} fatura, Ã–denen {FormatMoney(_subscriptionYearly.PaidTotal)}, Kalan {FormatMoney(_subscriptionYearly.RemainingTotal)}"),
                new("En YÃ¼ksek Ay", _subscriptionYearly.HighestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.HighestMonth), _subscriptionYearly.HighestMonth == 0 ? "-" : $"Toplam {FormatMoney(_subscriptionYearly.HighestMonthTotal)}"),
                new("En DÃ¼ÅŸÃ¼k Ay", _subscriptionYearly.LowestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.LowestMonth), _subscriptionYearly.LowestMonth == 0 ? "-" : $"Toplam {FormatMoney(_subscriptionYearly.LowestMonthTotal)}"),
            };

            var headers = new[] { "Ay", "Fatura", "Toplam", "Ã–denen", "Kalan", "Ã–denmemiÅŸ", "GecikmiÅŸ", "PDF Eksik" };
            var rows = _subscriptionYearly.Months.Select(m => (IReadOnlyList<string>)new[]
            {
                TurkishCulture.DateTimeFormat.GetMonthName(m.Month),
                m.InvoiceCount.ToString(CultureInfo.InvariantCulture),
                FormatMoney(m.TotalAmount),
                FormatMoney(m.PaidTotal),
                FormatMoney(m.RemainingTotal),
                m.UnpaidInvoiceCount.ToString(CultureInfo.InvariantCulture),
                m.OverdueInvoiceCount.ToString(CultureInfo.InvariantCulture),
                m.MissingPdfCount.ToString(CultureInfo.InvariantCulture),
            }).ToList();

            return (summary, (headers, rows), SecondaryTitle: GetSelectedSubscriptionLabel(), SecondTable: null);
        }

        if (_activeTab == ReportTab.Subscription)
        {
            var current = _subscriptionComparison.Current;
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam Fatura", current.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture), $"Toplam {FormatMoney(current.TotalAmount)}"),
                new("Ã–denen", FormatMoney(current.PaidTotal), $"Ã–nceki ay {FormatMoney(_subscriptionComparison.Previous.PaidTotal)}"),
                new("Kalan", FormatMoney(current.RemainingTotal), $"Ã–nceki ay {FormatMoney(_subscriptionComparison.Previous.RemainingTotal)}"),
            };

            var headers = new[] { "DÃ¶nem", "TÃ¼r", "Abonelik", "Kurum", "Durum", "Fatura No", "Son Ã–deme", "Tutar", "Ã–denen", "Kalan", "PDF" };
            var rows = current.Rows.Select(r => (IReadOnlyList<string>)new[]
            {
                r.Invoice.Period,
                r.Invoice.InvoiceTypeName,
                r.Invoice.SubscriptionName,
                r.Invoice.InstitutionName,
                r.Invoice.State,
                r.Invoice.InvoiceNo,
                r.Invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
                FormatMoney(r.Invoice.Amount),
                FormatMoney(r.Invoice.PaidAmount),
                FormatMoney(r.Invoice.RemainingAmount),
                r.PdfState,
            }).ToList();

            return (summary, (headers, rows), SecondaryTitle: GetSelectedSubscriptionLabel(), SecondTable: null);
        }

        if (_activeTab == ReportTab.DocumentHealth)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Fatura PDF", (_documentHealth.InvoiceNoPdfCount + _documentHealth.InvoiceMissingFileCount).ToString(CultureInfo.InvariantCulture), $"Yok {_documentHealth.InvoiceNoPdfCount}, KayÄ±p {_documentHealth.InvoiceMissingFileCount}"),
                new("Ã–deme PDF", (_documentHealth.PaymentNoPdfCount + _documentHealth.PaymentMissingFileCount).ToString(CultureInfo.InvariantCulture), $"Yok {_documentHealth.PaymentNoPdfCount}, KayÄ±p {_documentHealth.PaymentMissingFileCount}"),
                new("AynÄ± Hash", (_documentHealth.DuplicateInvoiceHashItemCount + _documentHealth.DuplicatePaymentHashItemCount).ToString(CultureInfo.InvariantCulture), $"Fatura {_documentHealth.DuplicateInvoiceHashItemCount}, Ã–deme {_documentHealth.DuplicatePaymentHashItemCount}"),
            };

            var headers = new[] { "UyarÄ±", "Tip", "DÃ¶nem/Tarih", "TÃ¼r", "Abonelik", "Kurum", "Tutar", "PDF", "Yol", "Hash", "Not" };
            var rows = _documentHealth.Issues.Select(i => (IReadOnlyList<string>)new[]
            {
                i.IssueType,
                i.EntityType,
                i.PeriodOrDate,
                i.InvoiceTypeName,
                i.SubscriptionName,
                i.InstitutionName,
                FormatMoney(i.Amount),
                i.PdfState,
                i.PdfFilePath,
                i.PdfSha256Hash,
                i.Note,
            }).ToList();

            return (summary, (headers, rows), SecondaryTitle: null, SecondTable: null);
        }

        if (_activeTab == ReportTab.Consistency)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("ERROR", _consistency.ErrorCount.ToString(CultureInfo.InvariantCulture), "Kritik tutarsÄ±zlÄ±k"),
                new("WARN", _consistency.WarningCount.ToString(CultureInfo.InvariantCulture), "UyarÄ±"),
                new("Toplam", _consistency.TotalCount.ToString(CultureInfo.InvariantCulture), "Issue"),
            };

            var headers = new[] { "Seviye", "Kod", "VarlÄ±k", "Id", "Mesaj" };
            var rows = _consistency.Issues.Select(i => (IReadOnlyList<string>)new[]
            {
                i.Severity,
                i.Code,
                i.Entity,
                i.EntityId?.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
                i.Message,
            }).ToList();

            return (summary, (headers, rows), SecondaryTitle: null, SecondTable: null);
        }

        if (_activeTab == ReportTab.AuditLog)
        {
            var filteredLogs = GetFilteredAuditLogs();
            var today = DateTime.Today;
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam Kayit", filteredLogs.Count.ToString(CultureInfo.InvariantCulture), $"Bugun {filteredLogs.Count(x => x.CreatedAt.LocalDateTime.Date == today)} kayit"),
                new("Islem Turu", filteredLogs.Select(x => x.ActionType).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString(CultureInfo.InvariantCulture), "Farkli aksiyon sayisi"),
                new("Varlik", filteredLogs.Select(x => x.TableName).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString(CultureInfo.InvariantCulture), "Kayit tutulan tablo sayisi"),
            };

            var headers = new[] { "Tarih", "Islem", "Varlik", "Kayit Id", "Aciklama", "Kullanici" };
            var rows = filteredLogs.Select(log => (IReadOnlyList<string>)new[]
            {
                log.CreatedAt == DateTimeOffset.MinValue
                    ? string.Empty
                    : log.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm", TurkishCulture),
                FormatAuditActionLabel(log.ActionType),
                FormatAuditTableLabel(log.TableName),
                log.RecordId.ToString(CultureInfo.InvariantCulture),
                log.Description,
                string.IsNullOrWhiteSpace(log.UserName) ? "system" : log.UserName,
            }).ToList();

            return (summary, (headers, rows), SecondaryTitle: null, SecondTable: null);
        }

        // Default: actionable lists
        var actionableItems = _activeTab switch
        {
            ReportTab.Overdue => _report.Overdue,
            ReportTab.Upcoming => _report.Upcoming,
            _ => _report.Unpaid,
        };
        var defaultSummary = new List<PdfReportWriter.SummaryItem>
        {
            new("Toplam", actionableItems.Count.ToString(CultureInfo.InvariantCulture), string.Empty),
            new("Kalan", FormatMoney(actionableItems.Sum(x => x.Invoice.RemainingAmount)), string.Empty),
            new("PDF Eksik", actionableItems.Count(x => x.PdfState != "PDF Var").ToString(CultureInfo.InvariantCulture), string.Empty),
        };

        // Align actionable exports with the Excel/PDF report plan (richer invoice + payment columns).
        var defaultHeaders = new[]
        {
            "DÃ¶nem",
            "TÃ¼r",
            "Abonelik",
            "Kurum",
            "Durum",
            "Fatura Tarihi",
            "Son Ã–deme",
            "Fatura No",
            "Tutar",
            "KullanÄ±m",
            "Birim",
            "Ã–denen",
            "Kalan",
            "Fatura PDF",
            "Ã–deme Tarihi",
            "Ã–deme PDF",
            "AÃ§Ä±klama",
        };

        var defaultRows = actionableItems.Select(i =>
        {
            var (paymentDate, paymentPdfState) = GetPaymentInfo(i.Invoice.Id);
            return (IReadOnlyList<string>)new[]
            {
                i.Invoice.Period,
                i.Invoice.InvoiceTypeName,
                i.Invoice.SubscriptionName,
                i.Invoice.InstitutionName,
                i.Invoice.State,
                i.Invoice.InvoiceDate.ToString("dd.MM.yyyy", TurkishCulture),
                i.Invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
                i.Invoice.InvoiceNo,
                FormatMoney(i.Invoice.Amount),
                i.Invoice.UsageAmount.ToString("N2", TurkishCulture),
                i.Invoice.UsageUnit,
                FormatMoney(i.Invoice.PaidAmount),
                FormatMoney(i.Invoice.RemainingAmount),
                i.PdfState,
                paymentDate,
                paymentPdfState,
                i.Invoice.Description,
            };
        }).ToList();

        return (defaultSummary, (defaultHeaders, defaultRows), SecondaryTitle: null, SecondTable: null);
    }

    private (string PaymentDate, string PaymentPdfState) GetPaymentInfo(long invoiceId)
    {
        if (_paymentRepository is null)
        {
            return (string.Empty, string.Empty);
        }

        if (!_paymentsByInvoice.TryGetValue(invoiceId, out var list) || list.Count == 0)
        {
            return (string.Empty, "PDF Yok");
        }

        var latest = list[0];
        var latestDate = latest.PaymentDate.ToString("dd.MM.yyyy", TurkishCulture);

        var anyHasPdf = list.Any(p => p.HasPdf);
        if (!anyHasPdf)
        {
            return (latestDate, "PDF Yok");
        }

        var anyMissing = list.Any(p => _paymentRepository.IsPdfMissing(p));
        return (latestDate, anyMissing ? "PDF KayÄ±p" : "PDF Var");
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

        _yearlyAllReport = YearlyInvoiceListReportCalculator.Calculate(
            invoices,
            GetSelectedYear(),
            GetSelectedInvoiceTypeId(),
            DateTime.Today,
            invoice => _invoiceRepository.IsPdfMissing(invoice));

        if (_activeTab is ReportTab.Monthly or ReportTab.YearlyAll)
        {
            ApplyTab(_activeTab);
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
            _subscriptionYearly = SubscriptionYearlyReport.Empty;
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

            _subscriptionYearly = SubscriptionYearlyReportCalculator.Calculate(
                invoices,
                selectedSubscriptionId.Value,
                GetSelectedSubscriptionYear(),
                DateTime.Today,
                invoice => _invoiceRepository.IsPdfMissing(invoice));
        }

        if (_activeTab == ReportTab.Subscription)
        {
            ApplyTab(ReportTab.Subscription);
        }
        else if (_activeTab == ReportTab.SubscriptionYearly)
        {
            ApplyTab(ReportTab.SubscriptionYearly);
        }
    }

    private void TypeYearlyFilter_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (_isRefreshingTypeYearlyFilters)
        {
            return;
        }

        if (_invoiceRepository is null)
        {
            return;
        }

        var selectedType = GetSelectedTypeYearlyInvoiceType();
        if (selectedType is null || selectedType.InvoiceTypeId is null)
        {
            _typeYearly = InvoiceTypeYearlyReport.Empty;
        }
        else
        {
            var invoices = _invoiceRepository.GetAll();
            _typeYearly = InvoiceTypeYearlyReportCalculator.Calculate(
                invoices,
                selectedType.InvoiceTypeId.Value,
                selectedType.Label,
                GetSelectedTypeYearlyYear(),
                DateTime.Today,
                invoice => _invoiceRepository.IsPdfMissing(invoice));
        }

        if (_activeTab == ReportTab.TypeYearly)
        {
            ApplyTab(ReportTab.TypeYearly);
        }
    }

    private void ApplyTab(ReportTab tab)
    {
        _activeTab = tab;

        SetTabActive(UnpaidTabButton, tab == ReportTab.Unpaid);
        SetTabActive(OverdueTabButton, tab == ReportTab.Overdue);
        SetTabActive(UpcomingTabButton, tab == ReportTab.Upcoming);
        SetTabActive(MonthlyTabButton, tab == ReportTab.Monthly);
        SetTabActive(YearlyTabButton, tab == ReportTab.YearlyAll);
        SetTabActive(SubscriptionTabButton, tab == ReportTab.Subscription);
        SetTabActive(SubscriptionYearlyTabButton, tab == ReportTab.SubscriptionYearly);
        SetTabActive(TypeYearlyTabButton, tab == ReportTab.TypeYearly);
        SetTabActive(DocumentHealthTabButton, tab == ReportTab.DocumentHealth);
        SetTabActive(ConsistencyTabButton, tab == ReportTab.Consistency);
        SetTabActive(AuditLogTabButton, tab == ReportTab.AuditLog);

        MonthlyFilterPanel.Visibility = tab is ReportTab.Monthly or ReportTab.YearlyAll ? Visibility.Visible : Visibility.Collapsed;
        MonthlyMonthInput.Visibility = tab == ReportTab.YearlyAll ? Visibility.Collapsed : Visibility.Visible;
        SubscriptionFilterPanel.Visibility = tab is ReportTab.Subscription or ReportTab.SubscriptionYearly
            ? Visibility.Visible
            : Visibility.Collapsed;
        SubscriptionMonthInput.Visibility = tab == ReportTab.Subscription ? Visibility.Visible : Visibility.Collapsed;
        TypeYearlyFilterPanel.Visibility = tab == ReportTab.TypeYearly ? Visibility.Visible : Visibility.Collapsed;
        AuditLogFilterPanel.Visibility = tab == ReportTab.AuditLog ? Visibility.Visible : Visibility.Collapsed;
        AuditLogDetailPanel.Visibility = Visibility.Collapsed;

        var items = tab switch
        {
            ReportTab.Overdue => _report.Overdue,
            ReportTab.Upcoming => _report.Upcoming,
            ReportTab.Monthly => Array.Empty<ActionableInvoice>(),
            ReportTab.YearlyAll => Array.Empty<ActionableInvoice>(),
            ReportTab.Subscription => Array.Empty<ActionableInvoice>(),
            ReportTab.SubscriptionYearly => Array.Empty<ActionableInvoice>(),
            ReportTab.TypeYearly => Array.Empty<ActionableInvoice>(),
            ReportTab.DocumentHealth => Array.Empty<ActionableInvoice>(),
            ReportTab.Consistency => Array.Empty<ActionableInvoice>(),
            ReportTab.AuditLog => Array.Empty<ActionableInvoice>(),
            _ => _report.Unpaid,
        };

        if (tab == ReportTab.Monthly)
        {
            ApplyMonthlyTiles();
            ReportGrid.ItemsSource = _monthlyReport.Rows.Select(ToMonthlyRow).ToList();
            ReportGrid.Visibility = Visibility.Visible;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.YearlyAll)
        {
            ApplyYearlyAllTiles();
            ReportGrid.ItemsSource = _yearlyAllReport.Rows.Select(ToMonthlyRow).ToList();

            ReportGrid.Visibility = Visibility.Visible;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.Subscription)
        {
            ApplySubscriptionTiles();
            ReportGrid.ItemsSource = _subscriptionComparison.Current.Rows.Select(ToMonthlyRow).ToList();
            ReportGrid.Visibility = Visibility.Visible;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.SubscriptionYearly)
        {
            ApplySubscriptionYearlyTiles();
            YearlyGrid.ItemsSource = _subscriptionYearly.Months.Select(ToYearlyRow).ToList();
            ReportGrid.Visibility = Visibility.Collapsed;
            YearlyGrid.Visibility = Visibility.Visible;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.TypeYearly)
        {
            ApplyTypeYearlyTiles();
            YearlyGrid.ItemsSource = _typeYearly.Months.Select(ToYearlyRow).ToList();
            DistributionGrid.ItemsSource = _typeYearly.Distribution.Select(ToDistributionRow).ToList();
            ReportGrid.Visibility = Visibility.Collapsed;
            YearlyGrid.Visibility = Visibility.Visible;
            DistributionGrid.Visibility = Visibility.Visible;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.DocumentHealth)
        {
            ApplyDocumentHealthTiles();
            DocumentHealthGrid.ItemsSource = _documentHealth.Issues.Select(ToDocumentHealthRow).ToList();
            ReportGrid.Visibility = Visibility.Collapsed;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Visible;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.Consistency)
        {
            ApplyConsistencyTiles();
            ConsistencyGrid.ItemsSource = _consistency.Issues.ToList();
            ReportGrid.Visibility = Visibility.Collapsed;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Visible;
            AuditLogGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.AuditLog)
        {
            var rows = GetFilteredAuditLogs().Select(ToAuditLogRow).ToList();
            ApplyAuditLogTiles();
            RefreshRecentAuditLogExports(Path.Combine(AppPaths.Resolve().RootDirectory, "exports"));
            AuditLogGrid.ItemsSource = rows;
            if (_selectedAuditLog is not null)
            {
                var matchingRow = rows.FirstOrDefault(row => row.Source.Id == _selectedAuditLog.Id);
                AuditLogGrid.SelectedItem = matchingRow;
                ApplyAuditLogDetail(matchingRow?.Source);
                if (matchingRow is not null)
                {
                    FocusAuditLogRow(matchingRow);
                }
            }
            else if (rows.Count > 0)
            {
                AuditLogGrid.SelectedItem = rows[0];
                ApplyAuditLogDetail(rows[0].Source);
            }
            else
            {
                AuditLogGrid.SelectedItem = null;
                ApplyAuditLogDetail(null);
            }
            ReportGrid.Visibility = Visibility.Collapsed;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Visible;
            AuditLogDetailPanel.Visibility = rows.Count == 0 ? Visibility.Collapsed : AuditLogDetailPanel.Visibility;
        }
        else
        {
            ApplyActionableTiles();
            ReportGrid.ItemsSource = items.Select(ToActionableRow).ToList();
            ReportGrid.Visibility = Visibility.Visible;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
            ConsistencyGrid.Visibility = Visibility.Collapsed;
            AuditLogGrid.Visibility = Visibility.Collapsed;
            AuditLogDetailPanel.Visibility = Visibility.Collapsed;
        }
    }

    private string GetInvoicePdfState(Invoice invoice)
    {
        if (!invoice.HasPdf)
        {
            return "PDF Yok";
        }

        if (_invoiceRepository is null)
        {
            return "PDF Var";
        }

        return _invoiceRepository.IsPdfMissing(invoice) ? "PDF KayÄ±p" : "PDF Var";
    }

    private static void SetTabActive(Button button, bool isActive)
    {
        button.Foreground = isActive ? Brushes.White : new SolidColorBrush(Color.FromRgb(51, 65, 85));
        button.Background = isActive ? new SolidColorBrush(Color.FromRgb(37, 99, 235)) : new SolidColorBrush(Color.FromRgb(231, 238, 246));
        button.BorderThickness = isActive ? new Thickness(0) : new Thickness(1);
    }

    private void ApplyActionableTiles()
    {
        Tile1LabelText.Text = "Ã–denmemiÅŸ";
        Tile1ValueText.Text = _report.Unpaid.Count.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Kalan {FormatMoney(_report.UnpaidRemainingTotal)}";

        Tile2LabelText.Text = "GecikmiÅŸ";
        Tile2ValueText.Text = _report.Overdue.Count.ToString(CultureInfo.InvariantCulture);
        Tile2DetailText.Text = $"Kalan {FormatMoney(_report.OverdueRemainingTotal)}";

        Tile3LabelText.Text = "YaklaÅŸan (7 gÃ¼n)";
        Tile3ValueText.Text = _report.Upcoming.Count.ToString(CultureInfo.InvariantCulture);
        Tile3DetailText.Text = $"Kalan {FormatMoney(_report.UpcomingRemainingTotal)}";

        MonthlyFilterHintText.Text = string.Empty;
    }

    private void ApplyMonthlyTiles()
    {
        Tile1LabelText.Text = "Toplam Fatura";
        Tile1ValueText.Text = _monthlyReport.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Toplam {FormatMoney(_monthlyReport.TotalAmount)}";

        Tile2LabelText.Text = "Ã–denen";
        Tile2ValueText.Text = FormatMoney(_monthlyReport.PaidTotal);
        Tile2DetailText.Text = $"PDF eksik {_monthlyReport.MissingPdfCount}";

        Tile3LabelText.Text = "Kalan";
        Tile3ValueText.Text = FormatMoney(_monthlyReport.RemainingTotal);
        Tile3DetailText.Text = $"Ã–denmemiÅŸ {_monthlyReport.UnpaidInvoiceCount}, GecikmiÅŸ {_monthlyReport.OverdueInvoiceCount}";

        MonthlyFilterHintText.Text = $"{_monthlyReport.Year:D4}/{_monthlyReport.Month:D2} dÃ¶nemi listeleniyor";
        SubscriptionHintText.Text = string.Empty;
    }

    private void ApplyYearlyAllTiles()
    {
        var year = _yearlyAllReport.Year == 0 ? GetSelectedYear() : _yearlyAllReport.Year;

        Tile1LabelText.Text = "Toplam (YÃ„Â±l)";
        Tile1ValueText.Text = _yearlyAllReport.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Toplam {FormatMoney(_yearlyAllReport.TotalAmount)}";

        Tile2LabelText.Text = "Ãƒâ€“denen";
        Tile2ValueText.Text = FormatMoney(_yearlyAllReport.PaidTotal);
        Tile2DetailText.Text = $"PDF eksik {_yearlyAllReport.MissingPdfCount}";

        Tile3LabelText.Text = "Kalan";
        Tile3ValueText.Text = FormatMoney(_yearlyAllReport.RemainingTotal);
        Tile3DetailText.Text = $"Ãƒâ€“denmemiÃ…Å¸ {_yearlyAllReport.UnpaidInvoiceCount}, GecikmiÃ…Å¸ {_yearlyAllReport.OverdueInvoiceCount}";

        MonthlyFilterHintText.Text = $"{year} yÃ„Â±lÃ„Â± listeleniyor";
        SubscriptionHintText.Text = string.Empty;
    }

    private void ApplySubscriptionTiles()
    {
        var current = _subscriptionComparison.Current;
        var previous = _subscriptionComparison.Previous;

        Tile1LabelText.Text = "Toplam Fatura";
        Tile1ValueText.Text = current.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Toplam {FormatMoney(current.TotalAmount)} ({FormatDelta(_subscriptionComparison.TotalAmountDelta)})";

        Tile2LabelText.Text = "Ã–denen";
        Tile2ValueText.Text = FormatMoney(current.PaidTotal);
        Tile2DetailText.Text = $"Ã–nceki ay {FormatMoney(previous.PaidTotal)} ({FormatDelta(_subscriptionComparison.PaidDelta)})";

        Tile3LabelText.Text = "Kalan";
        Tile3ValueText.Text = FormatMoney(current.RemainingTotal);
        Tile3DetailText.Text = $"Ã–nceki ay {FormatMoney(previous.RemainingTotal)} ({FormatDelta(_subscriptionComparison.RemainingDelta)})";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = $"{current.Year:D4}/{current.Month:D2} dÃ¶nemi (Ã¶nceki ay karÅŸÄ±laÅŸtÄ±rmalÄ±)";
    }

    private void ApplySubscriptionYearlyTiles()
    {
        var year = _subscriptionYearly.Year;
        Tile1LabelText.Text = "Toplam (YÄ±l)";
        Tile1ValueText.Text = FormatMoney(_subscriptionYearly.TotalAmount);
        Tile1DetailText.Text = $"{_subscriptionYearly.TotalInvoiceCount} fatura, Ã–denen {FormatMoney(_subscriptionYearly.PaidTotal)}, Kalan {FormatMoney(_subscriptionYearly.RemainingTotal)}";

        Tile2LabelText.Text = "En YÃ¼ksek Ay";
        Tile2ValueText.Text = _subscriptionYearly.HighestMonth == 0
            ? "-"
            : $"{TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.HighestMonth)}";
        Tile2DetailText.Text = _subscriptionYearly.HighestMonth == 0
            ? "-"
            : $"Toplam {FormatMoney(_subscriptionYearly.HighestMonthTotal)}";

        Tile3LabelText.Text = "En DÃ¼ÅŸÃ¼k Ay";
        Tile3ValueText.Text = _subscriptionYearly.LowestMonth == 0
            ? "-"
            : $"{TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.LowestMonth)}";
        Tile3DetailText.Text = _subscriptionYearly.LowestMonth == 0
            ? "-"
            : $"Toplam {FormatMoney(_subscriptionYearly.LowestMonthTotal)}";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = year == 0 ? string.Empty : $"{year} yÄ±lÄ± (12 ay)";
    }

    private void ApplyTypeYearlyTiles()
    {
        Tile1LabelText.Text = "Toplam (YÄ±l)";
        Tile1ValueText.Text = FormatMoney(_typeYearly.TotalAmount);
        Tile1DetailText.Text = $"{_typeYearly.TotalInvoiceCount} fatura, Ã–denen {FormatMoney(_typeYearly.PaidTotal)}, Kalan {FormatMoney(_typeYearly.RemainingTotal)}";

        Tile2LabelText.Text = "En YÃ¼ksek Ay";
        Tile2ValueText.Text = _typeYearly.HighestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_typeYearly.HighestMonth);
        Tile2DetailText.Text = _typeYearly.HighestMonth == 0 ? "-" : $"Toplam {FormatMoney(_typeYearly.HighestMonthTotal)}";

        Tile3LabelText.Text = "En DÃ¼ÅŸÃ¼k Ay";
        Tile3ValueText.Text = _typeYearly.LowestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_typeYearly.LowestMonth);
        Tile3DetailText.Text = _typeYearly.LowestMonth == 0 ? "-" : $"Toplam {FormatMoney(_typeYearly.LowestMonthTotal)}";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = string.Empty;
        TypeYearlyHintText.Text = _typeYearly.Year == 0 ? string.Empty : $"{_typeYearly.InvoiceTypeName} / {_typeYearly.Year}";
    }

    private void ApplyDocumentHealthTiles()
    {
        Tile1LabelText.Text = "Fatura PDF";
        Tile1ValueText.Text = (_documentHealth.InvoiceNoPdfCount + _documentHealth.InvoiceMissingFileCount).ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = $"Yok {_documentHealth.InvoiceNoPdfCount}, KayÄ±p {_documentHealth.InvoiceMissingFileCount}";

        Tile2LabelText.Text = "Ã–deme PDF";
        Tile2ValueText.Text = (_documentHealth.PaymentNoPdfCount + _documentHealth.PaymentMissingFileCount).ToString(CultureInfo.InvariantCulture);
        Tile2DetailText.Text = $"Yok {_documentHealth.PaymentNoPdfCount}, KayÄ±p {_documentHealth.PaymentMissingFileCount}";

        Tile3LabelText.Text = "AynÄ± Hash";
        Tile3ValueText.Text = (_documentHealth.DuplicateInvoiceHashItemCount + _documentHealth.DuplicatePaymentHashItemCount).ToString(CultureInfo.InvariantCulture);
        Tile3DetailText.Text = $"Fatura {_documentHealth.DuplicateInvoiceHashItemCount}, Ã–deme {_documentHealth.DuplicatePaymentHashItemCount}";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = string.Empty;
        TypeYearlyHintText.Text = string.Empty;
    }

    private void ApplyConsistencyTiles()
    {
        Tile1LabelText.Text = "ERROR";
        Tile1ValueText.Text = _consistency.ErrorCount.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = "Kritik tutarsizlik";

        Tile2LabelText.Text = "WARN";
        Tile2ValueText.Text = _consistency.WarningCount.ToString(CultureInfo.InvariantCulture);
        Tile2DetailText.Text = "Uyari";

        Tile3LabelText.Text = "Toplam";
        Tile3ValueText.Text = _consistency.TotalCount.ToString(CultureInfo.InvariantCulture);
        Tile3DetailText.Text = "Issue";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = string.Empty;
        TypeYearlyHintText.Text = string.Empty;
    }

    private void ApplyAuditLogTiles()
    {
        var filteredLogs = GetFilteredAuditLogs();
        var todayCount = filteredLogs.Count(x => x.CreatedAt.LocalDateTime.Date == DateTime.Today);
        var actionTypeCount = filteredLogs.Select(x => x.ActionType).Distinct(StringComparer.OrdinalIgnoreCase).Count();
        var entityNames = filteredLogs
            .Select(x => FormatAuditTableLabel(x.TableName))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(3)
            .ToList();

        Tile1LabelText.Text = "Toplam Kayit";
        Tile1ValueText.Text = filteredLogs.Count.ToString(CultureInfo.InvariantCulture);
        Tile1DetailText.Text = filteredLogs.Count == 0
            ? "Kayit bulunmuyor"
            : $"Son kayit {(filteredLogs[0].CreatedAt == DateTimeOffset.MinValue ? "-" : filteredLogs[0].CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm", TurkishCulture))}";

        Tile2LabelText.Text = "Bugun";
        Tile2ValueText.Text = todayCount.ToString(CultureInfo.InvariantCulture);
        Tile2DetailText.Text = $"{actionTypeCount} farkli islem turu";

        Tile3LabelText.Text = "Varliklar";
        Tile3ValueText.Text = filteredLogs.Select(x => x.TableName).Distinct(StringComparer.OrdinalIgnoreCase).Count().ToString(CultureInfo.InvariantCulture);
        Tile3DetailText.Text = entityNames.Count == 0 ? "-" : string.Join(", ", entityNames);

        var filterText = GetAuditLogFilterText();
        AuditLogHintText.Text = filterText;
        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = string.Empty;
        TypeYearlyHintText.Text = string.Empty;
    }

    private void ApplyAuditLogDetail(AuditLog? log)
    {
        if (log is null)
        {
            AuditLogDetailPanel.Visibility = Visibility.Collapsed;
            AuditLogDetailTitleText.Text = "Kayit detayi";
            AuditLogDetailMetaText.Text = string.Empty;
            AuditLogOldValueTextBox.Text = string.Empty;
            AuditLogNewValueTextBox.Text = string.Empty;
            AuditLogDiffGrid.ItemsSource = Array.Empty<AuditLogDiffRow>();
            return;
        }

        AuditLogDetailPanel.Visibility = Visibility.Visible;
        AuditLogDetailTitleText.Text = $"{FormatAuditActionLabel(log.ActionType)} / {FormatAuditTableLabel(log.TableName)}";
        AuditLogDetailMetaText.Text = $"Kayit Id: {log.RecordId} | Kullanici: {(string.IsNullOrWhiteSpace(log.UserName) ? "system" : log.UserName)} | Tarih: {(log.CreatedAt == DateTimeOffset.MinValue ? "-" : log.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm", TurkishCulture))}";
        AuditLogOldValueTextBox.Text = FormatAuditPayload(log.OldValue);
        AuditLogNewValueTextBox.Text = FormatAuditPayload(log.NewValue);
        var diffRows = BuildAuditLogDiffRows(log.OldValue, log.NewValue);
        if (AuditLogDiffChangedOnlyCheckBox.IsChecked == true)
        {
            diffRows = diffRows.Where(row => !string.Equals(row.ChangeType, "Ayni", StringComparison.Ordinal)).ToList();
        }

        AuditLogDiffGrid.ItemsSource = diffRows;
    }

    private void CopyAuditLogText(string? value, string successMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            AuditLogHintText.Text = "Kopyalanacak icerik yok.";
            return;
        }

        try
        {
            Clipboard.SetText(value);
            AuditLogHintText.Text = successMessage;
        }
        catch (Exception exception) when (exception is InvalidOperationException or ExternalException)
        {
            AuditLogHintText.Text = $"Kopyalama basarisiz: {exception.Message}";
        }
    }

    private void ExportAuditLogDetail(string format)
    {
        if (_selectedAuditLog is null)
        {
            AuditLogHintText.Text = "Disa aktarilacak kayit yok.";
            return;
        }

        var diffRows = (AuditLogDiffGrid.ItemsSource as IEnumerable<AuditLogDiffRow>)?.ToList() ?? new List<AuditLogDiffRow>();
        if (diffRows.Count == 0)
        {
            AuditLogHintText.Text = "Disa aktarilacak diff kaydi yok.";
            return;
        }

        try
        {
            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);

            var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture);
            var baseName = $"audit-log-{_selectedAuditLog.Id}-{timestamp}";
            var fileName = format == "json" ? $"{baseName}.json" : $"{baseName}.txt";
            var filePath = Path.Combine(exportsDir, fileName);

            if (format == "json")
            {
                var payload = new
                {
                    id = _selectedAuditLog.Id,
                    action = FormatAuditActionLabel(_selectedAuditLog.ActionType),
                    actionKey = _selectedAuditLog.ActionType,
                    entity = FormatAuditTableLabel(_selectedAuditLog.TableName),
                    entityKey = _selectedAuditLog.TableName,
                    recordId = _selectedAuditLog.RecordId,
                    user = string.IsNullOrWhiteSpace(_selectedAuditLog.UserName) ? "system" : _selectedAuditLog.UserName,
                    createdAt = _selectedAuditLog.CreatedAt == DateTimeOffset.MinValue
                        ? null
                        : _selectedAuditLog.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", TurkishCulture),
                    description = _selectedAuditLog.Description,
                    oldValue = AuditLogOldValueTextBox.Text,
                    newValue = AuditLogNewValueTextBox.Text,
                    diff = diffRows.Select(row => new
                    {
                        field = row.FieldName,
                        changeType = row.ChangeType,
                        oldValue = row.OldValue,
                        newValue = row.NewValue
                    }).ToList()
                };

                var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            else
            {
                var text = BuildAuditLogDiffClipboardText(_selectedAuditLog, diffRows);
                File.WriteAllText(filePath, text);
            }

            _lastAuditLogExportPath = filePath;
            RefreshRecentAuditLogExports(exportsDir);
            AuditLogHintText.Text = $"Disa aktarildi: exports/{fileName}";
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            AuditLogHintText.Text = $"Disa aktarma basarisiz: {exception.Message}";
        }
    }

    private static string BuildAuditLogDiffClipboardText(AuditLog log, IReadOnlyList<AuditLogDiffRow> diffRows)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Islem: {FormatAuditActionLabel(log.ActionType)}");
        builder.AppendLine($"Varlik: {FormatAuditTableLabel(log.TableName)}");
        builder.AppendLine($"Kayit Id: {log.RecordId}");
        builder.AppendLine($"Kullanici: {(string.IsNullOrWhiteSpace(log.UserName) ? "system" : log.UserName)}");
        builder.AppendLine($"Tarih: {(log.CreatedAt == DateTimeOffset.MinValue ? "-" : log.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm", TurkishCulture))}");
        if (!string.IsNullOrWhiteSpace(log.Description))
        {
            builder.AppendLine($"Aciklama: {log.Description}");
        }

        builder.AppendLine();
        builder.AppendLine("Alan\tDurum\tEski\tYeni");

        foreach (var row in diffRows)
        {
            builder.AppendLine($"{row.FieldName}\t{row.ChangeType}\t{NormalizeClipboardCell(row.OldValue)}\t{NormalizeClipboardCell(row.NewValue)}");
        }

        return builder.ToString().TrimEnd();
    }

    private static string NormalizeClipboardCell(string value)
    {
        return value
            .Replace("\r\n", " | ", StringComparison.Ordinal)
            .Replace("\n", " | ", StringComparison.Ordinal)
            .Replace("\t", " ", StringComparison.Ordinal);
    }

    private static string FormatAuditExportFileTypeLabel(string extension)
    {
        return extension.ToLowerInvariant() switch
        {
            ".json" => "JSON",
            ".txt" => "TXT",
            _ => extension.TrimStart('.').ToUpperInvariant()
        };
    }

    private string? ResolveLastAuditLogExportPath(string exportsDir)
    {
        if (!string.IsNullOrWhiteSpace(_lastAuditLogExportPath) && File.Exists(_lastAuditLogExportPath))
        {
            return _lastAuditLogExportPath;
        }

        RefreshRecentAuditLogExports(exportsDir);
        return _recentAuditLogExports.FirstOrDefault()?.FilePath;
    }

    private string GetSelectedAuditLogExportTypeFilter()
    {
        return (AuditLogExportTypeFilterInput.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Tum";
    }

    private void RefreshRecentAuditLogExports(string exportsDir)
    {
        var selectedType = GetSelectedAuditLogExportTypeFilter();
        var selectedPath = (AuditLogRecentExportsInput.SelectedItem as AuditLogExportItem)?.FilePath;
        _recentAuditLogExports = Directory.Exists(exportsDir)
            ? Directory.GetFiles(exportsDir, "audit-log-*.*", SearchOption.TopDirectoryOnly)
                .Where(path => selectedType == "Tum" || string.Equals(FormatAuditExportFileTypeLabel(Path.GetExtension(path)), selectedType, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(path => File.GetLastWriteTimeUtc(path))
                .Take(5)
                .Select(path => new AuditLogExportItem(
                    FilePath: path,
                    FileName: Path.GetFileName(path),
                    FileTypeLabel: FormatAuditExportFileTypeLabel(Path.GetExtension(path)),
                    TimestampLabel: File.GetLastWriteTime(path).ToString("dd.MM HH:mm", TurkishCulture),
                    IsLastUsed: !string.IsNullOrWhiteSpace(_lastAuditLogExportPath)
                        && string.Equals(_lastAuditLogExportPath, path, StringComparison.OrdinalIgnoreCase)))
                .ToList()
            : [];

        AuditLogRecentExportsInput.ItemsSource = _recentAuditLogExports;
        if (_recentAuditLogExports.Count == 0)
        {
            AuditLogRecentExportsInput.SelectedItem = null;
            return;
        }

        var preferredItem = _recentAuditLogExports.FirstOrDefault(item => item.IsLastUsed)
            ?? _recentAuditLogExports.FirstOrDefault(item => string.Equals(item.FilePath, selectedPath, StringComparison.OrdinalIgnoreCase))
            ?? _recentAuditLogExports.FirstOrDefault();

        if (preferredItem is not null)
        {
            AuditLogRecentExportsInput.SelectedItem = preferredItem;
        }
    }

    private void TryOpenAuditLogPath(string filePath, string successMessage)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });

            AuditLogHintText.Text = successMessage;
        }
        catch (Exception exception) when (exception is Win32Exception or InvalidOperationException or IOException or UnauthorizedAccessException)
        {
            AuditLogHintText.Text = $"Dosya acilamadi: {exception.Message}";
        }
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

    private static YearlyRow ToYearlyRow(SubscriptionYearlyMonthRow row)
    {
        return new YearlyRow(
            TurkishCulture.DateTimeFormat.GetMonthName(row.Month),
            row.InvoiceCount.ToString(CultureInfo.InvariantCulture),
            FormatMoney(row.TotalAmount),
            FormatMoney(row.PaidTotal),
            FormatMoney(row.RemainingTotal),
            row.UnpaidInvoiceCount.ToString(CultureInfo.InvariantCulture),
            row.OverdueInvoiceCount.ToString(CultureInfo.InvariantCulture),
            row.MissingPdfCount.ToString(CultureInfo.InvariantCulture));
    }

    private static DistributionRow ToDistributionRow(InvoiceTypeYearlyDistributionRow row)
    {
        return new DistributionRow(
            row.SubscriptionName,
            row.InstitutionName,
            row.InvoiceCount.ToString(CultureInfo.InvariantCulture),
            FormatMoney(row.TotalAmount),
            FormatMoney(row.PaidTotal),
            FormatMoney(row.RemainingTotal),
            row.MissingPdfCount.ToString(CultureInfo.InvariantCulture));
    }

    private static DocumentHealthRow ToDocumentHealthRow(DocumentHealthIssue issue)
    {
        return new DocumentHealthRow(
            issue.IssueType,
            issue.EntityType,
            issue.PeriodOrDate,
            issue.InvoiceTypeName,
            issue.SubscriptionName,
            issue.InstitutionName,
            FormatMoney(issue.Amount),
            issue.PdfState,
            issue.PdfFilePath,
            issue.PdfSha256Hash,
            issue.Note);
    }

    private static AuditLogRow ToAuditLogRow(AuditLog log)
    {
        return new AuditLogRow(
            log,
            log.CreatedAt == DateTimeOffset.MinValue
                ? string.Empty
                : log.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm", TurkishCulture),
            FormatAuditActionLabel(log.ActionType),
            FormatAuditTableLabel(log.TableName),
            log.RecordId.ToString(CultureInfo.InvariantCulture),
            log.Description,
            string.IsNullOrWhiteSpace(log.UserName) ? "system" : log.UserName);
    }

    private static string FormatAuditActionLabel(string value)
    {
        return value switch
        {
            "subscription_created" => "Abonelik Olusturuldu",
            "subscription_updated" => "Abonelik Guncellendi",
            "subscription_deactivated" => "Abonelik Pasif Yapildi",
            "invoice_created" => "Fatura Olusturuldu",
            "invoice_updated" => "Fatura Guncellendi",
            "invoice_pdf_attached" => "Fatura PDF Eklendi",
            "payment_created" => "Odeme Olusturuldu",
            "payment_updated" => "Odeme Guncellendi",
            "payment_pdf_attached" => "Odeme PDF Eklendi",
            _ => value.Replace('_', ' '),
        };
    }

    private static string FormatAuditTableLabel(string value)
    {
        return value switch
        {
            "subscriptions" => "Abonelik",
            "invoices" => "Fatura",
            "payments" => "Odeme",
            "audit_logs" => "Audit Log",
            _ => value,
        };
    }

    private static string FormatAuditPayload(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Kayit yok";
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            return JsonSerializer.Serialize(document.RootElement, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (JsonException)
        {
            return value.Trim();
        }
    }

    private static IReadOnlyList<AuditLogDiffRow> BuildAuditLogDiffRows(string oldValue, string newValue)
    {
        var oldMap = FlattenAuditPayload(oldValue);
        var newMap = FlattenAuditPayload(newValue);
        var keys = oldMap.Keys
            .Union(newMap.Keys, StringComparer.OrdinalIgnoreCase)
            .OrderBy(key => key, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (keys.Count == 0)
        {
            return new[]
            {
                new AuditLogDiffRow("-", "Kayit yok", "Kayit yok", "Degisiklik yok"),
            };
        }

        return keys.Select(key =>
        {
            oldMap.TryGetValue(key, out var oldText);
            newMap.TryGetValue(key, out var newText);

            var changeType = oldText switch
            {
                null when newText is not null => "Eklendi",
                not null when newText is null => "Silindi",
                _ when string.Equals(oldText, newText, StringComparison.Ordinal) => "Ayni",
                _ => "Degisti",
            };

            return new AuditLogDiffRow(
                key,
                oldText ?? "-",
                newText ?? "-",
                changeType);
        }).ToList();
    }

    private static Dictionary<string, string> FlattenAuditPayload(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        try
        {
            using var document = JsonDocument.Parse(value);
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            FlattenJsonElement(document.RootElement, prefix: string.Empty, map);
            return map;
        }
        catch (JsonException)
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["value"] = value.Trim(),
            };
        }
    }

    private static void FlattenJsonElement(JsonElement element, string prefix, IDictionary<string, string> map)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in element.EnumerateObject())
                {
                    var childPrefix = string.IsNullOrWhiteSpace(prefix) ? property.Name : $"{prefix}.{property.Name}";
                    FlattenJsonElement(property.Value, childPrefix, map);
                }
                break;
            case JsonValueKind.Array:
                var index = 0;
                foreach (var item in element.EnumerateArray())
                {
                    FlattenJsonElement(item, $"{prefix}[{index}]", map);
                    index++;
                }
                if (index == 0)
                {
                    map[prefix] = "[]";
                }
                break;
            case JsonValueKind.String:
                map[prefix] = element.GetString() ?? string.Empty;
                break;
            case JsonValueKind.Number:
            case JsonValueKind.True:
            case JsonValueKind.False:
                map[prefix] = element.ToString();
                break;
            case JsonValueKind.Null:
            case JsonValueKind.Undefined:
                map[prefix] = "null";
                break;
            default:
                map[prefix] = element.ToString();
                break;
        }
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
                var typeOptions = new List<InvoiceTypeOption> { new("TÃ¼m TÃ¼rler", null) };
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

    private void EnsureTypeYearlyFiltersInitialized(IReadOnlyList<Invoice> invoices)
    {
        _isRefreshingTypeYearlyFilters = true;
        try
        {
            if (TypeYearlyInvoiceTypeInput.Items.Count == 0)
            {
                var types = _invoiceTypeRepository?.GetAll() ?? Array.Empty<InvoiceType>();
                var options = types.Select(item => new InvoiceTypeOption(item.Name, item.Id)).ToList();
                TypeYearlyInvoiceTypeInput.ItemsSource = options;
                TypeYearlyInvoiceTypeInput.DisplayMemberPath = nameof(InvoiceTypeOption.Label);
                TypeYearlyInvoiceTypeInput.SelectedItem = options.FirstOrDefault();
            }

            if (TypeYearlyYearInput.Items.Count == 0)
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

                TypeYearlyYearInput.ItemsSource = years;
                TypeYearlyYearInput.SelectedItem = years.Contains(DateTime.Today.Year) ? DateTime.Today.Year : years[0];
            }
        }
        finally
        {
            _isRefreshingTypeYearlyFilters = false;
        }
    }

    private void EnsureAuditLogFiltersInitialized()
    {
        _isRefreshingAuditLogFilters = true;
        try
        {
            var preferredActionType = GetSelectedAuditLogActionType() ?? _auditLogFilterPreferences.ActionType;
            var preferredEntityName = GetSelectedAuditLogEntity() ?? _auditLogFilterPreferences.EntityName;
            var preferredUserName = GetSelectedAuditLogUser() ?? _auditLogFilterPreferences.UserName;

            var actionOptions = new List<AuditLogActionOption>
            {
                new("Tum Islemler", null),
            };

            actionOptions.AddRange(_auditLogs
                .Select(log => log.ActionType)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .Select(value => new AuditLogActionOption(FormatAuditActionLabel(value), value)));

            AuditLogActionInput.ItemsSource = actionOptions;
            AuditLogActionInput.DisplayMemberPath = nameof(AuditLogActionOption.Label);

            AuditLogActionInput.SelectedItem = actionOptions.FirstOrDefault(option =>
                string.Equals(option.ActionType, preferredActionType, StringComparison.OrdinalIgnoreCase))
                ?? actionOptions[0];

            var entityOptions = new List<AuditLogEntityOption>
            {
                new("Tum Varliklar", null),
            };

            entityOptions.AddRange(_auditLogs
                .Select(log => log.TableName)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .Select(value => new AuditLogEntityOption(FormatAuditTableLabel(value), value)));

            AuditLogEntityInput.ItemsSource = entityOptions;
            AuditLogEntityInput.DisplayMemberPath = nameof(AuditLogEntityOption.Label);

            AuditLogEntityInput.SelectedItem = entityOptions.FirstOrDefault(option =>
                string.Equals(option.TableName, preferredEntityName, StringComparison.OrdinalIgnoreCase))
                ?? entityOptions[0];

            var userOptions = new List<AuditLogUserOption>
            {
                new("Tum Kullanicilar", null),
            };

            userOptions.AddRange(_auditLogs
                .Select(log => string.IsNullOrWhiteSpace(log.UserName) ? "system" : log.UserName.Trim())
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .OrderBy(value => value, StringComparer.CurrentCultureIgnoreCase)
                .Select(value => new AuditLogUserOption(value, value)));

            AuditLogUserInput.ItemsSource = userOptions;
            AuditLogUserInput.DisplayMemberPath = nameof(AuditLogUserOption.Label);

            AuditLogUserInput.SelectedItem = userOptions.FirstOrDefault(option =>
                string.Equals(option.UserName, preferredUserName, StringComparison.CurrentCultureIgnoreCase))
                ?? userOptions[0];

            AuditLogSearchInput.Text = string.IsNullOrWhiteSpace(AuditLogSearchInput.Text)
                ? _auditLogFilterPreferences.SearchText ?? string.Empty
                : AuditLogSearchInput.Text;

            if (AuditLogStartDateInput.SelectedDate is null && _auditLogFilterPreferences.StartDate is not null)
            {
                AuditLogStartDateInput.SelectedDate = _auditLogFilterPreferences.StartDate.Value;
            }

            if (AuditLogEndDateInput.SelectedDate is null && _auditLogFilterPreferences.EndDate is not null)
            {
                AuditLogEndDateInput.SelectedDate = _auditLogFilterPreferences.EndDate.Value;
            }

            AuditLogDiffChangedOnlyCheckBox.IsChecked = _auditLogFilterPreferences.ChangedOnly;

            if (AuditLogStartDateInput.SelectedDate is DateTime start &&
                AuditLogEndDateInput.SelectedDate is DateTime end &&
                start.Date > end.Date)
            {
                AuditLogEndDateInput.SelectedDate = start.Date;
            }
        }
        finally
        {
            _isRefreshingAuditLogFilters = false;
        }
    }

    private void ResetAuditLogFilterInputs()
    {
        _isRefreshingAuditLogFilters = true;
        try
        {
            if (AuditLogActionInput.Items.Count > 0)
            {
                AuditLogActionInput.SelectedIndex = 0;
            }

            if (AuditLogEntityInput.Items.Count > 0)
            {
                AuditLogEntityInput.SelectedIndex = 0;
            }

            if (AuditLogUserInput.Items.Count > 0)
            {
                AuditLogUserInput.SelectedIndex = 0;
            }

            AuditLogStartDateInput.SelectedDate = null;
            AuditLogEndDateInput.SelectedDate = null;
            AuditLogSearchInput.Text = string.Empty;
            AuditLogDiffChangedOnlyCheckBox.IsChecked = false;
        }
        finally
        {
            _isRefreshingAuditLogFilters = false;
        }
    }

    private void SaveAuditLogFilterPreferences()
    {
        _auditLogFilterPreferences = new AuditLogFilterPreferences(
            ActionType: GetSelectedAuditLogActionType(),
            EntityName: GetSelectedAuditLogEntity(),
            UserName: GetSelectedAuditLogUser(),
            SearchText: string.IsNullOrWhiteSpace(AuditLogSearchInput.Text) ? null : AuditLogSearchInput.Text.Trim(),
            StartDate: AuditLogStartDateInput.SelectedDate?.Date,
            EndDate: AuditLogEndDateInput.SelectedDate?.Date,
            ChangedOnly: AuditLogDiffChangedOnlyCheckBox.IsChecked == true);

        if (string.IsNullOrWhiteSpace(_rootDirectory))
        {
            return;
        }

        try
        {
            _auditLogFilterPreferences.Save(_rootDirectory);
        }
        catch
        {
            // Filter preference save failures should never break the report screen.
        }
    }

    private bool TryFocusSelectedAuditLog()
    {
        var matchingRow = (AuditLogGrid.ItemsSource as IEnumerable<AuditLogRow>)?
            .FirstOrDefault(row => row.Source.Id == _selectedAuditLog?.Id);

        if (matchingRow is null)
        {
            return false;
        }

        AuditLogGrid.SelectedItem = matchingRow;
        FocusAuditLogRow(matchingRow);
        return true;
    }

    private void FocusAuditLogRow(AuditLogRow row)
    {
        AuditLogGrid.UpdateLayout();
        AuditLogGrid.ScrollIntoView(row);
        AuditLogGrid.Focus();
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

    private InvoiceTypeOption? GetSelectedTypeYearlyInvoiceType()
    {
        return TypeYearlyInvoiceTypeInput.SelectedItem as InvoiceTypeOption;
    }

    private int GetSelectedTypeYearlyYear()
    {
        if (TypeYearlyYearInput.SelectedItem is int year)
        {
            return year;
        }

        return DateTime.Today.Year;
    }

    private enum ReportTab
    {
        Unpaid,
        Overdue,
        Upcoming,
        Monthly,
        YearlyAll,
        Subscription,
        SubscriptionYearly,
        TypeYearly,
        DocumentHealth,
        Consistency,
        AuditLog,
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

    private sealed record AuditLogActionOption(string Label, string? ActionType);

    private sealed record AuditLogEntityOption(string Label, string? TableName);

    private sealed record AuditLogUserOption(string Label, string? UserName);

    private sealed record YearlyRow(
        string MonthName,
        string InvoiceCountText,
        string TotalAmountText,
        string PaidText,
        string RemainingText,
        string UnpaidCountText,
        string OverdueCountText,
        string MissingPdfCountText);

    private sealed record DistributionRow(
        string SubscriptionName,
        string InstitutionName,
        string InvoiceCountText,
        string TotalAmountText,
        string PaidText,
        string RemainingText,
        string MissingPdfCountText);

    private sealed record DocumentHealthRow(
        string IssueType,
        string EntityType,
        string PeriodOrDate,
        string InvoiceTypeName,
        string SubscriptionName,
        string InstitutionName,
        string AmountText,
        string PdfState,
        string PdfFilePath,
        string PdfSha256Hash,
        string Note);

    private sealed record AuditLogRow(
        AuditLog Source,
        string CreatedAtText,
        string ActionType,
        string TableName,
        string RecordIdText,
        string Description,
        string UserName);

    private sealed record AuditLogDiffRow(
        string FieldName,
        string OldValue,
        string NewValue,
        string ChangeType);

    private sealed record AuditLogExportItem(
        string FilePath,
        string FileName,
        string FileTypeLabel,
        string TimestampLabel,
        bool IsLastUsed)
    {
        public string DisplayName => $"{TimestampLabel} {FileName}";
    }
}

