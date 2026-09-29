using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SupportDesk.Application.Interfaces;
using SupportDesk.WinUI;

namespace SupportDesk.WinUI.Pages;

public sealed partial class TicketsPage : Page
{
    private ITicketService? _ticketService;
    private ICustomerService? _customerService;
    public TicketsPage()
    {
        InitializeComponent();
    }

    private void NewTicketButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        Frame.Navigate(
            typeof(NewTicketPage),
            new NewTicketNavigationParameter
            {
                TicketService = _ticketService!,
                CustomerService = _customerService!
            });
    }

    public void SetServices(
    ITicketService ticketService,
    ICustomerService customerService)
    {
        _ticketService = ticketService;
        _customerService = customerService;
    }
}
