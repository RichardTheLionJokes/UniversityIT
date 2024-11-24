using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Models.ServMon;

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
            var servEventEntity = DataBaseMappings.ServEventToEntity(servEvent);

            await _context.ServEvents.AddAsync(servEventEntity);
            await _context.SaveChangesAsync();

            return servEventEntity.Id;
        }

        public async Task<List<ServEvent>> Get()
        {
            var servEventEntities = await _context.ServEvents
                .AsNoTracking()
                .Include(se => se.Server)
                .OrderByDescending(se => se.HappenedAt)
                .ToListAsync();

            var servEvents = servEventEntities
                .Select(se => DataBaseMappings.ServEventFromEntity(se))
                .ToList();

            return servEvents;
        }
    }
}