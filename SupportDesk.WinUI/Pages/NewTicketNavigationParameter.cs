using SupportDesk.Application.Interfaces;

namespace SupportDesk.WinUI.Pages;

public class NewTicketNavigationParameter
{
    public ITicketService TicketService { get; set; } = null!;
    public ICustomerService CustomerService { get; set; } = null!;
}