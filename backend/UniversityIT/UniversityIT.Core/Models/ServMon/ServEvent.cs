using UniversityIT.Core.Enums.ServMon;

namespace UniversityIT.Core.Models.ServMon
{
    public class ServEvent
    {
        public Guid Id { get; }
        public DateTime HappenedAt { get; }
        public ServStatus? ServStatus { get; }
        public Guid ServerId { get; }
        public Server? Server { get; }
    }
}