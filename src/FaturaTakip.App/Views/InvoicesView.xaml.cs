using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Payments;
using FaturaTakip.App.Data.Subscriptions;
using FaturaTakip.App.Infrastructure;
using Microsoft.Win32;

namespace FaturaTakip.App.Views;

public partial class InvoicesView : UserControl
{
    private static readonly CultureInfo TurkishCulture = CultureInfo.GetCultureInfo("tr-TR");
    private InvoiceRepository? _invoiceRepository;
    private PaymentRepository? _paymentRepository;
    private SubscriptionRepository? _subscriptionRepository;
    private IReadOnlyList<Invoice> _invoices = Array.Empty<Invoice>();
    private IReadOnlyList<Payment> _payments = Array.Empty<Payment>();
    private IReadOnlyList<Subscription> _subscriptions = Array.Empty<Subscription>();
    private Invoice? _selectedInvoice;
    private Payment? _selectedPayment;
    private string? _pendingPdfSourcePath;
    private bool _isInitialized;
    private bool _isEditingExisting;
    private bool _isRefreshingFilterOptions;

    public event EventHandler? InvoicesChanged;

    public InvoicesView()
    {
        InitializeComponent();
    }

    public void Initialize(string databasePath)
    {
        _invoiceRepository = new InvoiceRepository(databasePath);
        _paymentRepository = new PaymentRepository(databasePath);
        _subscriptionRepository = new SubscriptionRepository(databasePath);
        _isInitialized = true;

        InvoiceMonthInput.ItemsSource = Enumerable.Range(1, 12)
            .Select(month => new MonthOption(month, TurkishCulture.DateTimeFormat.GetMonthName(month)))
            .ToList();
        InvoiceMonthFilterInput.ItemsSource = new[] { new MonthFilterOption("Tüm Aylar", null) }
            .Concat(Enumerable.Range(1, 12)
                .Select(month => new MonthFilterOption(TurkishCulture.DateTimeFormat.GetMonthName(month), month)))
            .ToList();
        InvoiceMonthFilterInput.SelectedIndex = 0;
        InvoicePaymentStatusFilterInput.ItemsSource = new List<PaymentStatusFilterOption>
        {
            new("Tüm Durumlar", InvoicePaymentStatusFilter.All),
            new("Ödenmedi", InvoicePaymentStatusFilter.Unpaid),
            new("Ödendi", InvoicePaymentStatusFilter.Paid),
            new("İptal", InvoicePaymentStatusFilter.Canceled),
            new("Gecikmiş", InvoicePaymentStatusFilter.Overdue),
        };
        InvoicePaymentStatusFilterInput.SelectedIndex = 0;
        InvoicePdfStatusFilterInput.ItemsSource = new List<PdfStatusFilterOption>
        {
            new("Tüm PDF", InvoicePdfStatusFilter.All),
            new("PDF Var", InvoicePdfStatusFilter.HasPdf),
            new("PDF Eksik", InvoicePdfStatusFilter.MissingPdf),
        };
        InvoicePdfStatusFilterInput.SelectedIndex = 0;

        RefreshSubscriptionLists();
        RefreshInvoices();
    }

    public void Refresh()
    {
        if (!_isInitialized)
        {
            return;
        }

        RefreshSubscriptionLists();
        RefreshInvoices(_selectedInvoice?.Id);
    }

    private void RefreshSubscriptionLists()
    {
        if (_subscriptionRepository is null)
        {
            return;
        }

        var selectedSubscriptionId = (InvoiceSubscriptionFilterInput.SelectedItem as SubscriptionFilterOption)?.SubscriptionId;
        _subscriptions = _subscriptionRepository.GetAll();
        InvoiceSubscriptionInput.ItemsSource = _subscriptions;

        var filters = new List<SubscriptionFilterOption>
        {
            new("Tüm Abonelikler", null),
        };
        filters.AddRange(_subscriptions.Select(item => new SubscriptionFilterOption(
            $"{item.InvoiceTypeName} - {item.SubscriptionName}",
            item.Id)));

        InvoiceSubscriptionFilterInput.ItemsSource = filters;
        InvoiceSubscriptionFilterInput.SelectedItem = filters.FirstOrDefault(item => item.SubscriptionId == selectedSubscriptionId) ?? filters[0];
    }

    private void RefreshInvoices(long? selectedId = null)
    {
        if (_invoiceRepository is null)
        {
            return;
        }

        _invoices = _invoiceRepository.GetAll();
        var unpaidCount = _invoices.Count(item => item.Status == "unpaid");
        var overdueCount = _invoices.Count(item => item.Status == "unpaid" && item.DueDate.Date < DateTime.Today);
        var missingPdfCount = _invoices.Count(item => _invoiceRepository.IsPdfMissing(item));

        InvoiceCountText.Text = _invoices.Count.ToString(CultureInfo.InvariantCulture);
        UnpaidInvoiceCountText.Text = unpaidCount.ToString(CultureInfo.InvariantCulture);
        OverdueInvoiceCountText.Text = overdueCount.ToString(CultureInfo.InvariantCulture);
        MissingPdfCountText.Text = missingPdfCount.ToString(CultureInfo.InvariantCulture);

        RefreshDynamicFilterOptions();
        ApplyFiltersToGrid(selectedId);
    }

