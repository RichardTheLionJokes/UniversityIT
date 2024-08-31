namespace UniversityIT.DataAccess.Entities.ServMon
{
    public class ServEventEntity
    {
        public Guid Id { get; set; }
        public DateTime HappenedAt { get; set; }
        public ServStatusEntity? ServStatus { get; set; }
        public Guid ServerId { get; set; }
        public ServerEntity? Server { get; set; }
    }
}