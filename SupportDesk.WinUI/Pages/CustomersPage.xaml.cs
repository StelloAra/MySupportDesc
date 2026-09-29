using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Application.Interfaces;
using System.Threading.Tasks;

namespace SupportDesk.WinUI.Pages;

public sealed partial class CustomersPage : Page
{
    private ICustomerService? _customerService;

    public CustomersPage()
    {
        InitializeComponent();
    }

    public void SetCustomerService(ICustomerService customerService)
    {
        _customerService = customerService;
        _ = LoadCustomersAsync();
    }

    private async Task LoadCustomersAsync()
    {
        var customers = await _customerService!.GetAllAsync();

        CustomersListView.ItemsSource = customers;
    }
}