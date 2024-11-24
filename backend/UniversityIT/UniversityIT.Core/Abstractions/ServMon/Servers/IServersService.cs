using CSharpFunctionalExtensions;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.Core.ValueObjects;

namespace UniversityIT.Core.Abstractions.ServMon.Servers
{
    public interface IServersService
    {
        Task<Result<Guid>> CreateServer(Server server);
        Task<List<Server>> GetAllServers();
        Task<Guid> UpdateServer(Guid id, NetAddress netAddress, string description, string shortDescription, bool activity);
        Task<Guid> DeleteServer(Guid id);
        Task<NetStatus> PingServer(Server server);
        Task<NetStatus> PingServerById(Guid id);
    }
}