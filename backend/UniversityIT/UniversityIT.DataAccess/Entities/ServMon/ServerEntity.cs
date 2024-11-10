using UniversityIT.Core.ValueObjects;
using UniversityIT.DataAccess.Entities.Common;

namespace UniversityIT.DataAccess.Entities.ServMon
{
    public class ServerEntity
    {
        public Guid Id { get; set; }
        public required NetAddress NetAddress { get; set; }
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Activity { get; set; }
        public int CurrentStatusId { get; set; }
        public NetStatusEntity? CurrentStatus { get; set; }
        public ICollection<ServEventEntity> ServEvents { get; set; } = [];
    }
}