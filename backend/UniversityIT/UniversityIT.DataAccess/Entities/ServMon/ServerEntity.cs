namespace UniversityIT.DataAccess.Entities.ServMon
{
    public class ServerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Activity { get; set; }
    }
}
