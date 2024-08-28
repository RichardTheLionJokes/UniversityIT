namespace UniversityIT.API.Contracts.ServMon.Servers
{
    public record ServersRequest(
        string Name,
        string IpAddress,
        string Description,
        string ShortDescription,
        bool Activity);
}
