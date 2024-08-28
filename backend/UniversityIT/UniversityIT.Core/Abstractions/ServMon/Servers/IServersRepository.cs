using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.Servers
{
    public interface IServersRepository
    {
        Task<Guid> Create(Server server);
        Task<Guid> Delete(Guid id);
        Task<List<Server>> Get();
        Task<Guid> Update(Guid id, string name, string ipAddress, string description, string shortDescription, bool activity);
    }
}