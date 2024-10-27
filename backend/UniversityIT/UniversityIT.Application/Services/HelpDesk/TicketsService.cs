using UniversityIT.Core.Abstractions.HelpDesk.Tickets;
using UniversityIT.Core.Models.HelpDesk;

namespace UniversityIT.Application.Services.HelpDesk
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsService(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<Guid> CreateTicket(Ticket ticket)
        {
            return await _ticketsRepository.Create(ticket);
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _ticketsRepository.Get();
        }

        public async Task<Ticket> GetTicketById(Guid id)
        {
            return await _ticketsRepository.GetById(id);
        }

        public async Task<List<Ticket>> GetTicketsByUserId(Guid UserId)
        {
            return await _ticketsRepository.GetByUserId(UserId);
        }

        public async Task<Guid> UpdateTicket(Guid id, string name, string description, string place, bool isCompleted)
        {
            return await _ticketsRepository.Update(id, name, description, place, isCompleted);
        }

        public async Task<Guid> DeleteTicket(Guid id)
        {
            return await _ticketsRepository.Delete(id);
        }
    }
}