using UniversityIT.DataAccess.Entities.Auth;

namespace UniversityIT.DataAccess.Entities.HelpDesk
{
    public class TicketEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool NotificationsSent { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}