    private void RefreshDynamicFilterOptions()
    {
        var selectedYear = (InvoiceYearFilterInput.SelectedItem as YearFilterOption)?.Year;
        var selectedInvoiceTypeId = (InvoiceTypeFilterInput.SelectedItem as InvoiceTypeFilterOption)?.InvoiceTypeId;

        _isRefreshingFilterOptions = true;
        try
        {
            var years = new List<YearFilterOption>
            {
                new("Tüm Yıllar", null),
            };
            years.AddRange(_invoices
                .Select(item => item.InvoiceYear)
                .Distinct()
                .OrderByDescending(item => item)
                .Select(item => new YearFilterOption(item.ToString(CultureInfo.InvariantCulture), item)));
            InvoiceYearFilterInput.ItemsSource = years;
            InvoiceYearFilterInput.SelectedItem = years.FirstOrDefault(item => item.Year == selectedYear) ?? years[0];

            var invoiceTypes = new List<InvoiceTypeFilterOption>
            {
                new("Tüm Türler", null),
            };
            invoiceTypes.AddRange(_invoices
                .GroupBy(item => new { item.InvoiceTypeId, item.InvoiceTypeName })
                .OrderBy(group => group.Key.InvoiceTypeName, StringComparer.CurrentCultureIgnoreCase)
                .Select(group => new InvoiceTypeFilterOption(group.Key.InvoiceTypeName, group.Key.InvoiceTypeId)));
            InvoiceTypeFilterInput.ItemsSource = invoiceTypes;
            InvoiceTypeFilterInput.SelectedItem = invoiceTypes.FirstOrDefault(item => item.InvoiceTypeId == selectedInvoiceTypeId) ?? invoiceTypes[0];
        }
        finally
        {
            _isRefreshingFilterOptions = false;
        }
    }

    private IEnumerable<Invoice> ApplyFilters(IEnumerable<Invoice> source)
    {
        var criteria = ReadFilterCriteria();
        return InvoiceFilter.Apply(
            source,
            criteria,
            DateTime.Today,
            invoice => _invoiceRepository?.IsPdfMissing(invoice) ?? !invoice.HasPdf);
    }

    private void InvoiceFilter_Changed(object sender, RoutedEventArgs e)
    {
        if (!_isInitialized || _isRefreshingFilterOptions)
        {
            return;
        }

        ApplyFiltersToGrid(_selectedInvoice?.Id);
    }

