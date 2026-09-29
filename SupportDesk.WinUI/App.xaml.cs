using Microsoft.UI.Xaml;
using SupportDesk.Infrastructure.Data;
using SupportDesk.Application.Interfaces;
using SupportDesk.Infrastructure.Repositories;
using SupportDesk.Application.Services;


namespace SupportDesk.WinUI;
public partial class App : Microsoft.UI.Xaml.Application
{
    private Window? _window;
    private readonly JsonFileHandler _fileHandler;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketService _ticketService;
    private readonly ICustomerService _customerService;

    public App()
    {
        InitializeComponent();
        _fileHandler = new JsonFileHandler();

        _customerRepository = new JsonCustomerRepository(
        _fileHandler);

        _customerService = new CustomerService(
        _customerRepository);

        _ticketRepository = new JsonTicketRepository(
        _fileHandler,
        _customerRepository);

        _ticketService = new TicketService(
        _ticketRepository,
        _customerRepository);
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        _window = new MainWindow(_ticketService, _customerService);
        _window.Activate();
    }
}
