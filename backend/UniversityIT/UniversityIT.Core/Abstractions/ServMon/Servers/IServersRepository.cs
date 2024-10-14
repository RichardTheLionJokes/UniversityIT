using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.Servers
{
    public interface IServersRepository
    {
        Task<Guid> Create(Server server);
        Task<List<Server>> Get();
        Task<Server> GetById(Guid id);
        Task<Guid> Update(Guid id, string name, string ipAddress, string description, string shortDescription, bool activity);
        Task<Guid> Delete(Guid id);
        Task<NetStatus> ChangeStatus(Guid id, NetStatus status);
    }
}