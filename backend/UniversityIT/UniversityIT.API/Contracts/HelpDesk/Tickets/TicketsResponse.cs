namespace UniversityIT.API.Contracts.HelpDesk.Tickets
{
    public record TicketsResponse(
        Guid id,
        string Name,
        string Description,
        string Place,
        DateTime CreatedAt,
        bool NotificationsSent,
        bool IsCompleted,
        Guid AuthorId,
        string? Author);
}