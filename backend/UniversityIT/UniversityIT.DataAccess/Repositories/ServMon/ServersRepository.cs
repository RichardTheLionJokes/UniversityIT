using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.Core.ValueObjects;

namespace UniversityIT.DataAccess.Repositories.ServMon
{
    public class ServersRepository : IServersRepository
    {
        private readonly UniversityITDbContext _context;

        public ServersRepository(UniversityITDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Server server)
        {
            var serverEntity = DataBaseMappings.ServerToEntity(server);

            await _context.Servers.AddAsync(serverEntity);
            await _context.SaveChangesAsync();

            return serverEntity.Id;
        }

        public async Task<List<Server>> Get()
        {
            var serverEntities = await _context.Servers
                .AsNoTracking()
                .OrderBy(s => s.NetAddress.NetName)
                .ToListAsync();

            var servers = serverEntities
                .Select(s => DataBaseMappings.ServerFromEntity(s))
                .ToList();

            return servers;
        }

        public async Task<Server> GetById(Guid id)
        {
            var serverEntity = await _context.Servers
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception();

            return DataBaseMappings.ServerFromEntity(serverEntity);
        }

        public async Task<Guid> Update(Guid id, NetAddress NetAddress, string shortDescription, string description, bool activity)
        {
            await _context.Servers
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(s => s.NetAddress.NetName, s => NetAddress.NetName)
                    .SetProperty(s => s.NetAddress.IpAddress, s => NetAddress.IpAddress)
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

        public async Task<NetStatus> ChangeStatus(Guid id, NetStatus status)
        {
            await _context.Servers
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(s => s.CurrentStatusId, s => (int)status));

            return status;
        }

        public async Task<bool> ServerExists(NetAddress netAddress)
        {
            return await _context.Servers
                .AsNoTracking()
                .AnyAsync(s => s.NetAddress == netAddress);
        }
    }
}