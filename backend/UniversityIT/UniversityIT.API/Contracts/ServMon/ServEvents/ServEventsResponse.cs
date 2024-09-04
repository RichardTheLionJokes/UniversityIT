using UniversityIT.API.Contracts.ServMon.Servers;
using UniversityIT.Core.Enums.ServMon;

namespace UniversityIT.API.Contracts.ServMon.ServEvents
{
    public record ServEventsResponse(
        Guid Id,
        DateTime HappenedAt,
        string ServStatus,
        ServersResponse? Server);
}
