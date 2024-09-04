using UniversityIT.Core.Enums.ServMon;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.ServEvents
{
    public interface IServEventsRepository
    {
        Task<List<ServEvent>> Get();
        Task<Guid> Create(Guid servId, ServStatus servStatus);
    }
}