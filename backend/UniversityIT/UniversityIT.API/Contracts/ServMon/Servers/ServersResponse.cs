namespace UniversityIT.API.Contracts.ServMon.Servers
{
    public record ServersResponse(
        Guid id,
        string Name,
        string IpAddress,
        string Description,
        string ShortDescription,
        bool Activity);
}
