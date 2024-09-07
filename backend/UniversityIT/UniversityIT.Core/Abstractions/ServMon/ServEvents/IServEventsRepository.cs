using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.ServEvents
{
    public interface IServEventsRepository
    {
        Task<List<ServEvent>> Get();
        Task<Guid> Create(Guid servId, NetStatus servStatus);
    }
}