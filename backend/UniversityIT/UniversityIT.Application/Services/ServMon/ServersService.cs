﻿using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Enums.ServMon;
using UniversityIT.Core.Models.ServMon;

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

        public async Task<List<Server>> GetAllServers()
        {
            return await _serversRepository.Get();
        }

        public async Task<Guid> CreateServer(Server server)
        {
            return await _serversRepository.Create(server);
        }

        public async Task<Guid> UpdateServer(Guid id, string name, string ipAddress, string shortDescription, string description, bool activity)
        {
            return await _serversRepository.Update(id, name, ipAddress, shortDescription, description, activity);
        }

        public async Task<Guid> DeleteServer(Guid id)
        {
            return await _serversRepository.Delete(id);
        }

        public async Task<bool> PingServerById(Guid id)
        {
            Server server = await _serversRepository.GetById(id);

            bool isActive = await _pinger.AddressIsAvailable(server);
            ServStatus curStatus = isActive ? ServStatus.Available : ServStatus.NotAvailable;

            if (curStatus != server.CurrentStatus)
            {
                await _serversRepository.ChangeStatus(id, curStatus);
                await _servEventsRepository.Create(id, curStatus);
            }

            return isActive;
        }
    }
}