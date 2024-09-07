using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.DataAccess.Entities.ServMon;

namespace UniversityIT.DataAccess.Repositories.ServMon
{
    public class ServEventsRepository : IServEventsRepository
    {
        private readonly UniversityITDbContext _context;

        public ServEventsRepository(UniversityITDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServEvent>> Get()
        {
            var servEventEntities = await _context.ServEvents
                .AsNoTracking()
                .Include(se => se.Server)
                .ToListAsync();

            var servEvents = servEventEntities
                .Select(se => ServEvent.Create(
                    se.Id,
                    se.HappenedAt,
                    (NetStatus)se.ServStatusId,
                    se.Server != null ? Server.Create(
                        se.Server.Id,
                        se.Server.Name,
                        se.Server.IpAddress,
                        se.Server.ShortDescription,
                        se.Server.Description,
                        se.Server.Activity,
                        (NetStatus)se.Server.CurrentStatusId).Value : null)
                .Value)
                .ToList();

            return servEvents;
        }

        public async Task<Guid> Create(Guid servId, NetStatus servStatus)
        {
            var servEventEntity = new ServEventEntity
            {
                Id = Guid.NewGuid(),
                HappenedAt = DateTime.Now,
                ServStatusId = (int)servStatus,
                ServerId = servId
            };

            await _context.ServEvents.AddAsync(servEventEntity);
            await _context.SaveChangesAsync();

            return servEventEntity.Id;
        }
    }
}