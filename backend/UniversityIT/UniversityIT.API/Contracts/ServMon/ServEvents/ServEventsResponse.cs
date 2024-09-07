using UniversityIT.API.Contracts.ServMon.Servers;

namespace UniversityIT.API.Contracts.ServMon.ServEvents
{
    public record ServEventsResponse(
        Guid Id,
        DateTime HappenedAt,
        string ServStatus,
        ServersResponse? Server);
}