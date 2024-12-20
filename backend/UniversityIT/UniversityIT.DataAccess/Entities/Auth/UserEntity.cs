﻿using UniversityIT.DataAccess.Entities.HelpDesk;

namespace UniversityIT.DataAccess.Entities.Auth
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<RoleEntity> Roles { get; set; } = [];
        public ICollection<TicketEntity> Tickets { get; set; } = [];
    }
}