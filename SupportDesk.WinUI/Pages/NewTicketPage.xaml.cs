using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Application.Interfaces;
using System.Threading.Tasks;


namespace SupportDesk.WinUI.Pages;

public sealed partial class NewTicketPage : Page
{
    private ITicketService? _ticketService;
    private ICustomerService? _customerService;
    public NewTicketPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(
        Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        var parameter = (NewTicketNavigationParameter)e.Parameter;

        _ticketService = parameter.TicketService;
        _customerService = parameter.CustomerService;

        _ = LoadCustomersAsync();
    }

    private async Task LoadCustomersAsync()
    {
        var customers = await _customerService!.GetAllAsync();

        CustomerComboBox.ItemsSource = customers;
    }

    private void CancelButton_Click(
    object sender,
    Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.GoBack();
    }

    private void CreateTicketButton_Click(
    object sender,
    RoutedEventArgs e)
    {
    }
}
