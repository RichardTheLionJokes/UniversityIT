using UniversityIT.Core.Models.HelpDesk;

namespace UniversityIT.Core.Abstractions.HelpDesk.Tickets
{
    public interface ITicketsRepository
    {
        Task<Guid> Create(Ticket ticket);
        Task<List<Ticket>> Get();
        Task<Ticket> GetById(Guid id);
        Task<List<Ticket>> GetByUserId(Guid userId);
        Task<Guid> Update(Guid id, string name, string description, string place, bool isCompleted);
        Task<Guid> Delete(Guid id);
        Task SetNotification(Guid id);
    }
}