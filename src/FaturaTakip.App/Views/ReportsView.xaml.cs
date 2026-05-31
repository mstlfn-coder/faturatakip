using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
    private ActionableInvoiceReport _report = ActionableInvoiceReport.Empty;
    private MonthlyInvoiceReport _monthlyReport = MonthlyInvoiceReport.Empty;
    private SubscriptionMonthlyComparison _subscriptionComparison = SubscriptionMonthlyComparison.Empty;
    private SubscriptionYearlyReport _subscriptionYearly = SubscriptionYearlyReport.Empty;
    private InvoiceTypeYearlyReport _typeYearly = InvoiceTypeYearlyReport.Empty;
    private DocumentHealthReport _documentHealth = DocumentHealthReport.Empty;
    private ReportTab _activeTab = ReportTab.Unpaid;
    private bool _isInitialized;
    private bool _isRefreshingMonthlyFilters;
    private bool _isRefreshingSubscriptionFilters;
    private bool _isRefreshingTypeYearlyFilters;

    public ReportsView()
    {
        InitializeComponent();
    }

    public void Initialize(string databasePath)
    {
        _invoiceRepository = new InvoiceRepository(databasePath);
        _paymentRepository = new PaymentRepository(databasePath);
        _invoiceTypeRepository = new InvoiceTypeRepository(databasePath);
        _subscriptionRepository = new SubscriptionRepository(databasePath);
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

            var meta = new ExcelExportWriter.ReportMeta(
                AppTitle: "KURUM FATURA TAKIP PROGRAMI",
                InstitutionName: string.Empty,
                ReportTitle: GetPdfReportTitle(),
                ReportPeriod: GetPdfReportPeriod(),
                ReportDate: DateTime.Today,
                CreatedBy: Environment.UserName,
                FilterText: GetPdfReportFilterText());

            var (pdfSummary, primary, _secondaryTitle, secondTable) = BuildPdfContent();
            var summary = pdfSummary.Select(s => new ExcelExportWriter.SummaryItem(s.Label, s.Value, s.Detail)).ToList();
            var primaryRows = primary.Rows.Select(r => (IReadOnlyList<object?>)r.Cast<object?>().ToArray()).ToList();

            if (secondTable is null)
            {
                ExcelExportWriter.WriteReportWithHeader(
                    filePath,
                    sheetName: "Rapor",
                    meta: meta,
                    summary: summary,
                    headers: primary.Headers,
                    rows: primaryRows);
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
                    secondRows: secondRows);
            }

            var hint = $"Excel yazıldı: exports/{fileName}";
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
            // Raporlar ekranında ortak bir status bandı yok; ipucu alanını hata mesajı için kullanıyoruz.
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
            var meta = new PdfReportWriter.ReportMeta(
                AppTitle: "KURUM FATURA TAKIP PROGRAMI",
                InstitutionName: string.Empty,
                ReportTitle: GetPdfReportTitle(),
                ReportPeriod: GetPdfReportPeriod(),
                ReportDate: DateTime.Today,
                CreatedBy: createdBy,
                FilterText: GetPdfReportFilterText());

            var (summary, primary, secondaryTitle, secondTable) = BuildPdfContent();

            PdfReportWriter.WriteSimpleTableReport(
                filePath,
                meta,
                summary,
                primary.Headers,
                primary.Rows,
                secondaryTitle: secondaryTitle,
                secondTable: secondTable);

            SetPdfHint($"PDF yazıldı: exports/{fileName}");
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            SetPdfHint(exception.Message);
        }
    }

    private void SetPdfHint(string message)
    {
        // Raporlar ekranında ortak bir status bandı yok; mevcut ipucu alanlarını kullanıyoruz.
        TypeYearlyHintText.Text = message;
        MonthlyFilterHintText.Text = message;
        SubscriptionHintText.Text = message;
    }

    private string GetPdfReportTitle()
    {
        return _activeTab switch
        {
            ReportTab.Unpaid => "ÖDENMEMİŞ FATURALAR RAPORU",
            ReportTab.Overdue => "GECİKMİŞ FATURALAR RAPORU",
            ReportTab.Upcoming => "YAKLAŞAN ÖDEMELER RAPORU",
            ReportTab.Monthly => "AYLIK FATURA RAPORU",
            ReportTab.Subscription => "ABONELİK AYLIK FATURA RAPORU",
            ReportTab.SubscriptionYearly => "ABONELİK YILLIK FATURA RAPORU",
            ReportTab.TypeYearly => "TÜR YILLIK FATURA RAPORU",
            ReportTab.DocumentHealth => "EVRAK KONTROL RAPORU",
            _ => "RAPOR",
        };
    }

    private string GetPdfReportPeriod()
    {
        return _activeTab switch
        {
            ReportTab.Monthly => $"{_monthlyReport.Year:D4}/{_monthlyReport.Month:D2}",
            ReportTab.Subscription => $"{_subscriptionComparison.Current.Year:D4}/{_subscriptionComparison.Current.Month:D2}",
            ReportTab.SubscriptionYearly => _subscriptionYearly.Year == 0 ? string.Empty : $"{_subscriptionYearly.Year}",
            ReportTab.TypeYearly => _typeYearly.Year == 0 ? string.Empty : $"{_typeYearly.Year}",
            _ => string.Empty,
        };
    }

    private string GetPdfReportFilterText()
    {
        return _activeTab switch
        {
            ReportTab.Monthly => $"Tür: {(GetSelectedInvoiceTypeLabel() ?? "Tüm Türler")}",
            ReportTab.Subscription => $"Abonelik: {(GetSelectedSubscriptionLabel() ?? string.Empty)}",
            ReportTab.SubscriptionYearly => $"Abonelik: {(GetSelectedSubscriptionLabel() ?? string.Empty)}",
            ReportTab.TypeYearly => $"Tür: {(GetSelectedTypeYearlyInvoiceType()?.Label ?? string.Empty)}",
            ReportTab.DocumentHealth => "Fatura + Ödeme evrak kontrolü",
            _ => string.Empty,
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
                new("Ödenen", FormatMoney(_monthlyReport.PaidTotal), $"PDF eksik {_monthlyReport.MissingPdfCount}"),
                new("Kalan", FormatMoney(_monthlyReport.RemainingTotal), $"Ödenmemiş {_monthlyReport.UnpaidInvoiceCount}, Gecikmiş {_monthlyReport.OverdueInvoiceCount}"),
            };

            var headers = new[] { "Dönem", "Tür", "Abonelik", "Kurum", "Durum", "Fatura No", "Son Ödeme", "Tutar", "Ödenen", "Kalan", "PDF" };
            var rows = _monthlyReport.Rows.Select(r => (IReadOnlyList<string>)new[]
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

            return (summary, (headers, rows), secondaryTitle: null, secondTable: null);
        }

        if (_activeTab == ReportTab.TypeYearly)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam (Yıl)", FormatMoney(_typeYearly.TotalAmount), $"{_typeYearly.TotalInvoiceCount} fatura, Ödenen {FormatMoney(_typeYearly.PaidTotal)}, Kalan {FormatMoney(_typeYearly.RemainingTotal)}"),
                new("En Yüksek Ay", _typeYearly.HighestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_typeYearly.HighestMonth), _typeYearly.HighestMonth == 0 ? "-" : $"Toplam {FormatMoney(_typeYearly.HighestMonthTotal)}"),
                new("En Düşük Ay", _typeYearly.LowestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_typeYearly.LowestMonth), _typeYearly.LowestMonth == 0 ? "-" : $"Toplam {FormatMoney(_typeYearly.LowestMonthTotal)}"),
            };

            var headers = new[] { "Ay", "Fatura", "Toplam", "Ödenen", "Kalan", "Ödenmemiş", "Gecikmiş", "PDF Eksik" };
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

            var distHeaders = new[] { "Abonelik", "Kurum", "Fatura", "Toplam", "Ödenen", "Kalan", "PDF Eksik" };
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

            var second = (Headers: (IReadOnlyList<string>)distHeaders, Rows: (IReadOnlyList<IReadOnlyList<string>>)distRows, Title: "Abonelik Dağılımı");
            var secondaryTitle = _typeYearly.Year == 0 ? null : $"{_typeYearly.InvoiceTypeName} / {_typeYearly.Year}";
            return (summary, (headers, rows), secondaryTitle, second);
        }

        if (_activeTab == ReportTab.SubscriptionYearly)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam (Yıl)", FormatMoney(_subscriptionYearly.TotalAmount), $"{_subscriptionYearly.TotalInvoiceCount} fatura, Ödenen {FormatMoney(_subscriptionYearly.PaidTotal)}, Kalan {FormatMoney(_subscriptionYearly.RemainingTotal)}"),
                new("En Yüksek Ay", _subscriptionYearly.HighestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.HighestMonth), _subscriptionYearly.HighestMonth == 0 ? "-" : $"Toplam {FormatMoney(_subscriptionYearly.HighestMonthTotal)}"),
                new("En Düşük Ay", _subscriptionYearly.LowestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.LowestMonth), _subscriptionYearly.LowestMonth == 0 ? "-" : $"Toplam {FormatMoney(_subscriptionYearly.LowestMonthTotal)}"),
            };

            var headers = new[] { "Ay", "Fatura", "Toplam", "Ödenen", "Kalan", "Ödenmemiş", "Gecikmiş", "PDF Eksik" };
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

            return (summary, (headers, rows), secondaryTitle: GetSelectedSubscriptionLabel(), secondTable: null);
        }

        if (_activeTab == ReportTab.Subscription)
        {
            var current = _subscriptionComparison.Current;
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Toplam Fatura", current.TotalInvoiceCount.ToString(CultureInfo.InvariantCulture), $"Toplam {FormatMoney(current.TotalAmount)}"),
                new("Ödenen", FormatMoney(current.PaidTotal), $"Önceki ay {FormatMoney(_subscriptionComparison.Previous.PaidTotal)}"),
                new("Kalan", FormatMoney(current.RemainingTotal), $"Önceki ay {FormatMoney(_subscriptionComparison.Previous.RemainingTotal)}"),
            };

            var headers = new[] { "Dönem", "Tür", "Abonelik", "Kurum", "Durum", "Fatura No", "Son Ödeme", "Tutar", "Ödenen", "Kalan", "PDF" };
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

            return (summary, (headers, rows), secondaryTitle: GetSelectedSubscriptionLabel(), secondTable: null);
        }

        if (_activeTab == ReportTab.DocumentHealth)
        {
            var summary = new List<PdfReportWriter.SummaryItem>
            {
                new("Fatura PDF", (_documentHealth.InvoiceNoPdfCount + _documentHealth.InvoiceMissingFileCount).ToString(CultureInfo.InvariantCulture), $"Yok {_documentHealth.InvoiceNoPdfCount}, Kayıp {_documentHealth.InvoiceMissingFileCount}"),
                new("Ödeme PDF", (_documentHealth.PaymentNoPdfCount + _documentHealth.PaymentMissingFileCount).ToString(CultureInfo.InvariantCulture), $"Yok {_documentHealth.PaymentNoPdfCount}, Kayıp {_documentHealth.PaymentMissingFileCount}"),
                new("Aynı Hash", (_documentHealth.DuplicateInvoiceHashItemCount + _documentHealth.DuplicatePaymentHashItemCount).ToString(CultureInfo.InvariantCulture), $"Fatura {_documentHealth.DuplicateInvoiceHashItemCount}, Ödeme {_documentHealth.DuplicatePaymentHashItemCount}"),
            };

            var headers = new[] { "Uyarı", "Tip", "Dönem/Tarih", "Tür", "Abonelik", "Kurum", "Tutar", "PDF", "Yol", "Hash", "Not" };
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

            return (summary, (headers, rows), secondaryTitle: null, secondTable: null);
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
        var defaultHeaders = new[] { "Dönem", "Tür", "Abonelik", "Kurum", "Durum", "Fatura No", "Son Ödeme", "Tutar", "Ödenen", "Kalan", "PDF" };
        var defaultRows = actionableItems.Select(i => (IReadOnlyList<string>)new[]
        {
            i.Invoice.Period,
            i.Invoice.InvoiceTypeName,
            i.Invoice.SubscriptionName,
            i.Invoice.InstitutionName,
            i.Invoice.State,
            i.Invoice.InvoiceNo,
            i.Invoice.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
            FormatMoney(i.Invoice.Amount),
            FormatMoney(i.Invoice.PaidAmount),
            FormatMoney(i.Invoice.RemainingAmount),
            i.PdfState,
        }).ToList();

        return (defaultSummary, (defaultHeaders, defaultRows), secondaryTitle: null, secondTable: null);
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
        SetTabActive(SubscriptionTabButton, tab == ReportTab.Subscription);
        SetTabActive(SubscriptionYearlyTabButton, tab == ReportTab.SubscriptionYearly);
        SetTabActive(TypeYearlyTabButton, tab == ReportTab.TypeYearly);
        SetTabActive(DocumentHealthTabButton, tab == ReportTab.DocumentHealth);

        MonthlyFilterPanel.Visibility = tab == ReportTab.Monthly ? Visibility.Visible : Visibility.Collapsed;
        SubscriptionFilterPanel.Visibility = tab is ReportTab.Subscription or ReportTab.SubscriptionYearly
            ? Visibility.Visible
            : Visibility.Collapsed;
        SubscriptionMonthInput.Visibility = tab == ReportTab.Subscription ? Visibility.Visible : Visibility.Collapsed;
        TypeYearlyFilterPanel.Visibility = tab == ReportTab.TypeYearly ? Visibility.Visible : Visibility.Collapsed;

        var items = tab switch
        {
            ReportTab.Overdue => _report.Overdue,
            ReportTab.Upcoming => _report.Upcoming,
            ReportTab.Monthly => Array.Empty<ActionableInvoice>(),
            ReportTab.Subscription => Array.Empty<ActionableInvoice>(),
            ReportTab.SubscriptionYearly => Array.Empty<ActionableInvoice>(),
            ReportTab.TypeYearly => Array.Empty<ActionableInvoice>(),
            ReportTab.DocumentHealth => Array.Empty<ActionableInvoice>(),
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
        }
        else if (tab == ReportTab.Subscription)
        {
            ApplySubscriptionTiles();
            ReportGrid.ItemsSource = _subscriptionComparison.Current.Rows.Select(ToMonthlyRow).ToList();
            ReportGrid.Visibility = Visibility.Visible;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
        }
        else if (tab == ReportTab.SubscriptionYearly)
        {
            ApplySubscriptionYearlyTiles();
            YearlyGrid.ItemsSource = _subscriptionYearly.Months.Select(ToYearlyRow).ToList();
            ReportGrid.Visibility = Visibility.Collapsed;
            YearlyGrid.Visibility = Visibility.Visible;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
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
        }
        else if (tab == ReportTab.DocumentHealth)
        {
            ApplyDocumentHealthTiles();
            DocumentHealthGrid.ItemsSource = _documentHealth.Issues.Select(ToDocumentHealthRow).ToList();
            ReportGrid.Visibility = Visibility.Collapsed;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Visible;
        }
        else
        {
            ApplyActionableTiles();
            ReportGrid.ItemsSource = items.Select(ToActionableRow).ToList();
            ReportGrid.Visibility = Visibility.Visible;
            YearlyGrid.Visibility = Visibility.Collapsed;
            DistributionGrid.Visibility = Visibility.Collapsed;
            DocumentHealthGrid.Visibility = Visibility.Collapsed;
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

    private void ApplySubscriptionYearlyTiles()
    {
        var year = _subscriptionYearly.Year;
        Tile1LabelText.Text = "Toplam (Yıl)";
        Tile1ValueText.Text = FormatMoney(_subscriptionYearly.TotalAmount);
        Tile1DetailText.Text = $"{_subscriptionYearly.TotalInvoiceCount} fatura, Ödenen {FormatMoney(_subscriptionYearly.PaidTotal)}, Kalan {FormatMoney(_subscriptionYearly.RemainingTotal)}";

        Tile2LabelText.Text = "En Yüksek Ay";
        Tile2ValueText.Text = _subscriptionYearly.HighestMonth == 0
            ? "-"
            : $"{TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.HighestMonth)}";
        Tile2DetailText.Text = _subscriptionYearly.HighestMonth == 0
            ? "-"
            : $"Toplam {FormatMoney(_subscriptionYearly.HighestMonthTotal)}";

        Tile3LabelText.Text = "En Düşük Ay";
        Tile3ValueText.Text = _subscriptionYearly.LowestMonth == 0
            ? "-"
            : $"{TurkishCulture.DateTimeFormat.GetMonthName(_subscriptionYearly.LowestMonth)}";
        Tile3DetailText.Text = _subscriptionYearly.LowestMonth == 0
            ? "-"
            : $"Toplam {FormatMoney(_subscriptionYearly.LowestMonthTotal)}";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = year == 0 ? string.Empty : $"{year} yılı (12 ay)";
    }

    private void ApplyTypeYearlyTiles()
    {
        Tile1LabelText.Text = "Toplam (Yıl)";
        Tile1ValueText.Text = FormatMoney(_typeYearly.TotalAmount);
        Tile1DetailText.Text = $"{_typeYearly.TotalInvoiceCount} fatura, Ödenen {FormatMoney(_typeYearly.PaidTotal)}, Kalan {FormatMoney(_typeYearly.RemainingTotal)}";

        Tile2LabelText.Text = "En Yüksek Ay";
        Tile2ValueText.Text = _typeYearly.HighestMonth == 0 ? "-" : TurkishCulture.DateTimeFormat.GetMonthName(_typeYearly.HighestMonth);
        Tile2DetailText.Text = _typeYearly.HighestMonth == 0 ? "-" : $"Toplam {FormatMoney(_typeYearly.HighestMonthTotal)}";

        Tile3LabelText.Text = "En Düşük Ay";
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
        Tile1DetailText.Text = $"Yok {_documentHealth.InvoiceNoPdfCount}, Kayıp {_documentHealth.InvoiceMissingFileCount}";

        Tile2LabelText.Text = "Ödeme PDF";
        Tile2ValueText.Text = (_documentHealth.PaymentNoPdfCount + _documentHealth.PaymentMissingFileCount).ToString(CultureInfo.InvariantCulture);
        Tile2DetailText.Text = $"Yok {_documentHealth.PaymentNoPdfCount}, Kayıp {_documentHealth.PaymentMissingFileCount}";

        Tile3LabelText.Text = "Aynı Hash";
        Tile3ValueText.Text = (_documentHealth.DuplicateInvoiceHashItemCount + _documentHealth.DuplicatePaymentHashItemCount).ToString(CultureInfo.InvariantCulture);
        Tile3DetailText.Text = $"Fatura {_documentHealth.DuplicateInvoiceHashItemCount}, Ödeme {_documentHealth.DuplicatePaymentHashItemCount}";

        MonthlyFilterHintText.Text = string.Empty;
        SubscriptionHintText.Text = string.Empty;
        TypeYearlyHintText.Text = string.Empty;
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
        Subscription,
        SubscriptionYearly,
        TypeYearly,
        DocumentHealth,
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
}

