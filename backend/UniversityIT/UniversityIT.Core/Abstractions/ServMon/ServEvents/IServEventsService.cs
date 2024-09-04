using UniversityIT.Core.Enums.ServMon;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.ServEvents
{
    public interface IServEventsService
    {
        Task<List<ServEvent>> GetAllServEvents();
        //Task<Guid> CreateServEvent(Guid servId, ServStatus servStatus);
    }
}