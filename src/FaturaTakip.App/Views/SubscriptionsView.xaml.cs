using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FaturaTakip.App.Data.InvoiceTypes;
using FaturaTakip.App.Data.Subscriptions;

namespace FaturaTakip.App.Views;

public partial class SubscriptionsView : UserControl
{
    private InvoiceTypeRepository? _invoiceTypeRepository;
    private SubscriptionRepository? _subscriptionRepository;
    private IReadOnlyList<InvoiceType> _invoiceTypes = Array.Empty<InvoiceType>();
    private IReadOnlyList<Subscription> _subscriptions = Array.Empty<Subscription>();
    private Subscription? _selectedSubscription;
    private bool _isInitialized;
    private bool _isEditingExisting;

    public event EventHandler? SubscriptionsChanged;

    public SubscriptionsView()
    {
        InitializeComponent();
    }

    public void Initialize(string databasePath)
    {
        _invoiceTypeRepository = new InvoiceTypeRepository(databasePath);
        _subscriptionRepository = new SubscriptionRepository(databasePath);
        _isInitialized = true;

        SubscriptionStateFilterInput.ItemsSource = new[]
        {
            new StateFilterOption("Tüm Durumlar", null),
            new StateFilterOption("Aktif", true),
            new StateFilterOption("Pasif", false),
        };
        SubscriptionStateFilterInput.SelectedIndex = 0;

        RefreshInvoiceTypeLists();
        RefreshSubscriptions();
    }

    public void Refresh()
    {
        if (!_isInitialized)
        {
            return;
        }

        RefreshInvoiceTypeLists();
        RefreshSubscriptions(_selectedSubscription?.Id);
    }

    private void RefreshInvoiceTypeLists()
    {
        if (_invoiceTypeRepository is null)
        {
            return;
        }

        _invoiceTypes = _invoiceTypeRepository.GetAll();
        SubscriptionInvoiceTypeInput.ItemsSource = _invoiceTypes;

        var typeFilters = new List<InvoiceTypeFilterOption>
        {
            new("Tüm Türler", null),
        };
        typeFilters.AddRange(_invoiceTypes.Select(item => new InvoiceTypeFilterOption(item.Name, item.Id)));

        SubscriptionTypeFilterInput.ItemsSource = typeFilters;
        SubscriptionTypeFilterInput.SelectedIndex = 0;
    }

    private void RefreshSubscriptions(long? selectedId = null)
    {
        if (_subscriptionRepository is null)
        {
            return;
        }

        _subscriptions = _subscriptionRepository.GetAll();
        var activeCount = _subscriptions.Count(item => item.IsActive);
        var passiveCount = _subscriptions.Count - activeCount;

        SubscriptionCountText.Text = _subscriptions.Count.ToString(CultureInfo.InvariantCulture);
        ActiveSubscriptionCountText.Text = activeCount.ToString(CultureInfo.InvariantCulture);
        PassiveSubscriptionCountText.Text = passiveCount.ToString(CultureInfo.InvariantCulture);

        var filtered = ApplyFilters(_subscriptions).ToList();
        SubscriptionGrid.ItemsSource = filtered;

        var selected = selectedId is null
            ? null
            : filtered.FirstOrDefault(item => item.Id == selectedId.Value);

        if (selected is null)
        {
            ClearSubscriptionForm();
            return;
        }

        SubscriptionGrid.SelectedItem = selected;
        ApplySelectedSubscription(selected);
    }

    private IEnumerable<Subscription> ApplyFilters(IEnumerable<Subscription> source)
    {
        var query = source;

        if (SubscriptionTypeFilterInput.SelectedItem is InvoiceTypeFilterOption { InvoiceTypeId: { } invoiceTypeId })
        {
            query = query.Where(item => item.InvoiceTypeId == invoiceTypeId);
        }

        if (SubscriptionStateFilterInput.SelectedItem is StateFilterOption { IsActive: { } isActive })
        {
            query = query.Where(item => item.IsActive == isActive);
        }

        var search = SubscriptionSearchInput.Text.Trim();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(item =>
                Contains(item.InvoiceTypeName, search) ||
                Contains(item.SubscriptionName, search) ||
                Contains(item.InstitutionName, search) ||
                Contains(item.SubscriberNo, search) ||
                Contains(item.InstallationNo, search) ||
                Contains(item.MeterNo, search) ||
                Contains(item.ProviderCompany, search) ||
                Contains(item.UnitName, search));
        }

