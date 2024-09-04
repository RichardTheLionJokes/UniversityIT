using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Application.Abstractions.Common
{
    public interface IPinger
    {
        public Task<bool> AddressIsAvailable(Server server, int timeout = 1000);
    }
}
