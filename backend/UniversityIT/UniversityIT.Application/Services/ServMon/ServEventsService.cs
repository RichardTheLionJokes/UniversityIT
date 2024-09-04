using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Enums.ServMon;
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

        //public async Task<Guid> CreateServEvent(Guid servId, ServStatus servStatus)
        //{
        //    return await _servEventsRepository.Create(servId, servStatus);
        //}
    }
}