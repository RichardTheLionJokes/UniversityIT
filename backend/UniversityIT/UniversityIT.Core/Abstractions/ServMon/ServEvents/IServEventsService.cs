using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.ServEvents
{
    public interface IServEventsService
    {
        Task<List<ServEvent>> GetAllServEvents();
    }
}