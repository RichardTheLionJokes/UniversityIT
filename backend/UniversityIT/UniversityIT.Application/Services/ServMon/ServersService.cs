using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Application.Services.ServMon
{
    public class ServersService : IServersService
    {
        private readonly IServersRepository _serversRepository;

        public ServersService(IServersRepository serversRepository)
        {
            _serversRepository = serversRepository;
        }

        public async Task<List<Server>> GetAllServers()
        {
            return await _serversRepository.Get();
        }

        public async Task<Guid> CreateServer(Server server)
        {
            return await _serversRepository.Create(server);
        }

        public async Task<Guid> UpdateServer(Guid id, string name, string ipAddress, string shortDescription, string description, bool activity)
        {
            return await _serversRepository.Update(id, name, ipAddress, shortDescription, description, activity);
        }

        public async Task<Guid> DeleteServer(Guid id)
        {
            return await _serversRepository.Delete(id);
        }
    }
}
