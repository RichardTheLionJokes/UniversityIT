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

        public async Task<Guid> Create(ServEvent servEvent)
        {
            var servEventEntity = new ServEventEntity
            {
                Id = servEvent.Id,
                HappenedAt = servEvent.HappenedAt,
                ServStatusId = (int)servEvent.ServStatus,
                ServerId = servEvent.ServerId
            };

            await _context.ServEvents.AddAsync(servEventEntity);
            await _context.SaveChangesAsync();

            return servEventEntity.Id;
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
                    se.ServerId,
                    se.Server?.Name,
                    se.Server?.IpAddress)
                .Value)
                .ToList();

            return servEvents;
        }
    }
}