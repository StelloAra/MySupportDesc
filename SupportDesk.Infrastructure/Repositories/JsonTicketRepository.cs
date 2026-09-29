using SupportDesk.Application.Interfaces;
using SupportDesk.Domain;
using SupportDesk.Domain.Tickets;
using SupportDesk.Infrastructure.Data;

namespace SupportDesk.Infrastructure.Repositories;

public class JsonTicketRepository : ITicketRepository
{
    private readonly JsonFileHandler _fileHandler;
    private readonly ICustomerRepository _customerRepository;

    public async Task<List<SupportTicket>> GetAllAsync()
    {
        var data = await _fileHandler.ReadAsync<TicketData>(
            DataPaths.TicketsFile);

        var tickets = new List<SupportTicket>();

        foreach (var item in data)
        {
            var customer = await _customerRepository.GetByIdAsync(
                item.CustomerId);

            if (customer == null)
            {
                throw new InvalidDataException(
                    $"Customer was not found for ticket: {item.Id}");
            }

            var comments = item.Comments
                .Select(c => Comment.Rehydrate(
                    c.Text,
                    c.CreatedAt))
                .ToList();

            var ticket = SupportTicket.Rehydrate(
                item.Id,
                item.Title,
                item.Description,
                customer,
                item.Priority,
                item.Status,
                item.Technician,
                item.CreatedAt,
                comments);

            tickets.Add(ticket);
        }

        return tickets;
    }

    public async Task<SupportTicket?> GetByIdAsync(Guid id)
    {
        var tickets = await GetAllAsync();

        return tickets.FirstOrDefault(t => t.Id == id);
    }

    public async Task AddAsync(SupportTicket ticket)
    {
        var tickets = await GetAllAsync();

        tickets.Add(ticket);

        var data = tickets.Select(t => new TicketData
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CustomerId = t.Customer.Id,
            Priority = t.Priority,
            Status = t.Status,
            Technician = t.Technician,
            CreatedAt = t.CreatedAt,
            Comments = t.GetComments()
                .Select(c => new CommentData
                {
                    Text = c.Text,
                    CreatedAt = c.CreatedAt
                })
                .ToList()
        }).ToList();

        await _fileHandler.WriteAsync(
            DataPaths.TicketsFile,
            data);
    }

    public async Task UpdateAsync(SupportTicket ticket)
    {
        var tickets = await GetAllAsync();

        var index = tickets.FindIndex(t => t.Id == ticket.Id);

        if (index == -1)
        {
            throw new ArgumentException("Ticket was not found.");
        }

        tickets[index] = ticket;

        var data = tickets.Select(t => new TicketData
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CustomerId = t.Customer.Id,
            Priority = t.Priority,
            Status = t.Status,
            Technician = t.Technician,
            CreatedAt = t.CreatedAt,
            Comments = t.GetComments()
                .Select(c => new CommentData
                {
                    Text = c.Text,
                    CreatedAt = c.CreatedAt
                })
                .ToList()
        }).ToList();

        await _fileHandler.WriteAsync(
            DataPaths.TicketsFile,
            data);
    }

    public JsonTicketRepository(
        JsonFileHandler fileHandler,
        ICustomerRepository customerRepository)
    {
        _fileHandler = fileHandler;
        _customerRepository = customerRepository;
    }
}