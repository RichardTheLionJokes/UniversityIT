using CSharpFunctionalExtensions;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.Core.ValueObjects;

namespace UniversityIT.Application.Services.ServMon
{
    public class ServersService : IServersService
    {
        private readonly IServersRepository _serversRepository;
        private readonly IServEventsRepository _servEventsRepository;
        private readonly IPinger _pinger;

        public ServersService(IServersRepository serversRepository, IServEventsRepository servEventsRepository, IPinger pinger)
        {
            _serversRepository = serversRepository;
            _servEventsRepository = servEventsRepository;
            _pinger = pinger;
        }

        public async Task<Result<Guid>> CreateServer(Server server)
        {
            var netAddress = server.NetAddress;
            var serverExists = await _serversRepository.ServerExists(netAddress);
            if (!serverExists)
                return await _serversRepository.Create(server);
            else
                return Result.Failure<Guid>($"There is already a server with name '{netAddress.NetName}' and ip '{netAddress.IpAddress}'");
        }

        public async Task<List<Server>> GetAllServers()
        {
            return await _serversRepository.Get();
        }

        public async Task<Guid> UpdateServer(Guid id, NetAddress netAddress, string shortDescription, string description, bool activity)
        {
            return await _serversRepository.Update(id, netAddress, shortDescription, description, activity);
        }

        public async Task<Guid> DeleteServer(Guid id)
        {
            return await _serversRepository.Delete(id);
        }

        public async Task<NetStatus> PingServer(Server server)
        {
            NetStatus curStatus = await _pinger.AddressStatus(server);

            if (curStatus != server.CurrentStatus)
            {
                await _serversRepository.ChangeStatus(server.Id, curStatus);

                var servEvent = ServEvent.Create(
                    Guid.NewGuid(),
                    DateTime.Now,
                    curStatus,
                    server.Id,
                    server.NetAddress);

                await _servEventsRepository.Create(servEvent.Value);
            }

            return curStatus;
        }

        public async Task<NetStatus> PingServerById(Guid id)
        {
            Server server = await _serversRepository.GetById(id);

            NetStatus curStatus = await PingServer(server);

            return curStatus;
        }
    }
}