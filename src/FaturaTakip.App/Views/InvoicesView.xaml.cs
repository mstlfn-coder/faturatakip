using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    private string? _invoiceReviewModeLabel;
    private string? _invoiceReviewContextLabel;
    private long? _invoiceReviewPreferredInvoiceId;
    private string? _lastInvokedReviewActionKey;
    private string _rootDirectory = string.Empty;
    private InvoiceReviewPreferences _invoiceReviewPreferences = InvoiceReviewPreferences.Default;

    public event EventHandler? InvoicesChanged;

    public InvoicesView()
    {
        InitializeComponent();
    }

    public void Initialize(string databasePath)
    {
        _rootDirectory = AppPaths.Resolve().RootDirectory;
        _invoiceReviewPreferences = InvoiceReviewPreferences.LoadOrDefault(_rootDirectory);
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
        InvoiceReviewStatusFilterInput.ItemsSource = new List<ReviewStatusFilterOption>
        {
            new("Tüm İnceleme", InvoiceReviewStatusFilter.All),
            new("İncelendi", InvoiceReviewStatusFilter.Reviewed),
            new("İncelenmedi", InvoiceReviewStatusFilter.Unreviewed),
        };
        InvoiceReviewStatusFilterInput.SelectedIndex = 0;
        ShowInvoiceReviewContextCheckBox.IsChecked = _invoiceReviewPreferences.ShowContext;

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

    public void ShowUnreviewedInvoices()
    {
        if (!_isInitialized)
        {
            return;
        }

        RefreshSubscriptionLists();
        RefreshInvoices(_selectedInvoice?.Id);
        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        SelectReviewStatusFilter(InvoiceReviewStatusFilter.Unreviewed);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("İncelenmedi filtresi dashboard üzerinden uygulandı.", isError: false);
    }

    public void StartUnreviewedReviewMode(long? preferredInvoiceId = null, string? contextLabel = null)
    {
        if (!_isInitialized)
        {
            return;
        }

        RefreshSubscriptionLists();
        RefreshInvoices(_selectedInvoice?.Id);
        StartInvoiceReviewMode(
            reviewModeLabel: "İncelenmedi İnceleme",
            applyModeFilter: () => SelectReviewStatusFilter(InvoiceReviewStatusFilter.Unreviewed),
            successMessage: "İncelenmedi inceleme akışı rapor üzerinden başlatıldı.",
            preferredInvoiceId: preferredInvoiceId,
            contextLabel: contextLabel);
    }

    public void StartOverdueReviewMode(long? preferredInvoiceId = null, string? contextLabel = null)
    {
        if (!_isInitialized)
        {
            return;
        }

        RefreshSubscriptionLists();
        RefreshInvoices(_selectedInvoice?.Id);
        StartInvoiceReviewMode(
            reviewModeLabel: "Gecikmiş",
            applyModeFilter: () => SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Overdue),
            successMessage: "Gecikmiş inceleme akışı rapor üzerinden başlatıldı.",
            preferredInvoiceId: preferredInvoiceId,
            contextLabel: contextLabel);
    }

    public void StartMissingPdfReviewMode(long? preferredInvoiceId = null, string? contextLabel = null)
    {
        if (!_isInitialized)
        {
            return;
        }

        RefreshSubscriptionLists();
        RefreshInvoices(_selectedInvoice?.Id);
        StartInvoiceReviewMode(
            reviewModeLabel: "PDF Eksik",
            applyModeFilter: () => SelectPdfStatusFilter(InvoicePdfStatusFilter.MissingPdf),
            successMessage: "PDF eksik inceleme akışı rapor üzerinden başlatıldı.",
            preferredInvoiceId: preferredInvoiceId,
            contextLabel: contextLabel);
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

        _invoiceReviewModeLabel = null;
        ApplyFiltersToGrid(_selectedInvoice?.Id);
    }

    private void ClearInvoiceFiltersButton_Click(object sender, RoutedEventArgs e)
    {
        _invoiceReviewModeLabel = null;
        InvoiceSearchInput.Text = string.Empty;
        InvoiceTypeFilterInput.SelectedIndex = 0;
        InvoiceSubscriptionFilterInput.SelectedIndex = 0;
        InvoiceYearFilterInput.SelectedIndex = 0;
        InvoiceMonthFilterInput.SelectedIndex = 0;
        InvoicePaymentStatusFilterInput.SelectedIndex = 0;
        InvoicePdfStatusFilterInput.SelectedIndex = 0;
        InvoiceReviewStatusFilterInput.SelectedIndex = 0;
        ApplyFiltersToGrid(_selectedInvoice?.Id);
    }

    private void QuickFilterCurrentMonthButton_Click(object sender, RoutedEventArgs e)
    {
        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        SelectYearFilter(DateTime.Today.Year);
        SelectMonthFilter(DateTime.Today.Month);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("Bu ay filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private void QuickFilterUnpaidButton_Click(object sender, RoutedEventArgs e)
    {
        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Unpaid);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("Ödenmemiş filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private void QuickFilterOverdueButton_Click(object sender, RoutedEventArgs e)
    {
        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Overdue);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("Gecikmiş filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private void QuickFilterMissingPdfButton_Click(object sender, RoutedEventArgs e)
    {
        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        SelectPdfStatusFilter(InvoicePdfStatusFilter.MissingPdf);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("PDF eksik filtresi uygulandi; listede ilk kayda odaklanildi.", isError: false);
    }

    private void QuickFilterUnreviewedButton_Click(object sender, RoutedEventArgs e)
    {
        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        SelectReviewStatusFilter(InvoiceReviewStatusFilter.Unreviewed);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus("İncelenmedi filtresi uygulandı; listede ilk kayda odaklanıldı.", isError: false);
    }

    private void StartMissingPdfReviewButton_Click(object sender, RoutedEventArgs e)
    {
        StartInvoiceReviewMode(
            "PDF Eksik",
            () => SelectPdfStatusFilter(InvoicePdfStatusFilter.MissingPdf),
            "PDF Eksik kontrol turu baslatildi; listede ilk kayda odaklanildi.");
    }

    private void StartOverdueReviewButton_Click(object sender, RoutedEventArgs e)
    {
        StartInvoiceReviewMode(
            "Gecikmiş",
            () => SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Overdue),
            "Gecikmiş kontrol turu baslatildi; listede ilk kayda odaklanildi.");
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
            ReviewStatus: (InvoiceReviewStatusFilterInput.SelectedItem as ReviewStatusFilterOption)?.Value ?? InvoiceReviewStatusFilter.All,
            SearchText: InvoiceSearchInput.Text);
    }

    private Invoice? ApplyFiltersToGridCore(long? selectedId = null, bool selectFirstIfAvailable = false)
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
            UpdateInvoiceReviewNavigationControls();
            return null;
        }

        InvoiceGrid.SelectedItem = selected;
        InvoiceGrid.ScrollIntoView(selected);
        ApplySelectedInvoice(selected);
        return selected;
    }

    private void ApplyFiltersToGrid(long? selectedId = null, bool selectFirstIfAvailable = false)
    {
        _ = ApplyFiltersToGridCore(selectedId, selectFirstIfAvailable);
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
        InvoiceReviewStatusFilterInput.SelectedIndex = 0;
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

    private bool SelectInvoiceTypeFilter(string invoiceTypeName)
    {
        if (InvoiceTypeFilterInput.ItemsSource is not IEnumerable<InvoiceTypeFilterOption> options)
        {
            return false;
        }

        var match = options.FirstOrDefault(item =>
            item.InvoiceTypeId is not null &&
            string.Equals(item.Label, invoiceTypeName, StringComparison.CurrentCultureIgnoreCase));
        if (match is null)
        {
            return false;
        }

        InvoiceTypeFilterInput.SelectedItem = match;
        return true;
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

    private void SelectReviewStatusFilter(InvoiceReviewStatusFilter status)
    {
        if (InvoiceReviewStatusFilterInput.ItemsSource is not IEnumerable<ReviewStatusFilterOption> options)
        {
            return;
        }

        InvoiceReviewStatusFilterInput.SelectedItem = options.FirstOrDefault(item => item.Value == status)
            ?? options.FirstOrDefault();
    }

    private void InvoiceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (InvoiceGrid.SelectedItem is Invoice invoice)
        {
            ApplySelectedInvoice(invoice);
        }
    }

    private void InvoicesView_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) != (ModifierKeys.Control | ModifierKeys.Shift))
        {
            return;
        }

        switch (e.Key)
        {
            case Key.Right:
                MoveSelectedInvoice(1);
                e.Handled = true;
                break;
            case Key.Left:
                MoveSelectedInvoice(-1);
                e.Handled = true;
                break;
            case Key.O:
                if (TryOpenSelectedInvoicePdf(out var openMessage))
                {
                    CompleteInvoiceReviewAction(openMessage);
                }

                e.Handled = true;
                break;
            case Key.K:
                if (TryRevealSelectedInvoicePdfFolder(out var revealMessage))
                {
                    CompleteInvoiceReviewAction(revealMessage);
                }

                e.Handled = true;
                break;
            case Key.B:
                ToggleInvoiceReviewContextVisibility();
                e.Handled = true;
                break;
            case Key.C:
                CopyInvoiceReviewContextToClipboard();
                e.Handled = true;
                break;
            case Key.I:
                FocusInvoiceFromReviewContext();
                e.Handled = true;
                break;
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
        UpdateInvoiceReviewNoteControls(invoice);
        UpdatePdfControls(invoice);
        RefreshPaymentControls(invoice);
        SetInvoiceStatus($"Seçili kayıt: {invoice.InvoiceNo}", isError: false);
    }

    private void PreviousInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        MoveSelectedInvoice(-1);
    }

    private void NextInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        MoveSelectedInvoice(1);
    }

    private void OpenInvoicePdfAndNextButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryOpenSelectedInvoicePdf(out var actionMessage))
        {
            return;
        }

        CompleteInvoiceReviewAction(actionMessage);
    }

    private void SaveInvoiceReviewNoteButton_Click(object sender, RoutedEventArgs e)
    {
        if (_invoiceRepository is null || _selectedInvoice is null)
        {
            SetInvoiceStatus("İnceleme notu kaydetmek için önce bir fatura seçin.", isError: true);
            return;
        }

        try
        {
            var updated = _invoiceRepository.UpdateReviewStatus(_selectedInvoice.Id, InvoiceReviewNoteInput.Text, DateTimeOffset.Now);
            RefreshInvoices(updated.Id);
            InvoicesChanged?.Invoke(this, EventArgs.Empty);
            SetInvoiceStatus("İnceleme notu kaydedildi.", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void ClearInvoiceReviewNoteButton_Click(object sender, RoutedEventArgs e)
    {
        if (_invoiceRepository is null || _selectedInvoice is null)
        {
            SetInvoiceStatus("İnceleme işaretini temizlemek için önce bir fatura seçin.", isError: true);
            return;
        }

        try
        {
            var updated = _invoiceRepository.ClearReviewStatus(_selectedInvoice.Id);
            RefreshInvoices(updated.Id);
            InvoicesChanged?.Invoke(this, EventArgs.Empty);
            SetInvoiceStatus("İnceleme işareti temizlendi.", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private void MarkReviewedAndNextButton_Click(object sender, RoutedEventArgs e)
    {
        MarkSelectedInvoiceReviewedAndMoveNext();
    }

    private void RevealInvoicePdfFolderAndNextButton_Click(object sender, RoutedEventArgs e)
    {
        if (!TryRevealSelectedInvoicePdfFolder(out var actionMessage))
        {
            return;
        }

        CompleteInvoiceReviewAction(actionMessage);
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
            var list = GetVisibleInvoices();
            if (list.Count == 0)
            {
                SetInvoiceStatus("Excel aktarimi icin gorunur listede kayit yok.", isError: true);
                return;
            }

            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);
            var exportContext = BuildInvoiceExportContext();
            var fileName = $"faturalar-{exportContext.FileSlug}-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx";
            var filePath = Path.Combine(exportsDir, fileName);

            ExcelExportWriter.WriteInvoices(filePath, list, invoice => _invoiceRepository.IsPdfMissing(invoice));

            SetInvoiceStatus($"Filtreli Excel dosyasi olusturuldu ({exportContext.SummaryLabel}): exports/{fileName}", isError: false);
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
            var list = GetVisibleInvoices();
            if (list.Count == 0)
            {
                SetInvoiceStatus("PDF aktarimi icin gorunur listede kayit yok.", isError: true);
                return;
            }

            var paths = AppPaths.Resolve();
            var exportsDir = Path.Combine(paths.RootDirectory, "exports");
            Directory.CreateDirectory(exportsDir);
            var exportContext = BuildInvoiceExportContext();
            var fileName = $"faturalar-{exportContext.FileSlug}-{DateTime.Now:yyyyMMdd-HHmmss}.pdf";
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

            SetInvoiceStatus($"Filtreli PDF dosyasi olusturuldu ({exportContext.SummaryLabel}): exports/{fileName}", isError: false);
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            SetInvoiceStatus(exception.Message, isError: true);
        }
    }

    private List<Invoice> GetVisibleInvoices()
    {
        var items = InvoiceGrid.ItemsSource as IEnumerable<Invoice> ?? Array.Empty<Invoice>();
        return items.ToList();
    }

    private InvoiceExportContextBuilder.ExportContext BuildInvoiceExportContext()
    {
        var criteria = ReadFilterCriteria();
        return InvoiceExportContextBuilder.Build(
            criteria,
            DateTime.Today,
            (InvoiceTypeFilterInput.SelectedItem as InvoiceTypeFilterOption)?.Label,
            (InvoiceSubscriptionFilterInput.SelectedItem as SubscriptionFilterOption)?.Label);
    }

    private void MoveSelectedInvoice(int offset)
    {
        var visibleInvoices = GetVisibleInvoices();
        if (_selectedInvoice is null || visibleInvoices.Count == 0)
        {
            SetInvoiceStatus("Liste icinde ilerlemek icin once bir fatura secin.", isError: true);
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        var currentIndex = visibleInvoices.FindIndex(item => item.Id == _selectedInvoice.Id);
        if (!InvoiceReviewNavigator.TryMove(currentIndex, visibleInvoices.Count, offset, out var targetIndex))
        {
            var boundaryLabel = offset < 0 ? "Ilk kayittasiniz." : "Son kayittasiniz.";
            SetInvoiceStatus(boundaryLabel, isError: false);
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        var targetInvoice = visibleInvoices[targetIndex];
        InvoiceGrid.SelectedItem = targetInvoice;
        InvoiceGrid.ScrollIntoView(targetInvoice);
        ApplySelectedInvoice(targetInvoice);
        SetInvoiceStatus($"Kontrol turu: {targetIndex + 1}/{visibleInvoices.Count} - {targetInvoice.InvoiceNo}", isError: false);
    }

    private void UpdateInvoiceReviewNavigationControls()
    {
        if (PreviousInvoiceButton is null ||
            NextInvoiceButton is null ||
            InvoiceReviewHintText is null)
        {
            return;
        }

        var visibleInvoices = GetVisibleInvoices();
        var contextLabel = ShowInvoiceReviewContextCheckBox?.IsChecked == true
            ? _invoiceReviewContextLabel
            : null;
        UpdateInvoiceReviewContextPresentation(contextLabel);

        if (_selectedInvoice is null || visibleInvoices.Count == 0)
        {
            PreviousInvoiceButton.IsEnabled = false;
            NextInvoiceButton.IsEnabled = false;
            InvoiceReviewHintText.Text = InvoiceReviewNavigator.BuildHint(_invoiceReviewModeLabel, null, visibleInvoices.Count, includeShortcuts: true);
            return;
        }

        var currentIndex = visibleInvoices.FindIndex(item => item.Id == _selectedInvoice.Id);
        if (currentIndex < 0)
        {
            PreviousInvoiceButton.IsEnabled = false;
            NextInvoiceButton.IsEnabled = false;
            InvoiceReviewHintText.Text = InvoiceReviewNavigator.BuildHint(_invoiceReviewModeLabel, null, visibleInvoices.Count, includeShortcuts: true);
            return;
        }

        PreviousInvoiceButton.IsEnabled = currentIndex > 0;
        NextInvoiceButton.IsEnabled = currentIndex < visibleInvoices.Count - 1;
        InvoiceReviewHintText.Text = InvoiceReviewNavigator.BuildHint(_invoiceReviewModeLabel, currentIndex, visibleInvoices.Count, includeShortcuts: true);
    }

    private void ShowInvoiceReviewContextCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        _invoiceReviewPreferences = new InvoiceReviewPreferences(
            ShowContext: ShowInvoiceReviewContextCheckBox.IsChecked == true);
        TrySaveInvoiceReviewPreferences();
        UpdateInvoiceReviewNavigationControls();
    }

    private void ToggleInvoiceReviewContextVisibility()
    {
        if (ShowInvoiceReviewContextCheckBox is null)
        {
            return;
        }

        ShowInvoiceReviewContextCheckBox.IsChecked = ShowInvoiceReviewContextCheckBox.IsChecked != true;
        SetInvoiceStatus(
            ShowInvoiceReviewContextCheckBox.IsChecked == true
                ? "İnceleme bağlamı gösteriliyor."
                : "İnceleme bağlamı gizlendi.",
            isError: false);
    }

    private void CopyInvoiceReviewContextButton_Click(object sender, RoutedEventArgs e)
    {
        CopyInvoiceReviewContextToClipboard();
    }

    private void ApplyInvoiceReviewContextFilterButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyInvoiceReviewContextFilter();
    }

    private void ApplyInvoiceReviewContextNarrowButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyInvoiceReviewContextNarrow();
    }

    private void FocusInvoiceFromReviewContextButton_Click(object sender, RoutedEventArgs e)
    {
        FocusInvoiceFromReviewContext();
    }

    private void ApplyInvoiceReviewContextPeriodButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyInvoiceReviewContextPeriod();
    }

    private void ApplyInvoiceReviewContextTypeButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyInvoiceReviewContextType();
    }

    private void ApplyInvoiceReviewContextInvoiceNoButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyInvoiceReviewContextInvoiceNo();
    }

    private void InvoiceReviewActionBadgeButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string actionKey })
        {
            return;
        }

        _lastInvokedReviewActionKey = actionKey;
        UpdateInvoiceReviewContextPresentation(ShowInvoiceReviewContextCheckBox?.IsChecked == true ? _invoiceReviewContextLabel : null);

        switch (actionKey)
        {
            case "focus":
                FocusInvoiceFromReviewContext();
                break;
            case "narrow":
                ApplyInvoiceReviewContextNarrow();
                break;
            case "filter":
                ApplyInvoiceReviewContextFilter();
                break;
            case "period":
                ApplyInvoiceReviewContextPeriod();
                break;
            case "type":
                ApplyInvoiceReviewContextType();
                break;
            case "invoice_no":
                ApplyInvoiceReviewContextInvoiceNo();
                break;
        }
    }

    private List<string> ApplyInvoiceReviewContextSecondaryHints()
    {
        var appliedParts = new List<string>();

        if (InvoiceReviewContextFormatter.TryResolvePeriod(_invoiceReviewContextLabel, out var year, out var month))
        {
            SelectYearFilter(year);
            SelectMonthFilter(month);
            appliedParts.Add($"{year:D4}-{month:D2}");
        }

        if (InvoiceReviewContextFormatter.TryResolveInvoiceTypeName(_invoiceReviewContextLabel, out var invoiceTypeName) &&
            SelectInvoiceTypeFilter(invoiceTypeName))
        {
            appliedParts.Add(invoiceTypeName);
        }

        if (InvoiceReviewContextFormatter.TryResolveInvoiceNumber(_invoiceReviewContextLabel, out var invoiceNumber))
        {
            InvoiceSearchInput.Text = invoiceNumber;
            appliedParts.Add(invoiceNumber);
        }

        return appliedParts;
    }

    private void CopyInvoiceReviewContextToClipboard()
    {
        if (string.IsNullOrWhiteSpace(_invoiceReviewContextLabel))
        {
            SetInvoiceStatus("Kopyalanacak bağlam bilgisi yok.", isError: true);
            return;
        }

        try
        {
            Clipboard.SetText(_invoiceReviewContextLabel);
            SetInvoiceStatus("İnceleme bağlamı panoya kopyalandı.", isError: false);
        }
        catch (Exception exception) when (exception is ExternalException or InvalidOperationException)
        {
            SetInvoiceStatus($"Bağlam panoya kopyalanamadı: {exception.Message}", isError: true);
        }
    }

    private void ApplyInvoiceReviewContextFilter()
    {
        if (!InvoiceReviewContextFormatter.TryResolveSuggestedFilter(_invoiceReviewContextLabel, out var suggestedFilter))
        {
            SetInvoiceStatus("Bağlamdan uygulanabilir bir filtre çıkarılamadı.", isError: true);
            return;
        }

        _invoiceReviewModeLabel = null;
        ResetQuickFilters();

        switch (suggestedFilter)
        {
            case InvoiceReviewContextFormatter.SuggestedFilter.Unreviewed:
                SelectReviewStatusFilter(InvoiceReviewStatusFilter.Unreviewed);
                ApplyFiltersToGrid(selectFirstIfAvailable: true);
                SetInvoiceStatus("Bağlamdan 'İncelenmedi' filtresi uygulandı.", isError: false);
                break;
            case InvoiceReviewContextFormatter.SuggestedFilter.Overdue:
                SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Overdue);
                ApplyFiltersToGrid(selectFirstIfAvailable: true);
                SetInvoiceStatus("Bağlamdan 'Gecikmiş' filtresi uygulandı.", isError: false);
                break;
            case InvoiceReviewContextFormatter.SuggestedFilter.MissingPdf:
                SelectPdfStatusFilter(InvoicePdfStatusFilter.MissingPdf);
                ApplyFiltersToGrid(selectFirstIfAvailable: true);
                SetInvoiceStatus("Bağlamdan 'PDF Eksik' filtresi uygulandı.", isError: false);
                break;
        }
    }

    private void ApplyInvoiceReviewContextNarrow()
    {
        var hasSuggestedFilter = InvoiceReviewContextFormatter.TryResolveSuggestedFilter(_invoiceReviewContextLabel, out var suggestedFilter);
        var hasPeriod = InvoiceReviewContextFormatter.TryResolvePeriod(_invoiceReviewContextLabel, out var year, out var month);
        var hasInvoiceType = InvoiceReviewContextFormatter.TryResolveInvoiceTypeName(_invoiceReviewContextLabel, out var invoiceTypeName);
        var hasInvoiceNumber = InvoiceReviewContextFormatter.TryResolveInvoiceNumber(_invoiceReviewContextLabel, out var invoiceNumber);

        if (!hasSuggestedFilter && !hasPeriod && !hasInvoiceType && !hasInvoiceNumber)
        {
            SetInvoiceStatus("Bağlamdan daraltılacak bir ipucu çıkarılamadı.", isError: true);
            return;
        }

        _invoiceReviewModeLabel = null;
        ResetQuickFilters();

        var appliedParts = new List<string>();
        if (hasSuggestedFilter)
        {
            switch (suggestedFilter)
            {
                case InvoiceReviewContextFormatter.SuggestedFilter.Unreviewed:
                    SelectReviewStatusFilter(InvoiceReviewStatusFilter.Unreviewed);
                    appliedParts.Add("İncelenmedi");
                    break;
                case InvoiceReviewContextFormatter.SuggestedFilter.Overdue:
                    SelectPaymentStatusFilter(InvoicePaymentStatusFilter.Overdue);
                    appliedParts.Add("Gecikmiş");
                    break;
                case InvoiceReviewContextFormatter.SuggestedFilter.MissingPdf:
                    SelectPdfStatusFilter(InvoicePdfStatusFilter.MissingPdf);
                    appliedParts.Add("PDF Eksik");
                    break;
            }
        }

        if (hasPeriod)
        {
            SelectYearFilter(year);
            SelectMonthFilter(month);
            appliedParts.Add($"{year:D4}-{month:D2}");
        }

        if (hasInvoiceType && SelectInvoiceTypeFilter(invoiceTypeName))
        {
            appliedParts.Add(invoiceTypeName);
        }

        if (hasInvoiceNumber)
        {
            InvoiceSearchInput.Text = invoiceNumber;
            appliedParts.Add(invoiceNumber);
        }

        var selectedInvoice = ApplyFiltersToGridCore(selectedId: _invoiceReviewPreferredInvoiceId, selectFirstIfAvailable: true);
        if (selectedInvoice is null)
        {
            SetInvoiceStatus($"Bağlam daraltması uygulandı fakat eslesen kayit bulunamadi: {string.Join(" + ", appliedParts)}", isError: true);
            return;
        }

        if (_invoiceReviewPreferredInvoiceId is not null && selectedInvoice.Id == _invoiceReviewPreferredInvoiceId.Value)
        {
            SetInvoiceStatus($"Bağlam daraltması uygulandı ve baglam kaydina odaklanildi: {selectedInvoice.InvoiceNo}", isError: false);
            return;
        }

        if (_invoiceReviewPreferredInvoiceId is not null)
        {
            SetInvoiceStatus($"Bağlam daraltması uygulandı; baglam kaydi bulunamadigi icin en uygun kayda odaklanildi: {selectedInvoice.InvoiceNo}", isError: false);
            return;
        }

        SetInvoiceStatus($"Bağlam daraltması uygulandı: {string.Join(" + ", appliedParts)}", isError: false);
    }

    private void FocusInvoiceFromReviewContext()
    {
        var hasSuggestedFilter = InvoiceReviewContextFormatter.TryResolveSuggestedFilter(_invoiceReviewContextLabel, out var suggestedFilter);
        var hasPreferredInvoice = _invoiceReviewPreferredInvoiceId is not null;
        var appliedSecondaryParts = Array.Empty<string>();

        if (!hasSuggestedFilter &&
            !hasPreferredInvoice &&
            !InvoiceReviewContextFormatter.TryResolvePeriod(_invoiceReviewContextLabel, out _, out _) &&
            !InvoiceReviewContextFormatter.TryResolveInvoiceTypeName(_invoiceReviewContextLabel, out _) &&
            !InvoiceReviewContextFormatter.TryResolveInvoiceNumber(_invoiceReviewContextLabel, out _))
        {
            SetInvoiceStatus("Bağlamdan kurulacak bir inceleme akışı çıkarılamadı.", isError: true);
            return;
        }

        switch (hasSuggestedFilter)
        {
            case true when suggestedFilter == InvoiceReviewContextFormatter.SuggestedFilter.Unreviewed:
                StartUnreviewedReviewMode(_invoiceReviewPreferredInvoiceId, _invoiceReviewContextLabel);
                break;
            case true when suggestedFilter == InvoiceReviewContextFormatter.SuggestedFilter.Overdue:
                StartOverdueReviewMode(_invoiceReviewPreferredInvoiceId, _invoiceReviewContextLabel);
                break;
            case true when suggestedFilter == InvoiceReviewContextFormatter.SuggestedFilter.MissingPdf:
                StartMissingPdfReviewMode(_invoiceReviewPreferredInvoiceId, _invoiceReviewContextLabel);
                break;
            default:
                ResetQuickFilters();
                _invoiceReviewModeLabel = null;
                ApplyFiltersToGrid(selectedId: _invoiceReviewPreferredInvoiceId, selectFirstIfAvailable: hasPreferredInvoice);
                break;
        }

        appliedSecondaryParts = ApplyInvoiceReviewContextSecondaryHints().ToArray();
        var selectedInvoice = ApplyFiltersToGridCore(selectedId: _invoiceReviewPreferredInvoiceId, selectFirstIfAvailable: true);

        if (selectedInvoice is null)
        {
            var detailText = appliedSecondaryParts.Length == 0
                ? string.Empty
                : $" ({string.Join(" + ", appliedSecondaryParts)})";
            SetInvoiceStatus($"Bağlamdan inceleme görünümü kuruldu fakat eslesen kayit bulunamadi{detailText}.", isError: true);
            return;
        }

        var modeLabel = hasSuggestedFilter
            ? suggestedFilter switch
            {
                InvoiceReviewContextFormatter.SuggestedFilter.Unreviewed => "İncelenmedi",
                InvoiceReviewContextFormatter.SuggestedFilter.Overdue => "Gecikmiş",
                InvoiceReviewContextFormatter.SuggestedFilter.MissingPdf => "PDF Eksik",
                _ => "Bağlam"
            }
            : "Bağlam";
        var suffix = appliedSecondaryParts.Length == 0
            ? string.Empty
            : $" ({string.Join(" + ", appliedSecondaryParts)})";

        if (hasPreferredInvoice && selectedInvoice.Id == _invoiceReviewPreferredInvoiceId)
        {
            SetInvoiceStatus($"Bağlamdan {modeLabel} incelemesi kuruldu ve kayda odaklanıldı{suffix}: {selectedInvoice.InvoiceNo}", isError: false);
            return;
        }

        if (hasPreferredInvoice)
        {
            SetInvoiceStatus($"Bağlamdan {modeLabel} incelemesi kuruldu; baglam kaydi bulunamadigi icin en uygun kayda gecildi{suffix}: {selectedInvoice.InvoiceNo}", isError: false);
            return;
        }

        SetInvoiceStatus($"Bağlamdan {modeLabel} incelemesi kuruldu{suffix}: {selectedInvoice.InvoiceNo}", isError: false);
    }

    private void ApplyInvoiceReviewContextPeriod()
    {
        if (!InvoiceReviewContextFormatter.TryResolvePeriod(_invoiceReviewContextLabel, out var year, out var month))
        {
            SetInvoiceStatus("Bağlamdan uygulanabilir bir dönem çıkarılamadı.", isError: true);
            return;
        }

        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        SelectYearFilter(year);
        SelectMonthFilter(month);
        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus($"Bağlamdan dönem filtresi uygulandı: {year:D4}-{month:D2}", isError: false);
    }

    private void ApplyInvoiceReviewContextType()
    {
        if (!InvoiceReviewContextFormatter.TryResolveInvoiceTypeName(_invoiceReviewContextLabel, out var invoiceTypeName))
        {
            SetInvoiceStatus("Bağlamdan uygulanabilir bir fatura türü çıkarılamadı.", isError: true);
            return;
        }

        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        if (!SelectInvoiceTypeFilter(invoiceTypeName))
        {
            SetInvoiceStatus($"Bağlamdaki fatura türü listede bulunamadı: {invoiceTypeName}", isError: true);
            return;
        }

        ApplyFiltersToGrid(selectFirstIfAvailable: true);
        SetInvoiceStatus($"Bağlamdan fatura türü filtresi uygulandı: {invoiceTypeName}", isError: false);
    }

    private void ApplyInvoiceReviewContextInvoiceNo()
    {
        if (!InvoiceReviewContextFormatter.TryResolveInvoiceNumber(_invoiceReviewContextLabel, out var invoiceNumber))
        {
            SetInvoiceStatus("Bağlamdan uygulanabilir bir fatura no çıkarılamadı.", isError: true);
            return;
        }

        _invoiceReviewModeLabel = null;
        ResetQuickFilters();
        InvoiceSearchInput.Text = invoiceNumber;
        ApplyFiltersToGrid(selectedId: _invoiceReviewPreferredInvoiceId, selectFirstIfAvailable: true);
        SetInvoiceStatus($"Bağlamdan fatura no araması uygulandı: {invoiceNumber}", isError: false);
    }

    private void UpdateInvoiceReviewContextPresentation(string? contextLabel)
    {
        var hasContext = !string.IsNullOrWhiteSpace(contextLabel);
        var hasSuggestedFilter = InvoiceReviewContextFormatter.TryResolveSuggestedFilter(_invoiceReviewContextLabel, out _);
        var hasPreferredInvoice = _invoiceReviewPreferredInvoiceId is not null;
        var hasPeriod = InvoiceReviewContextFormatter.TryResolvePeriod(_invoiceReviewContextLabel, out _, out _);
        var hasInvoiceType = InvoiceReviewContextFormatter.TryResolveInvoiceTypeName(_invoiceReviewContextLabel, out _);
        var hasInvoiceNumber = InvoiceReviewContextFormatter.TryResolveInvoiceNumber(_invoiceReviewContextLabel, out _);
        var hasAnyNarrowing = hasSuggestedFilter || hasPeriod || hasInvoiceType || hasInvoiceNumber;
        var availableActionBadges = new List<ReviewActionBadge>();
        if (hasSuggestedFilter || hasPreferredInvoice || hasPeriod || hasInvoiceType || hasInvoiceNumber)
        {
            availableActionBadges.Add(new ReviewActionBadge("AKS", "Baglamdan Incele", "context", "focus", "Baglamdan inceleme akisini calistir", _lastInvokedReviewActionKey == "focus"));
        }
        if (hasAnyNarrowing)
        {
            availableActionBadges.Add(new ReviewActionBadge("DAR", "Baglami Daralt", "context", "narrow", "Baglam ipuclarini birlikte uygula", _lastInvokedReviewActionKey == "narrow"));
        }
        if (hasSuggestedFilter)
        {
            availableActionBadges.Add(new ReviewActionBadge("FLT", "Baglam Filtresi", "filter", "filter", "Baglamdan onerilen hizli filtreyi uygula", _lastInvokedReviewActionKey == "filter"));
        }
        if (hasPeriod)
        {
            availableActionBadges.Add(new ReviewActionBadge("DNM", "Baglam Donemi", "period", "period", "Baglamdaki donemi yil ve ay filtresine uygula", _lastInvokedReviewActionKey == "period"));
        }
        if (hasInvoiceType)
        {
            availableActionBadges.Add(new ReviewActionBadge("TUR", "Baglam Turu", "detail", "type", "Baglamdaki fatura turunu uygula", _lastInvokedReviewActionKey == "type"));
        }
        if (hasInvoiceNumber)
        {
            availableActionBadges.Add(new ReviewActionBadge("NO", "Baglam No", "detail", "invoice_no", "Baglamdaki fatura numarasini ara", _lastInvokedReviewActionKey == "invoice_no"));
        }

        if (InvoiceReviewContextBorder is not null)
        {
            InvoiceReviewContextBorder.Visibility = hasContext ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        if (InvoiceReviewContextText is not null)
        {
            InvoiceReviewContextText.Text = hasContext ? contextLabel : string.Empty;
        }

        if (InvoiceReviewActionBadges is not null)
        {
            InvoiceReviewActionBadges.ItemsSource = hasContext
                ? availableActionBadges
                : Array.Empty<ReviewActionBadge>();
            InvoiceReviewActionBadges.Visibility = hasContext
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        if (InvoiceReviewContextChips is not null)
        {
            InvoiceReviewContextChips.ItemsSource = hasContext
                ? InvoiceReviewContextFormatter.BuildChips(contextLabel)
                : Array.Empty<InvoiceReviewContextFormatter.ContextChip>();
        }

        if (CopyInvoiceReviewContextButton is not null)
        {
            CopyInvoiceReviewContextButton.IsEnabled = !string.IsNullOrWhiteSpace(_invoiceReviewContextLabel);
        }

        if (ApplyInvoiceReviewContextFilterButton is not null)
        {
            ApplyInvoiceReviewContextFilterButton.IsEnabled = hasSuggestedFilter;
        }

        if (ApplyInvoiceReviewContextNarrowButton is not null)
        {
            ApplyInvoiceReviewContextNarrowButton.IsEnabled = hasAnyNarrowing;
        }

        if (FocusInvoiceFromReviewContextButton is not null)
        {
            FocusInvoiceFromReviewContextButton.IsEnabled = hasSuggestedFilter || hasPreferredInvoice || hasPeriod || hasInvoiceType || hasInvoiceNumber;
        }

        if (ApplyInvoiceReviewContextPeriodButton is not null)
        {
            ApplyInvoiceReviewContextPeriodButton.IsEnabled = hasPeriod;
        }

        if (ApplyInvoiceReviewContextTypeButton is not null)
        {
            ApplyInvoiceReviewContextTypeButton.IsEnabled = hasInvoiceType;
        }

        if (ApplyInvoiceReviewContextInvoiceNoButton is not null)
        {
            ApplyInvoiceReviewContextInvoiceNoButton.IsEnabled = hasInvoiceNumber;
        }
    }

    private void TrySaveInvoiceReviewPreferences()
    {
        if (string.IsNullOrWhiteSpace(_rootDirectory))
        {
            return;
        }

        try
        {
            _invoiceReviewPreferences.Save(_rootDirectory);
        }
        catch
        {
            // Tercih kaydi basarisiz olsa da inceleme ekrani calismaya devam etmeli.
        }
    }

    private void StartInvoiceReviewMode(string reviewModeLabel, Action applyModeFilter, string successMessage, long? preferredInvoiceId = null, string? contextLabel = null)
    {
        ResetQuickFilters();
        applyModeFilter();
        _invoiceReviewModeLabel = reviewModeLabel;
        _invoiceReviewContextLabel = contextLabel;
        _invoiceReviewPreferredInvoiceId = preferredInvoiceId;
        ApplyFiltersToGrid(selectedId: preferredInvoiceId, selectFirstIfAvailable: true);
        UpdateInvoiceReviewNavigationControls();
        SetInvoiceStatus(successMessage, isError: false);
    }

    private bool TryOpenSelectedInvoicePdf(out string actionMessage)
    {
        actionMessage = string.Empty;

        if (_invoiceRepository is null || _selectedInvoice is null)
        {
            return false;
        }

        try
        {
            var pdfPath = _invoiceRepository.GetPdfAbsolutePath(_selectedInvoice);
            if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            {
                SetInvoiceStatus("PDF dosyasi bulunamadi.", isError: true);
                UpdatePdfControls(_selectedInvoice);
                return false;
            }

            Process.Start(new ProcessStartInfo(pdfPath)
            {
                UseShellExecute = true,
            });

            actionMessage = "Fatura PDF dosyasi acildi.";
            return true;
        }
        catch (Exception exception) when (exception is InvalidOperationException or IOException or UnauthorizedAccessException or Win32Exception)
        {
            SetInvoiceStatus(exception.Message, isError: true);
            return false;
        }
    }

    private bool TryRevealSelectedInvoicePdfFolder(out string actionMessage)
    {
        actionMessage = string.Empty;

        if (_invoiceRepository is null || _selectedInvoice is null)
        {
            return false;
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
                actionMessage = "Fatura PDF dosyasi klasorde gosterildi.";
                return true;
            }

            var pdfDirectory = _invoiceRepository.GetPdfDirectoryAbsolutePath(_selectedInvoice);
            Directory.CreateDirectory(pdfDirectory);
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"\"{pdfDirectory}\"",
                UseShellExecute = true,
            });
            actionMessage = "Fatura icin beklenen PDF klasoru acildi.";
            return true;
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidOperationException or Win32Exception)
        {
            SetInvoiceStatus(exception.Message, isError: true);
            return false;
        }
    }

    private void CompleteInvoiceReviewAction(string actionMessage)
    {
        var visibleInvoices = GetVisibleInvoices();
        if (_selectedInvoice is null || visibleInvoices.Count == 0)
        {
            SetInvoiceStatus(actionMessage, isError: false);
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        var currentIndex = visibleInvoices.FindIndex(item => item.Id == _selectedInvoice.Id);
        if (!InvoiceReviewNavigator.TryMove(currentIndex, visibleInvoices.Count, 1, out var targetIndex))
        {
            SetInvoiceStatus($"{actionMessage} Tur sonuna ulasildi.", isError: false);
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        var targetInvoice = visibleInvoices[targetIndex];
        InvoiceGrid.SelectedItem = targetInvoice;
        InvoiceGrid.ScrollIntoView(targetInvoice);
        ApplySelectedInvoice(targetInvoice);
        SetInvoiceStatus($"{actionMessage} Sonraki kayda gecildi ({targetIndex + 1}/{visibleInvoices.Count}).", isError: false);
    }

    private void UpdateInvoiceReviewNoteControls(Invoice? invoice)
    {
        if (invoice is null)
        {
            InvoiceReviewNoteInput.Text = string.Empty;
            InvoiceReviewNoteInput.IsEnabled = false;
            SaveInvoiceReviewNoteButton.IsEnabled = false;
            MarkReviewedAndNextButton.IsEnabled = false;
            ClearInvoiceReviewNoteButton.IsEnabled = false;
            InvoiceReviewedAtText.Text = "Bu kayıt için inceleme işareti yok.";
            return;
        }

        InvoiceReviewNoteInput.Text = invoice.ReviewNote;
        InvoiceReviewNoteInput.IsEnabled = true;
        SaveInvoiceReviewNoteButton.IsEnabled = true;
        MarkReviewedAndNextButton.IsEnabled = true;
        ClearInvoiceReviewNoteButton.IsEnabled = !string.IsNullOrWhiteSpace(invoice.ReviewNote) || invoice.ReviewedAt is not null;
        InvoiceReviewedAtText.Text = invoice.ReviewedAt is null
            ? "Bu kayıt için inceleme işareti yok."
            : $"Son inceleme: {invoice.ReviewedAt.Value.ToLocalTime():dd.MM.yyyy HH:mm}";
    }

    private void MarkSelectedInvoiceReviewedAndMoveNext()
    {
        if (_invoiceRepository is null || _selectedInvoice is null)
        {
            SetInvoiceStatus("Bakıldı işareti için önce bir fatura seçin.", isError: true);
            return;
        }

        try
        {
            var visibleInvoices = GetVisibleInvoices();
            var currentIndex = visibleInvoices.FindIndex(item => item.Id == _selectedInvoice.Id);
            long? nextInvoiceId = null;
            var nextPosition = 0;
            var totalVisibleCount = visibleInvoices.Count;

            if (InvoiceReviewNavigator.TryMove(currentIndex, totalVisibleCount, 1, out var nextIndex))
            {
                nextInvoiceId = visibleInvoices[nextIndex].Id;
                nextPosition = nextIndex + 1;
            }

            var updated = _invoiceRepository.UpdateReviewStatus(_selectedInvoice.Id, InvoiceReviewNoteInput.Text, DateTimeOffset.Now);
            RefreshInvoices(nextInvoiceId ?? updated.Id);
            InvoicesChanged?.Invoke(this, EventArgs.Empty);

            if (nextInvoiceId is null)
            {
                SetInvoiceStatus("Bakıldı işareti kaydedildi. Tur sonuna ulaşıldı.", isError: false);
                return;
            }

            SetInvoiceStatus($"Bakıldı işareti kaydedildi. Sonraki kayda geçildi ({nextPosition}/{totalVisibleCount}).", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
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
        if (TryOpenSelectedInvoicePdf(out var actionMessage))
        {
            SetInvoiceStatus(actionMessage, isError: false);
        }
    }

    private void RevealInvoicePdfFolderButton_Click(object sender, RoutedEventArgs e)
    {
        if (TryRevealSelectedInvoicePdfFolder(out var actionMessage))
        {
            SetInvoiceStatus(actionMessage, isError: false);
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
        UpdateInvoiceReviewNoteControls(null);
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
        UpdateInvoiceReviewNoteControls(null);
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
            OpenInvoicePdfAndNextButton.IsEnabled = false;
            RevealInvoicePdfFolderAndNextButton.IsEnabled = false;
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        if (invoice is null)
        {
            SetPdfInfo("PDF eklemek için dosya seçin; kayıt sırasında faturaya bağlanır.", isError: false);
            OpenInvoicePdfButton.IsEnabled = false;
            RevealInvoicePdfFolderButton.IsEnabled = false;
            OpenInvoicePdfAndNextButton.IsEnabled = false;
            RevealInvoicePdfFolderAndNextButton.IsEnabled = false;
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        if (!invoice.HasPdf)
        {
            SetPdfInfo("Bu faturaya PDF evrak eklenmemiş.", isError: false);
            OpenInvoicePdfButton.IsEnabled = false;
            RevealInvoicePdfFolderButton.IsEnabled = true;
            OpenInvoicePdfAndNextButton.IsEnabled = false;
            RevealInvoicePdfFolderAndNextButton.IsEnabled = true;
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        if (_invoiceRepository?.PdfFileExists(invoice) == true)
        {
            SetPdfInfo($"PDF kayıtlı: {invoice.PdfOriginalFileName}", isError: false);
            OpenInvoicePdfButton.IsEnabled = true;
            RevealInvoicePdfFolderButton.IsEnabled = true;
            OpenInvoicePdfAndNextButton.IsEnabled = true;
            RevealInvoicePdfFolderAndNextButton.IsEnabled = true;
            UpdateInvoiceReviewNavigationControls();
            return;
        }

        SetPdfInfo($"PDF kaydı var ancak dosya bulunamadı: {invoice.PdfFilePath}", isError: true);
        OpenInvoicePdfButton.IsEnabled = false;
        RevealInvoicePdfFolderButton.IsEnabled = true;
        OpenInvoicePdfAndNextButton.IsEnabled = false;
        RevealInvoicePdfFolderAndNextButton.IsEnabled = true;
        UpdateInvoiceReviewNavigationControls();
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

    private sealed record ReviewStatusFilterOption(string Label, InvoiceReviewStatusFilter Value);

    private sealed record ReviewActionBadge(string Prefix, string Text, string Kind, string ActionKey, string ToolTip, bool IsSelected);
}






