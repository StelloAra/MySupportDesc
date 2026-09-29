using SupportDesk.Domain.Tickets;

namespace SupportDesk.Infrastructure.Data;

public class TicketData
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid CustomerId { get; set; }

    public TicketPriority Priority { get; set; }

    public TicketStatus Status { get; set; }

    public string? Technician { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<CommentData> Comments { get; set; } = new();
}