    private void ClearInvoiceFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        InvoiceSearchInput.Text = string.Empty;
        InvoiceTypeFilterInput.SelectedIndex = 0;
        InvoiceSubscriptionFilterInput.SelectedIndex = 0;
        InvoiceYearFilterInput.SelectedIndex = 0;
        InvoiceMonthFilterInput.SelectedIndex = 0;
        InvoicePaymentStatusFilterInput.SelectedIndex = 0;
        InvoicePdfStatusFilterInput.SelectedIndex = 0;
        ApplyFiltersToGrid(_selectedInvoice?.Id);
    }

    private void QuickFilterCurrentMonthButton_Click(object sender, RoutedEventArgs e)
    {
        ResetQuickFilters();
        SelectYearFilter(DateTime.Today.Year);
        SelectMonthFilter(DateTime.Today.Month);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("Bu ay filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private void QuickFilterUnpaidButton_Click(object sender, RoutedEventArgs e)
    {
        ResetQuickFilters();
        SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Unpaid);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("Odenmemis filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private void QuickFilterOverdueButton_Click(object sender, RoutedEventArgs e)
    {
        ResetQuickFilters();
        SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Overdue);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("Gecikmis filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private void QuickFilterMissingPdfButton_Click(object sender, RoutedEventArgs e)
    {
        ResetQuickFilters();
        SelectPdfStatusFilter(InvoicePdfStatusFilter.MissingPdf);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("PDF eksik filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private InvoiceFilterCriteria ReadFilterCriteria()
    {
        return new InvoiceFilterCriteria(
            Year: (InvoiceYearFilterInput.SelectedItem as YearFilterOption)?.Year,
            Month: (InvoiceMonthFilterInput.SelectedItem as MonthFilterOption)?.Month,
            InvoiceTypeId: (InvoiceTypeFilterInput.SelectedItem as InvoiceTypeFilterOption)?.InvoiceTypeId,
            SubscriptionId: (InvoiceSubscriptionFilterInput.SelectedItem as SubscriptionFilterOption)?.SubscriptionId,
            PaymentStatus: (InvoicePaymentStatusFilterInput.SelectedItem as PaymentStatusFilterOption)?.Value ?? InvoicePaymentStatusFilter.All,
            PdfStatus: (InvoicePdfStatusFilterInput.SelectedItem as PdfStatusFilterOption)?.Value ?? InvoicePdfStatusFilter.All,
            SearchText: InvoiceSearchInput.Text);
    }

    private void ApplyFiltersToGrid(long? selectedId = null, bool selectFirstIfAvailable = false)
    {
        var filtered = ApplyFilters(_invoices).ToList();
        InvoiceGrid.ItemsSource = filtered;
        InvoiceFilterHintText.Text = filtered.Count == 0
            ? "Filtre sonucunda kayit bulunamadi."
            : $"{filtered.Count} kayit listeleniyor.";

        var selected = selectedId is null
            ? null
            : filtered.FirstOrDefault(item => item.Id == selectedId.Value);

        if (selected is null && selectFirstIfAvailable)
        {
            selected = filtered.FirstOrDefault();
        }

        if (selected is null)
        {
            InvoiceGrid.SelectedItem = null;
            ClearInvoiceForm();
            return;
        }

        InvoiceGrid.SelectedItem = selected;
        InvoiceGrid.ScrollIntoView(selected);
        ApplySelectedInvoice(selected);
    }

    private void ResetQuickFilters()
    {
        InvoiceSearchInput.Text = string.Empty;
        InvoiceTypeFilterInput.SelectedIndex = 0;
        InvoiceSubscriptionFilterInput.SelectedIndex = 0;
        InvoiceYearFilterInput.SelectedIndex = 0;
        InvoiceMonthFilterInput.SelectedIndex = 0;
        InvoicePaymentStatusFilterInput.SelectedIndex = 0;
        InvoicePdfStatusFilterInput.SelectedIndex = 0;
    }

    private void SelectYearFilter(int year)
    {
        if (InvoiceYearFilterInput.ItemsSource is not IEnumerable<YearFilterOption> options)
        {
            return;
        }

        InvoiceYearFilterInput.SelectedItem = options.FirstOrDefault(item => item.Year == year)
            ?? options.FirstOrDefault();
    }

    private void SelectMonthFilter(int month)
    {
        if (InvoiceMonthFilterInput.ItemsSource is not IEnumerable<MonthFilterOption> options)
        {
            return;
        }

        InvoiceMonthFilterInput.SelectedItem = options.FirstOrDefault(item => item.Month == month)
            ?? options.FirstOrDefault();
    }

    private void SelectPaymentStatusFilter(InvoicePaymentStatusFilter status)
    {
        if (InvoicePaymentStatusFilterInput.ItemsSource is not IEnumerable<PaymentStatusFilterOption> options)
        {
            return;
        }

        InvoicePaymentStatusFilterInput.SelectedItem = options.FirstOrDefault(item => item.Value == status)
            ?? options.FirstOrDefault();
    }

    private void SelectPdfStatusFilter(InvoicePdfStatusFilter status)
    {
        if (InvoicePdfStatusFilterInput.ItemsSource is not IEnumerable<PdfStatusFilterOption> options)
        {
            return;
        }

        InvoicePdfStatusFilterInput.SelectedItem = options.FirstOrDefault(item => item.Value == status)
            ?? options.FirstOrDefault();
    }

    private void InvoiceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (InvoiceGrid.SelectedItem is Invoice invoice)
        {
            ApplySelectedInvoice(invoice);
        }
    }

    private void ApplySelectedInvoice(Invoice invoice)
    {
        _selectedInvoice = invoice;
        _pendingPdfSourcePath = null;
        _isEditingExisting = true;

        InvoiceFormTitleText.Text = "Faturayı Düzenle";
        InvoiceSubscriptionInput.SelectedValue = invoice.SubscriptionId;
        InvoiceYearInput.Text = invoice.InvoiceYear.ToString(CultureInfo.InvariantCulture);
        InvoiceMonthInput.SelectedValue = invoice.InvoiceMonth;
        InvoiceDateInput.SelectedDate = invoice.InvoiceDate;
        InvoiceDueDateInput.SelectedDate = invoice.DueDate;
        InvoiceNoInput.Text = invoice.InvoiceNo;
        InvoiceAmountInput.Text = invoice.Amount.ToString("N2", TurkishCulture);
        UsageAmountInput.Text = invoice.UsageAmount.ToString("N2", TurkishCulture);
        UsageUnitInput.Text = invoice.UsageUnit;
        InvoiceDescriptionInput.Text = invoice.Description;
        UpdatePdfControls(invoice);
        RefreshPaymentControls(invoice);
        SetInvoiceStatus($"Seçili kayıt: {invoice.InvoiceNo}", isError: false);
    }

    private void NewInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        ClearInvoiceForm();
        InvoiceNoInput.Focus();
    }

    private void PrepareNextInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedInvoice is null)
        {
            SetInvoiceStatus("Sonraki ay taslagi icin once listeden bir fatura secin.", isError: true);
            return;
        }

        var draft = InvoiceDraftTemplateBuilder.FromInvoice(_selectedInvoice);
        ApplyInvoiceDraft(draft);
        InvoiceNoInput.Focus();
        SetInvoiceStatus("Secili faturadan sonraki ay icin taslak hazirlandi. Fatura no ve PDF alanlari bilincli olarak bos birakildi.", isError: false);
    }

    private void SaveInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (_invoiceRepository is null)
        {
            return;
        }

        try
        {
            var input = ReadInvoiceInput();
            var warning = InvoiceRepository.GetDueDateWarning(input);
            var saved = _selectedInvoice is null
                ? _invoiceRepository.Add(input)
                : _invoiceRepository.Update(_selectedInvoice.Id, input);
            var pdfAttached = false;

            if (!string.IsNullOrWhiteSpace(_pendingPdfSourcePath))
            {
                saved = _invoiceRepository.AttachPdf(saved.Id, _pendingPdfSourcePath);
                _pendingPdfSourcePath = null;
                pdfAttached = true;
            }

            RefreshInvoices(saved.Id);
            InvoicesChanged?.Invoke(this, EventArgs.Empty);
            var successMessage = pdfAttached ? "Fatura ve PDF kaydedildi." : "Fatura kaydedildi.";
            SetInvoiceStatus(warning is null ? successMessage : $"{successMessage} Uyarı: {warning}", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException or IOException or UnauthorizedAccessException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void ExportInvoicesButton_Click(object sender, RoutedEventArgs e)
    {
        if (_invoiceRepository is null)
        {
            return;
        }

        try
        {
            var items = InvoiceGrid.ItemsSource as IEnumerable<Invoice> ?? Array.Empty<Invoice>();
            var list = items.ToList();
            if (list.Count == 0)
            {
                SetInvoiceStatus("Excel aktarımı için listede kayıt yok.", isError: true);
                return;
            }

            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);
            var fileName = $"faturalar-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx";
            var filePath = Path.Combine(exportsDir, fileName);

            ExcelExportWriter.WriteInvoices(filePath, list, invoice => _invoiceRepository.IsPdfMissing(invoice));

            SetInvoiceStatus($"Excel dosyası oluşturuldu: exports/{fileName}", isError: false);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void ExportInvoicesPdfButton_Click(object sender, RoutedEventArgs e)
    {
        if (_invoiceRepository is null)
        {
            return;
        }

        try
        {
            var items = InvoiceGrid.ItemsSource as IEnumerable<Invoice> ?? Array.Empty<Invoice>();
            var list = items.ToList();
            if (list.Count == 0)
            {
                SetInvoiceStatus("PDF aktarımı için listede kayıt yok.", isError: true);
                return;
            }

            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);
            var fileName = $"faturalar-{DateTime.Now:yyyyMMdd-HHmmss}.pdf";
            var filePath = Path.Combine(exportsDir, fileName);

            var criteria = ReadFilterCriteria();
            var period = criteria.Year is null
                ? string.Empty
                : criteria.Month is null ? $"{criteria.Year}" : $"{criteria.Year:D4}/{criteria.Month.Value:D2}";

            var filterParts = new List<string>();
            if (InvoiceTypeFilterInput.SelectedItem is InvoiceTypeFilterOption typeOpt && typeOpt.InvoiceTypeId is not null)
                filterParts.Add($"Tür: {typeOpt.Label}");
            if (InvoiceSubscriptionFilterInput.SelectedItem is SubscriptionFilterOption subOpt && subOpt.SubscriptionId is not null)
                filterParts.Add($"Abonelik: {subOpt.Label}");
            if (InvoicePaymentStatusFilterInput.SelectedItem is PaymentStatusFilterOption payOpt && payOpt.Value != InvoicePaymentStatusFilter.All)
                filterParts.Add($"Durum: {payOpt.Label}");
            if (InvoicePdfStatusFilterInput.SelectedItem is PdfStatusFilterOption pdfOpt && pdfOpt.Value != InvoicePdfStatusFilter.All)
                filterParts.Add($"PDF: {pdfOpt.Label}");
            if (!string.IsNullOrWhiteSpace(criteria.SearchText))
                filterParts.Add($"Ara: {criteria.SearchText.Trim()}");

            var filterText = filterParts.Count == 0 ? "Tüm Kayıtlar" : string.Join(" / ", filterParts);

            var headers = new[] { "Dönem", "Tür", "Abonelik", "Kurum", "Durum", "Fatura No", "Fatura Tarihi", "Son Ödeme", "Tutar", "Ödenen", "Kalan", "PDF" };
            var rows = list
                .OrderBy(x => x.DueDate)
                .ThenBy(x => x.InvoiceTypeName, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(x => x.SubscriptionName, StringComparer.CurrentCultureIgnoreCase)
                .Select(x =>
                {
                    var pdfState = !x.HasPdf ? "PDF Yok" : (_invoiceRepository.IsPdfMissing(x) ? "PDF Kayıp" : "PDF Var");
                    return (IReadOnlyList<string>)new[]
                    {
                        x.Period,
                        x.InvoiceTypeName,
                        x.SubscriptionName,
                        x.InstitutionName,
                        x.State,
                        x.InvoiceNo,
                        x.InvoiceDate.ToString("dd.MM.yyyy", TurkishCulture),
                        x.DueDate.ToString("dd.MM.yyyy", TurkishCulture),
                        x.Amount.ToString("N2", TurkishCulture),
                        x.PaidAmount.ToString("N2", TurkishCulture),
                        x.RemainingAmount.ToString("N2", TurkishCulture),
                        pdfState,
                    };
                })
                .ToList();

            PdfReportWriter.WriteSimpleTableReport(
                filePath,
                new PdfReportWriter.ReportMeta(
                    AppTitle: ReportMetaConfig.LoadOrDefault(AppPaths.Resolve().RootDirectory).AppTitle,
                    InstitutionName: ReportMetaConfig.LoadOrDefault(AppPaths.Resolve().RootDirectory).InstitutionName,
                    ReportTitle: "FATURA LİSTESİ",
                    ReportPeriod: period,
                    ReportDate: DateTime.Today,
                    CreatedBy: Environment.UserName,
                    FilterText: string.Empty),
                summary: Array.Empty<PdfReportWriter.SummaryItem>(),
                headers,
                rows,
                notes: filterText);

            SetInvoiceStatus($"PDF dosyası oluşturuldu: exports/{fileName}", isError: false);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void SelectInvoicePdfButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Fatura PDF Evrakı Seç",
            Filter = "PDF Dosyaları (*.pdf)|*.pdf",
            CheckFileExists = true,
            Multiselect = false,
        };

        if (dialog.ShowDialog() != true)
        {
            return;
        }

        _pendingPdfSourcePath = dialog.FileName;
        UpdatePdfControls(_selectedInvoice);
        SetInvoiceStatus("PDF seçildi. Kaydet düğmesiyle faturaya eklenecek.", isError: false);
    }

    private void OpenInvoicePdfButton_Click(object sender, RoutedEventArgs e)
    {
        if (_invoiceRepository is null || _selectedInvoice is null)
        {
            return;
        }

        try
        {
            var pdfPath = _invoiceRepository.GetPdfAbsolutePath(_selectedInvoice);
            if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            {
                SetInvoiceStatus("PDF dosyası bulunamadı.", isError: true);
                UpdatePdfControls(_selectedInvoice);
                return;
            }

            Process.Start(new ProcessStartInfo(pdfPath)
            {
                UseShellExecute = true,
            });
        }
        catch (Exception exception) when (exception is InvalidOperationException or IOException or UnauthorizedAccessException or System.ComponentModel.Win32Exception)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void RevealInvoicePdfFolderButton_Click(object sender, RoutedEventArgs e)
    {
        if (_invoiceRepository is null || _selectedInvoice is null)
        {
            return;
        }

        try
        {
            if (_selectedInvoice.HasPdf && _invoiceRepository.PdfFileExists(_selectedInvoice))
            {
                var pdfPath = _invoiceRepository.GetPdfAbsolutePath(_selectedInvoice);
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = $"/select,\"{pdfPath}\"",
                    UseShellExecute = true,
                });
                SetInvoiceStatus("Fatura PDF dosyasi klasorde gosterildi.", isError: false);
                return;
            }

            var pdfDirectory = _invoiceRepository.GetPdfDirectoryAbsolutePath(_selectedInvoice);
            Directory.CreateDirectory(pdfDirectory);
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"\"{pdfDirectory}\"",
                UseShellExecute = true,
            });
            SetInvoiceStatus("Fatura icin beklenen PDF klasoru acildi.", isError: false);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException or Win32Exception)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void SavePaymentButton_Click(object sender, RoutedEventArgs e)
    {
        if (_paymentRepository is null || _selectedInvoice is null)
        {
            return;
        }

        try
        {
            if (PaymentDateInput.SelectedDate is null)
            {
                throw new InvalidOperationException("Ödeme tarihi zorunludur.");
            }

            var payment = _paymentRepository.Add(new PaymentInput(
                _selectedInvoice.Id,
                PaymentDateInput.SelectedDate.Value,
                ParseDecimal(PaymentAmountInput.Text, "Ödeme tutarı"),
                PaymentDescriptionInput.Text));

            RefreshInvoices(_selectedInvoice.Id);
            SelectPayment(payment.Id);
            InvoicesChanged?.Invoke(this, EventArgs.Empty);
            SetInvoiceStatus($"Ödeme kaydedildi: {payment.AmountText}", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void FillRemainingPaymentButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedInvoice is null)
        {
            SetInvoiceStatus("Kalan tutari doldurmak icin once bir fatura secin.", isError: true);
            return;
        }

        ApplyPaymentSuggestion(PaymentEntrySuggestionBuilder.CreateDefault(_selectedInvoice, DateTime.Today));
        SetInvoiceStatus("Odeme taslagi kalan tutarla guncellendi.", isError: false);
    }

    private void UseLastPaymentTemplateButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedInvoice is null)
        {
            SetInvoiceStatus("Son odemeden doldurmak icin once bir fatura secin.", isError: true);
            return;
        }

        if (_payments.Count == 0)
        {
            SetInvoiceStatus("Bu fatura icin daha once kaydedilmis odeme yok.", isError: true);
            return;
        }

        var suggestion = PaymentEntrySuggestionBuilder.CreateFromRecentPayment(_selectedInvoice, _payments, DateTime.Today);
        if (string.IsNullOrWhiteSpace(suggestion.Description))
        {
            SetInvoiceStatus("Son odemelerde kopyalanacak bir aciklama bulunamadi.", isError: true);
            return;
        }

        ApplyPaymentSuggestion(suggestion);
        SetInvoiceStatus("Odeme taslagi son aciklama ve kalan tutarla dolduruldu.", isError: false);
    }

    private void UseSelectedPaymentTemplateButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedInvoice is null)
        {
            SetInvoiceStatus("Secili odemeden doldurmak icin once bir fatura secin.", isError: true);
            return;
        }

        if (_selectedPayment is null)
        {
            SetInvoiceStatus("Secili odemeden doldurmak icin once odeme listesinden bir kayit secin.", isError: true);
            return;
        }

        var suggestion = PaymentEntrySuggestionBuilder.CreateFromSelectedPayment(_selectedInvoice, _selectedPayment, DateTime.Today);
        if (suggestion.Amount <= 0)
        {
            SetInvoiceStatus("Secili odemeden yeni taslak olusturulamadi; faturanin kalan tutari yok.", isError: true);
            return;
        }

        ApplyPaymentSuggestion(suggestion);
        SetInvoiceStatus("Odeme taslagi secili odemeye gore dolduruldu.", isError: false);
    }

    private void PaymentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (PaymentGrid.SelectedItem is Payment payment)
        {
            _selectedPayment = payment;
            UpdatePaymentPdfControls(payment);
            return;
        }

        _selectedPayment = null;
        UpdatePaymentPdfControls(null);
    }

    private void SelectPaymentPdfButton_Click(object sender, RoutedEventArgs e)
    {
        if (_paymentRepository is null || _selectedInvoice is null || _selectedPayment is null)
        {
            return;
        }

        var dialog = new OpenFileDialog
        {
            Title = "Ödeme PDF Evrakı Seç",
            Filter = "PDF Dosyaları (*.pdf)|*.pdf",
            CheckFileExists = true,
            Multiselect = false,
        };

        if (dialog.ShowDialog() != true)
        {
            return;
        }

        try
        {
            var attached = _paymentRepository.AttachPdf(_selectedPayment.Id, dialog.FileName);
            RefreshPaymentControls(_selectedInvoice);
            SelectPayment(attached.Id);
            SetInvoiceStatus("Ödeme PDF evrakı kaydedildi.", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException or IOException or UnauthorizedAccessException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void OpenPaymentPdfButton_Click(object sender, RoutedEventArgs e)
    {
        if (_paymentRepository is null || _selectedPayment is null)
        {
            return;
        }

        try
        {
            var pdfPath = _paymentRepository.GetPdfAbsolutePath(_selectedPayment);
            if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            {
                SetInvoiceStatus("Ödeme PDF dosyası bulunamadı.", isError: true);
                UpdatePaymentPdfControls(_selectedPayment);
                return;
            }

            Process.Start(new ProcessStartInfo(pdfPath)
            {
                UseShellExecute = true,
            });
        }
        catch (Exception exception) when (exception is InvalidOperationException or IOException or UnauthorizedAccessException or System.ComponentModel.Win32Exception)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void InvoiceSubscriptionInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_isInitialized || _isEditingExisting)
        {
            return;
        }

        if (InvoiceSubscriptionInput.SelectedItem is Subscription subscription &&
            string.IsNullOrWhiteSpace(UsageUnitInput.Text))
        {
            UsageUnitInput.Text = subscription.DefaultUsageUnit;
        }
    }

    private InvoiceInput ReadInvoiceInput()
    {
        var subscriptionId = InvoiceSubscriptionInput.SelectedValue is long selectedId
            ? selectedId
            : 0;

        if (!int.TryParse(InvoiceYearInput.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var invoiceYear))
        {
            throw new InvalidOperationException("Dönem yılı geçerli bir sayı olmalıdır.");
        }

        var invoiceMonth = InvoiceMonthInput.SelectedValue is int selectedMonth
            ? selectedMonth
            : 0;

        if (InvoiceDateInput.SelectedDate is null)
        {
            throw new InvalidOperationException("Fatura tarihi zorunludur.");
        }

        if (InvoiceDueDateInput.SelectedDate is null)
        {
            throw new InvalidOperationException("Son ödeme tarihi zorunludur.");
        }

        return new InvoiceInput(
            subscriptionId,
            invoiceYear,
            invoiceMonth,
            InvoiceDateInput.SelectedDate.Value,
            InvoiceDueDateInput.SelectedDate.Value,
            InvoiceNoInput.Text,
            ParseDecimal(InvoiceAmountInput.Text, "Fatura tutarı"),
            ParseDecimal(UsageAmountInput.Text, "Kullanım miktarı"),
            UsageUnitInput.Text,
            InvoiceDescriptionInput.Text);
    }

    private void ClearInvoiceForm()
    {
        _selectedInvoice = null;
        _selectedPayment = null;
        _pendingPdfSourcePath = null;
        _isEditingExisting = false;
        InvoiceGrid.SelectedItem = null;

        var today = DateTime.Today;
        InvoiceFormTitleText.Text = "Yeni Fatura";
        InvoiceSubscriptionInput.SelectedItem = _subscriptions.FirstOrDefault(item => item.IsActive) ?? _subscriptions.FirstOrDefault();
        InvoiceYearInput.Text = today.Year.ToString(CultureInfo.InvariantCulture);
        InvoiceMonthInput.SelectedValue = today.Month;
        InvoiceDateInput.SelectedDate = today;
        InvoiceDueDateInput.SelectedDate = today;
        InvoiceNoInput.Text = string.Empty;
        InvoiceAmountInput.Text = "0,00";
        UsageAmountInput.Text = "0,00";
        UsageUnitInput.Text = (InvoiceSubscriptionInput.SelectedItem as Subscription)?.DefaultUsageUnit ?? string.Empty;
        InvoiceDescriptionInput.Text = string.Empty;
        UpdatePdfControls(null);
        RefreshPaymentControls(null);
        SetInvoiceStatus("Yeni kayıt için alanları doldurun.", isError: false);
    }

    private void ApplyInvoiceDraft(InvoiceDraftTemplate draft)
    {
        _selectedInvoice = null;
        _selectedPayment = null;
        _pendingPdfSourcePath = null;
        _isEditingExisting = false;
        InvoiceGrid.SelectedItem = null;

        InvoiceFormTitleText.Text = "Yeni Fatura";
        InvoiceSubscriptionInput.SelectedValue = draft.SubscriptionId;
        InvoiceYearInput.Text = draft.InvoiceYear.ToString(CultureInfo.InvariantCulture);
        InvoiceMonthInput.SelectedValue = draft.InvoiceMonth;
        InvoiceDateInput.SelectedDate = draft.InvoiceDate;
        InvoiceDueDateInput.SelectedDate = draft.DueDate;
        InvoiceNoInput.Text = draft.InvoiceNo;
        InvoiceAmountInput.Text = draft.Amount.ToString("N2", TurkishCulture);
        UsageAmountInput.Text = draft.UsageAmount.ToString("N2", TurkishCulture);
        UsageUnitInput.Text = draft.UsageUnit;
        InvoiceDescriptionInput.Text = draft.Description;
        UpdatePdfControls(null);
        RefreshPaymentControls(null);
    }

    private static decimal ParseDecimal(string value, string label)
    {
        var trimmed = value.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return 0;
        }

        if (decimal.TryParse(trimmed, NumberStyles.Number, TurkishCulture, out var parsed) ||
            decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out parsed))
        {
            return parsed;
        }

        throw new InvalidOperationException($"{label} geçerli bir sayı olmalıdır.");
    }

    private void SetInvoiceStatus(string message, bool isError)
    {
        InvoiceStatusText.Text = message;
        InvoiceStatusText.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(185, 28, 28))
            : new SolidColorBrush(Color.FromRgb(95, 107, 122));
    }

    private void UpdatePdfControls(Invoice? invoice)
    {
        if (!string.IsNullOrWhiteSpace(_pendingPdfSourcePath))
        {
            SetPdfInfo($"Seçili PDF: {Path.GetFileName(_pendingPdfSourcePath)}. Kaydet ile faturaya eklenecek.", isError: false);
            OpenInvoicePdfButton.IsEnabled = false;
            RevealInvoicePdfFolderButton.IsEnabled = true;
            return;
        }

        if (invoice is null)
        {
            SetPdfInfo("PDF eklemek için dosya seçin; kayıt sırasında faturaya bağlanır.", isError: false);
            OpenInvoicePdfButton.IsEnabled = false;
            RevealInvoicePdfFolderButton.IsEnabled = false;
            return;
        }

        if (!invoice.HasPdf)
        {
            SetPdfInfo("Bu faturaya PDF evrak eklenmemiş.", isError: false);
            OpenInvoicePdfButton.IsEnabled = false;
            RevealInvoicePdfFolderButton.IsEnabled = true;
            return;
        }

        if (_invoiceRepository?.PdfFileExists(invoice) == true)
        {
            SetPdfInfo($"PDF kayıtlı: {invoice.PdfOriginalFileName}", isError: false);
            OpenInvoicePdfButton.IsEnabled = true;
            RevealInvoicePdfFolderButton.IsEnabled = true;
            return;
        }

        SetPdfInfo($"PDF kaydı var ancak dosya bulunamadı: {invoice.PdfFilePath}", isError: true);
        OpenInvoicePdfButton.IsEnabled = false;
        RevealInvoicePdfFolderButton.IsEnabled = true;
    }

    private void SetPdfInfo(string message, bool isError)
    {
        InvoicePdfInfoText.Text = message;
        InvoicePdfInfoText.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(185, 28, 28))
            : new SolidColorBrush(Color.FromRgb(95, 107, 122));
    }

    private void RefreshPaymentControls(Invoice? invoice)
    {
        _selectedPayment = null;
        _payments = Array.Empty<Payment>();
        PaymentGrid.ItemsSource = _payments;
        PaymentGrid.SelectedItem = null;
        PaymentDateInput.SelectedDate = DateTime.Today;
        PaymentAmountInput.Text = "0,00";
        PaymentDescriptionInput.Text = string.Empty;
        UpdatePaymentPdfControls(null);

        if (invoice is null)
        {
            SetPaymentInfo("Ödeme eklemek için önce faturayı kaydedin.", isError: false);
            SetPaymentInputEnabled(false);
            return;
        }

        _payments = _paymentRepository?.GetForInvoice(invoice.Id) ?? Array.Empty<Payment>();
        PaymentGrid.ItemsSource = _payments;
        SelectPayment();

        if (invoice.Status == "canceled")
        {
            SetPaymentInfo($"{invoice.PaymentSummaryText}. İptal faturaya ödeme eklenemez.", isError: false);
            SetPaymentInputEnabled(false);
            return;
        }

        if (invoice.RemainingAmount <= 0)
        {
            SetPaymentInfo($"{invoice.PaymentSummaryText}. Fatura tamamen ödendi.", isError: false);
            SetPaymentInputEnabled(false);
            return;
        }

        PaymentAmountInput.Text = invoice.RemainingAmount.ToString("N2", TurkishCulture);
        SetPaymentInfo(invoice.PaymentSummaryText, isError: false);
        SetPaymentInputEnabled(true);
    }

    private void SetPaymentInfo(string message, bool isError)
    {
        InvoicePaymentSummaryText.Text = message;
        InvoicePaymentSummaryText.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(185, 28, 28))
            : new SolidColorBrush(Color.FromRgb(95, 107, 122));
    }

    private void SetPaymentInputEnabled(bool isEnabled)
    {
        PaymentDateInput.IsEnabled = isEnabled;
        PaymentAmountInput.IsEnabled = isEnabled;
        PaymentDescriptionInput.IsEnabled = isEnabled;
        FillRemainingPaymentButton.IsEnabled = isEnabled;
        UseSelectedPaymentTemplateButton.IsEnabled = isEnabled && _selectedPayment is not null;
        UseLastPaymentTemplateButton.IsEnabled = isEnabled;
        SavePaymentButton.IsEnabled = isEnabled;
    }

    private void ApplyPaymentSuggestion(PaymentEntrySuggestion suggestion)
    {
        PaymentDateInput.SelectedDate = suggestion.PaymentDate;
        PaymentAmountInput.Text = suggestion.Amount.ToString("N2", TurkishCulture);
        PaymentDescriptionInput.Text = suggestion.Description;
    }

    private void SelectPayment(long? paymentId = null)
    {
        var selected = paymentId is null
            ? _payments.FirstOrDefault()
            : _payments.FirstOrDefault(item => item.Id == paymentId.Value) ?? _payments.FirstOrDefault();

        PaymentGrid.SelectedItem = selected;
        _selectedPayment = selected;
        UpdatePaymentPdfControls(selected);
    }

    private void UpdatePaymentPdfControls(Payment? payment)
    {
        if (UseSelectedPaymentTemplateButton is not null)
        {
            UseSelectedPaymentTemplateButton.IsEnabled = SavePaymentButton.IsEnabled && payment is not null;
        }
        if (payment is null)
        {
            SetPaymentPdfInfo("PDF eklemek için ödeme kaydı seçin.", isError: false);
            SelectPaymentPdfButton.IsEnabled = false;
            OpenPaymentPdfButton.IsEnabled = false;
            return;
        }

        SelectPaymentPdfButton.IsEnabled = true;
        if (!payment.HasPdf)
        {
            SetPaymentPdfInfo("Bu ödeme kaydına PDF evrak eklenmemiş.", isError: false);
            OpenPaymentPdfButton.IsEnabled = false;
            return;
        }

        if (_paymentRepository?.PdfFileExists(payment) == true)
        {
            SetPaymentPdfInfo($"PDF kayıtlı: {payment.PdfOriginalFileName}", isError: false);
            OpenPaymentPdfButton.IsEnabled = true;
            return;
        }

        SetPaymentPdfInfo($"PDF kaydı var ancak dosya bulunamadı: {payment.PdfFilePath}", isError: true);
        OpenPaymentPdfButton.IsEnabled = false;
    }

    private void SetPaymentPdfInfo(string message, bool isError)
    {
        PaymentPdfInfoText.Text = message;
        PaymentPdfInfoText.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(185, 28, 28))
            : new SolidColorBrush(Color.FromRgb(95, 107, 122));
    }

    private sealed record MonthOption(int Value, string Label);

    private sealed record InvoiceTypeFilterOption(string Label, long? InvoiceTypeId);

    private sealed record SubscriptionFilterOption(string Label, long? SubscriptionId);

    private sealed record YearFilterOption(string Label, int? Year);

    private sealed record MonthFilterOption(string Label, int? Month);

    private sealed record PaymentStatusFilterOption(string Label, InvoicePaymentStatusFilter Value);

    private sealed record PdfStatusFilterOption(string Label, InvoicePdfStatusFilter Value);
}



