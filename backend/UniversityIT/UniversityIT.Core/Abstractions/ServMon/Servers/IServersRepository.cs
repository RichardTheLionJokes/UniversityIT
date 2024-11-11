using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.Core.ValueObjects;

namespace UniversityIT.Core.Abstractions.ServMon.Servers
{
    public interface IServersRepository
    {
        Task<Guid> Create(Server server);
        Task<List<Server>> Get();
        Task<Server> GetById(Guid id);
        Task<Server> GetByNetAddress(NetAddress netAddress);
        Task<Guid> Update(Guid id, NetAddress NetAddress, string description, string shortDescription, bool activity);
        Task<Guid> Delete(Guid id);
        Task<NetStatus> ChangeStatus(Guid id, NetStatus status);
    }
}