        return query;
    }

    private static bool Contains(string value, string search)
    {
        return value.Contains(search, StringComparison.CurrentCultureIgnoreCase);
    }

    private void SubscriptionFilter_Changed(object sender, RoutedEventArgs e)
    {
        if (!_isInitialized)
        {
            return;
        }

        RefreshSubscriptions(_selectedSubscription?.Id);
    }

    private void SubscriptionGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SubscriptionGrid.SelectedItem is Subscription subscription)
        {
            ApplySelectedSubscription(subscription);
        }
    }

    private void ApplySelectedSubscription(Subscription subscription)
    {
        _selectedSubscription = subscription;
        _isEditingExisting = true;

        SubscriptionFormTitleText.Text = "Aboneliği Düzenle";
        SubscriptionInvoiceTypeInput.SelectedValue = subscription.InvoiceTypeId;
        SubscriptionNameInput.Text = subscription.SubscriptionName;
        InstitutionNameInput.Text = subscription.InstitutionName;
        SubscriberNoInput.Text = subscription.SubscriberNo;
        InstallationNoInput.Text = subscription.InstallationNo;
        MeterNoInput.Text = subscription.MeterNo;
        ProviderCompanyInput.Text = subscription.ProviderCompany;
        UnitNameInput.Text = subscription.UnitName;
        SubscriptionUsageUnitInput.Text = subscription.DefaultUsageUnit;
        StartDateInput.SelectedDate = subscription.StartDate;
        EndDateInput.SelectedDate = subscription.EndDate;
        SubscriptionIsActiveInput.IsChecked = subscription.IsActive;
        ServiceAddressInput.Text = subscription.ServiceAddress;
        SubscriptionDescriptionInput.Text = subscription.Description;

        ToggleSubscriptionButton.IsEnabled = true;
        ToggleSubscriptionButton.Content = subscription.IsActive ? "Pasife Al" : "Aktif Yap";
        SetSubscriptionStatus($"Seçili kayıt: {subscription.SubscriptionName}", isError: false);
    }

    private void NewSubscriptionButton_Click(object sender, RoutedEventArgs e)
    {
        ClearSubscriptionForm();
        SubscriptionNameInput.Focus();
    }

    private void SaveSubscriptionButton_Click(object sender, RoutedEventArgs e)
    {
        if (_subscriptionRepository is null)
        {
            return;
        }

        try
        {
            var input = ReadSubscriptionInput();
            var saved = _selectedSubscription is null
                ? _subscriptionRepository.Add(input)
                : _subscriptionRepository.Update(_selectedSubscription.Id, input);

            RefreshSubscriptions(saved.Id);
            SubscriptionsChanged?.Invoke(this, EventArgs.Empty);
            SetSubscriptionStatus("Abonelik kaydedildi.", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
        {
            SetSubscriptionStatus(exception.Message, isError: true);
        }
    }

    private void ToggleSubscriptionButton_Click(object sender, RoutedEventArgs e)
    {
        if (_subscriptionRepository is null)
        {
            return;
        }

        if (_selectedSubscription is null)
        {
            SetSubscriptionStatus("Önce bir abonelik seçin.", isError: true);
            return;
        }

        try
        {
            var nextState = !_selectedSubscription.IsActive;
            _subscriptionRepository.SetActive(_selectedSubscription.Id, nextState);
            RefreshSubscriptions(_selectedSubscription.Id);
            SubscriptionsChanged?.Invoke(this, EventArgs.Empty);
            SetSubscriptionStatus(nextState ? "Abonelik aktif yapıldı." : "Abonelik pasife alındı.", isError: false);
        }
        catch (Exception exception) when (exception is InvalidOperationException or Microsoft.Data.Sqlite.SqliteException)
        {
            SetSubscriptionStatus(exception.Message, isError: true);
        }
    }

    private void SubscriptionInvoiceTypeInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_isInitialized || _isEditingExisting)
        {
            return;
        }

        if (SubscriptionInvoiceTypeInput.SelectedItem is InvoiceType invoiceType &&
            string.IsNullOrWhiteSpace(SubscriptionUsageUnitInput.Text))
        {
            SubscriptionUsageUnitInput.Text = invoiceType.DefaultUsageUnit;
        }
    }

    private SubscriptionInput ReadSubscriptionInput()
    {
        var invoiceTypeId = SubscriptionInvoiceTypeInput.SelectedValue is long selectedId
            ? selectedId
            : 0;

        return new SubscriptionInput(
            invoiceTypeId,
            SubscriptionNameInput.Text,
            InstitutionNameInput.Text,
            SubscriberNoInput.Text,
            InstallationNoInput.Text,
            MeterNoInput.Text,
            ProviderCompanyInput.Text,
            ServiceAddressInput.Text,
            UnitNameInput.Text,
            SubscriptionUsageUnitInput.Text,
            SubscriptionIsActiveInput.IsChecked == true,
            StartDateInput.SelectedDate,
            EndDateInput.SelectedDate,
            SubscriptionDescriptionInput.Text);
    }

    private void ClearSubscriptionForm()
    {
        _selectedSubscription = null;
        _isEditingExisting = false;
        SubscriptionGrid.SelectedItem = null;

        SubscriptionFormTitleText.Text = "Yeni Abonelik";
        SubscriptionInvoiceTypeInput.SelectedItem = _invoiceTypes.FirstOrDefault(item => item.IsActive) ?? _invoiceTypes.FirstOrDefault();
        SubscriptionNameInput.Text = string.Empty;
        InstitutionNameInput.Text = string.Empty;
        SubscriberNoInput.Text = string.Empty;
        InstallationNoInput.Text = string.Empty;
        MeterNoInput.Text = string.Empty;
        ProviderCompanyInput.Text = string.Empty;
        UnitNameInput.Text = string.Empty;
        SubscriptionUsageUnitInput.Text = (SubscriptionInvoiceTypeInput.SelectedItem as InvoiceType)?.DefaultUsageUnit ?? string.Empty;
        StartDateInput.SelectedDate = null;
        EndDateInput.SelectedDate = null;
        SubscriptionIsActiveInput.IsChecked = true;
        ServiceAddressInput.Text = string.Empty;
        SubscriptionDescriptionInput.Text = string.Empty;
        ToggleSubscriptionButton.IsEnabled = false;
        ToggleSubscriptionButton.Content = "Pasife Al";
        SetSubscriptionStatus("Yeni kayıt için alanları doldurun.", isError: false);
    }

    private void SetSubscriptionStatus(string message, bool isError)
    {
        SubscriptionStatusText.Text = message;
        SubscriptionStatusText.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(185, 28, 28))
            : new SolidColorBrush(Color.FromRgb(95, 107, 122));
    }

    private sealed record InvoiceTypeFilterOption(string Label, long? InvoiceTypeId);

    private sealed record StateFilterOption(string Label, bool? IsActive);
}
