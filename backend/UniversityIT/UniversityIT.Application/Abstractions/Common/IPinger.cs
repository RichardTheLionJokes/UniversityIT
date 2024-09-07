using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Application.Abstractions.Common
{
    public interface IPinger
    {
        public Task<NetStatus> AddressStatus(Server server, int timeout = 1000);
    }
}