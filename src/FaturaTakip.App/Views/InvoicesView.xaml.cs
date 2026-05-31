using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.Subscriptions;
using Microsoft.Win32;

namespace FaturaTakip.App.Views;

public partial class InvoicesView : UserControl
{
    private static readonly CultureInfo TurkishCulture = CultureInfo.GetCultureInfo("tr-TR");
    private InvoiceRepository? _invoiceRepository;
    private SubscriptionRepository? _subscriptionRepository;
    private IReadOnlyList<Invoice> _invoices = Array.Empty<Invoice>();
    private IReadOnlyList<Subscription> _subscriptions = Array.Empty<Subscription>();
    private Invoice? _selectedInvoice;
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

        var filtered = ApplyFilters(_invoices).ToList();
        InvoiceGrid.ItemsSource = filtered;

        var selected = selectedId is null
            ? null
            : filtered.FirstOrDefault(item => item.Id == selectedId.Value);

        if (selected is null)
        {
            ClearInvoiceForm();
            return;
        }

        InvoiceGrid.SelectedItem = selected;
        ApplySelectedInvoice(selected);
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

        var selectedId = _selectedInvoice?.Id;
        var filtered = ApplyFilters(_invoices).ToList();
        InvoiceGrid.ItemsSource = filtered;

        if (selectedId is null)
        {
            return;
        }

        var selected = filtered.FirstOrDefault(item => item.Id == selectedId.Value);
        if (selected is not null)
        {
            InvoiceGrid.SelectedItem = selected;
        }
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

        var filtered = ApplyFilters(_invoices).ToList();
        InvoiceGrid.ItemsSource = filtered;
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
        SetInvoiceStatus($"Seçili kayıt: {invoice.InvoiceNo}", isError: false);
    }

    private void NewInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        ClearInvoiceForm();
        InvoiceNoInput.Focus();
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
        SetInvoiceStatus("Yeni kayıt için alanları doldurun.", isError: false);
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
            return;
        }

        if (invoice is null)
        {
            SetPdfInfo("PDF eklemek için dosya seçin; kayıt sırasında faturaya bağlanır.", isError: false);
            OpenInvoicePdfButton.IsEnabled = false;
            return;
        }

        if (!invoice.HasPdf)
        {
            SetPdfInfo("Bu faturaya PDF evrak eklenmemiş.", isError: false);
            OpenInvoicePdfButton.IsEnabled = false;
            return;
        }

        if (_invoiceRepository?.PdfFileExists(invoice) == true)
        {
            SetPdfInfo($"PDF kayıtlı: {invoice.PdfOriginalFileName}", isError: false);
            OpenInvoicePdfButton.IsEnabled = true;
            return;
        }

        SetPdfInfo($"PDF kaydı var ancak dosya bulunamadı: {invoice.PdfFilePath}", isError: true);
        OpenInvoicePdfButton.IsEnabled = false;
    }

    private void SetPdfInfo(string message, bool isError)
    {
        InvoicePdfInfoText.Text = message;
        InvoicePdfInfoText.Foreground = isError
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
