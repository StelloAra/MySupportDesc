using Microsoft.UI.Xaml;
using SupportDesk.WinUI.Pages;
using SupportDesk.Application.Interfaces;

namespace SupportDesk.WinUI;


public sealed partial class MainWindow : Window
{
    private readonly ITicketService _ticketService;
    private readonly ICustomerService _customerService;
    public MainWindow(ITicketService ticketService, ICustomerService customerService)
    {
        InitializeComponent();
        _ticketService = ticketService;
        _customerService = customerService;
        ContentFrame.Navigate(typeof(DashboardPage));
    }

    private void DashboardButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(DashboardPage));
    }

    private void TicketsButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        var page = new TicketsPage();

        page.SetServices(
            _ticketService,
            _customerService);

        ContentFrame.Navigate(page.GetType());
    }

    private void NewTicketButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        ContentFrame.Navigate(
           typeof(NewTicketPage),
           new NewTicketNavigationParameter
           {
               TicketService = _ticketService,
               CustomerService = _customerService
           });
    }

    private void CustomersButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        var page = new CustomersPage();

        page.SetCustomerService(_customerService);

        ContentFrame.Navigate(page.GetType());
    }
}
