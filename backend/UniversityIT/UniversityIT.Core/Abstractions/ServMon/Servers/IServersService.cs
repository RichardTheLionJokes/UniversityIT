using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.Servers
{
    public interface IServersService
    {
        Task<Guid> CreateServer(Server server);
        Task<List<Server>> GetAllServers();
        Task<Guid> UpdateServer(Guid id, string name, string ipAddress, string description, string shortDescription, bool activity);
        Task<Guid> DeleteServer(Guid id);
        Task<NetStatus> PingServer(Server server);
        Task<NetStatus> PingServerById(Guid id);
    }
}