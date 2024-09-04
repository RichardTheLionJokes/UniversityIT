using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Enums.ServMon;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.DataAccess.Entities.ServMon;

namespace UniversityIT.DataAccess.Repositories.ServMon
{
    public class ServersRepository : IServersRepository
    {
        private readonly UniversityITDbContext _context;

        public ServersRepository(UniversityITDbContext context)
        {
            _context = context;
        }

        public async Task<List<Server>> Get()
        {
            var serverEntities = await _context.Servers
                .AsNoTracking()
                .ToListAsync();

            var servers = serverEntities
                .Select(s => Server.Create(s.Id, s.Name, s.IpAddress, s.Description, s.ShortDescription, s.Activity, (ServStatus)s.CurrentStatusId).Value)
                .ToList();

            return servers;
        }

        public async Task<Server> GetById(Guid id)
        {
            var serverEntity = await _context.Servers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception();

            //return _mapper.Map<Server>(serverEntity);
            return Server.Create(
                serverEntity.Id,
                serverEntity.Name,
                serverEntity.IpAddress,
                serverEntity.Description,
                serverEntity.ShortDescription,
                serverEntity.Activity,
                (ServStatus)serverEntity.CurrentStatusId).Value;
        }
        public async Task<Guid> Create(Server server)
        {
            var serverEntity = new ServerEntity
            {
                Id = server.Id,
                Name = server.Name,
                IpAddress = server.IpAddress,
                Description = server.Description,
                ShortDescription = server.ShortDescription,
                Activity = server.Activity,
                CurrentStatusId = (int)server.CurrentStatus
            };
            await _context.Servers.AddAsync(serverEntity);
            await _context.SaveChangesAsync();
            return serverEntity.Id;
        }
        public async Task<Guid> Update(Guid id, string name, string ipAddress, string shortDescription, string description, bool activity)
        {
            await _context.Servers
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(s => s.Name, s => name)
                    .SetProperty(s => s.IpAddress, s => ipAddress)
                    .SetProperty(s => s.ShortDescription, s => shortDescription)
                    .SetProperty(s => s.Description, s => description)
                    .SetProperty(s => s.Activity, s => activity));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Servers
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<Guid> ChangeStatus(Guid id, ServStatus status)
        {
            await _context.Servers
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(s => s.CurrentStatusId, s => (int)status));

            return id;
        }
    }
}