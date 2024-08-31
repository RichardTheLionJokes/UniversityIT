using UniversityIT.Core.Enums.ServMon;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.Servers
{
    public interface IServersService
    {
        Task<Guid> CreateServer(Server server);
        Task<Guid> DeleteServer(Guid id);
        Task<List<Server>> GetAllServers();
        Task<Guid> UpdateServer(Guid id, string name, string ipAddress, string description, string shortDescription, bool activity);
    }
}