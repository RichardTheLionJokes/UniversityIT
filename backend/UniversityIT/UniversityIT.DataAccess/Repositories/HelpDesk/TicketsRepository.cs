using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Abstractions.HelpDesk.Tickets;
using UniversityIT.Core.Models.HelpDesk;
using UniversityIT.DataAccess.Entities.HelpDesk;

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
            var ticketEntity = new TicketEntity
            {
                Id = ticket.Id,
                Name = ticket.Name,
                Description = ticket.Description,
                Place = ticket.Place,
                CreatedAt = ticket.CreatedAt,
                NotificationsSent = ticket.NotificationsSent,
                IsCompleted = ticket.IsCompleted,
                UserId = ticket.AuthorId
            };

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
                .Select(t => Ticket.Create(
                    t.Id,
                    t.Name,
                    t.Description,
                    t.Place,
                    t.CreatedAt,
                    t.NotificationsSent,
                    t.IsCompleted,
                    t.UserId,
                    t.User?.UserName)
                .Value)
                .ToList();

            return tickets;
        }

        public async Task<Ticket> GetById(Guid id)
        {
            var ticketEntity = await _context.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception();

            return Ticket.Create(
                ticketEntity.Id,
                ticketEntity.Name,
                ticketEntity.Description,
                ticketEntity.Place,
                ticketEntity.CreatedAt,
                ticketEntity.NotificationsSent,
                ticketEntity.IsCompleted,
                ticketEntity.UserId,
                ticketEntity.User?.UserName).Value;
        }

        public async Task<List<Ticket>> GetByUserId(Guid userId)
        {
            var ticketEntities = await _context.Tickets
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .ToListAsync();

            var tickets = ticketEntities
                .Select(t => Ticket.Create(
                    t.Id,
                    t.Name,
                    t.Description,
                    t.Place,
                    t.CreatedAt,
                    t.NotificationsSent,
                    t.IsCompleted,
                    t.UserId,
                    t.User?.UserName)
                .Value)
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
    }
}