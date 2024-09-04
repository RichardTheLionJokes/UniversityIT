﻿using UniversityIT.Core.Enums.ServMon;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Core.Abstractions.ServMon.Servers
{
    public interface IServersRepository
    {
        Task<List<Server>> Get();
        Task<Server> GetById(Guid id);
        Task<Guid> Create(Server server);
        Task<Guid> Update(Guid id, string name, string ipAddress, string description, string shortDescription, bool activity);
        Task<Guid> Delete(Guid id);
        Task<Guid> ChangeStatus(Guid id, ServStatus status);
    }
}