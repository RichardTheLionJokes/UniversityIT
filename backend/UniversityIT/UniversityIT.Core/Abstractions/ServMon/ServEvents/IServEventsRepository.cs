using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.ServEvents
{
    public interface IServEventsRepository
    {
        Task<Guid> Create(ServEvent servEvent);
    }
}