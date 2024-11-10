namespace UniversityIT.API.Contracts.ServMon.ServEvents
{
    public record ServEventsResponse(
        Guid Id,
        DateTime HappenedAt,
        string ServStatus,
        Guid ServerId,
        string? ServerName,
        string? ServerIp);
}