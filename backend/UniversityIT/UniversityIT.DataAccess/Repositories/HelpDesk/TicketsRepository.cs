using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Abstractions.HelpDesk.Tickets;
using UniversityIT.Core.Models.HelpDesk;

namespace UniversityIT.DataAccess.Repositories.HelpDesk
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly UniversityITDbContext _context;

        public TicketsRepository(UniversityITDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Ticket ticket)
        {
            var ticketEntity = DataBaseMappings.TicketToEntity(ticket);

            await _context.Tickets.AddAsync(ticketEntity);
            await _context.SaveChangesAsync();

            return ticketEntity.Id;
        }

        public async Task<List<Ticket>> Get()
        {
            var ticketEntities = await _context.Tickets
                .AsNoTracking()
                .Include(t => t.User)
                .ToListAsync();

            var tickets = ticketEntities
                .Select(t => DataBaseMappings.EntityToTicket(t))
                .ToList();

            return tickets;
        }

        public async Task<Ticket> GetById(Guid id)
        {
            var ticketEntity = await _context.Tickets
                .AsNoTracking()
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception();

            return DataBaseMappings.EntityToTicket(ticketEntity);
        }

        public async Task<List<Ticket>> GetByUserId(Guid userId)
        {
            var ticketEntities = await _context.Tickets
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .ToListAsync();

            var tickets = ticketEntities
                .Select(t => DataBaseMappings.EntityToTicket(t))
                .ToList();

            return tickets;
        }

        public async Task<Guid> Update(Guid id, string name, string description, string place, bool isCompleted)
        {
            await _context.Tickets
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(t => t.Name, t => name)
                    .SetProperty(t => t.Description, t => description)
                    .SetProperty(t => t.Place, t => place)
                    .SetProperty(t => t.IsCompleted, t => isCompleted));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Tickets
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task SetNotification(Guid id)
        {
            await _context.Tickets
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(t => t.NotificationsSent, t => true));
        }
    }
}