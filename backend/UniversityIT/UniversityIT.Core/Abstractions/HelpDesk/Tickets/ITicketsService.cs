using UniversityIT.Core.Models.HelpDesk;

namespace UniversityIT.Core.Abstractions.HelpDesk.Tickets
{
    public interface ITicketsService
    {
        Task<Guid> CreateTicket(Ticket ticket);
        Task<List<Ticket>> GetAllTickets();
        Task<Ticket> GetTicketById(Guid id);
        Task<List<Ticket>> GetTicketsByUserId(Guid UserId);
        Task<Guid> UpdateTicket(Guid id, string name, string description, string place, bool isCompleted);
        Task<Guid> DeleteTicket(Guid id);
        Task NotifyAboutCreation(Guid id);
    }
}