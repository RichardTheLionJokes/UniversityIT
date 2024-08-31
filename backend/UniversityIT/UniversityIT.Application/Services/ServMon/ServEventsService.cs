using UniversityIT.Core.Abstractions.ServMon.ServEvents;

namespace UniversityIT.Application.Services.ServMon
{
    public class ServEventsService : IServEventsService
    {
        private readonly IServEventsRepository _servEventsRepository;

        public ServEventsService(IServEventsRepository servEventsRepository)
        {
            _servEventsRepository = servEventsRepository;
        }
    }
}