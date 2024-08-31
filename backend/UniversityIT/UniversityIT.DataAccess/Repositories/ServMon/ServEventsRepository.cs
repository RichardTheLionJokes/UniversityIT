using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.DataAccess.Repositories.ServMon
{
    public class ServEventsRepository : IServEventsRepository
    {
        public Task<Guid> Create(ServEvent servEvent)
        {
            throw new NotImplementedException();
        }
    }
}