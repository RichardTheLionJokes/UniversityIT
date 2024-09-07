using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Application.Services.ServMon
{
    public class ServEventsService : IServEventsService
    {
        private readonly IServEventsRepository _servEventsRepository;

        public ServEventsService(IServEventsRepository servEventsRepository)
        {
            _servEventsRepository = servEventsRepository;
        }

        public async Task<List<ServEvent>> GetAllServEvents()
        {
            return await _servEventsRepository.Get();
        }
    }
}