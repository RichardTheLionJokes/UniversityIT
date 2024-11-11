using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.Auth;
using UniversityIT.Core.Models.HelpDesk;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.DataAccess.Entities.Auth;
using UniversityIT.DataAccess.Entities.HelpDesk;
using UniversityIT.DataAccess.Entities.ServMon;

namespace UniversityIT.DataAccess
{
    internal static class DataBaseMappings
    {
        internal static UserEntity UserToEntity(User user, RoleEntity[] roleEntities)
        {
            return new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                FullName = user.FullName,
                Position = user.Position,
                PhoneNumber = user.PhoneNumber,
                Roles = roleEntities
            };
        }

        internal static User EntityToUser(UserEntity userEntity)
        {
            return User.Create(
                userEntity.Id,
                userEntity.UserName,
                userEntity.PasswordHash,
                userEntity.Email,
                userEntity.FullName,
                userEntity.Position,
                userEntity.PhoneNumber)
                .Value;
        }

        internal static ServerEntity ServerToEntity(Server server)
        {
            return new ServerEntity
            {
                Id = server.Id,
                NetAddress = server.NetAddress,
                Description = server.Description,
                ShortDescription = server.ShortDescription,
                Activity = server.Activity,
                CurrentStatusId = (int)server.CurrentStatus
            };
        }

        internal static Server EntityToServer(ServerEntity serverEntity)
        {
            return Server.Create(
                serverEntity.Id,
                serverEntity.NetAddress,
                serverEntity.Description,
                serverEntity.ShortDescription,
                serverEntity.Activity,
                (NetStatus)serverEntity.CurrentStatusId)
                .Value;
        }

        internal static ServEventEntity ServEventToEntity(ServEvent servEvent)
        {
            return new ServEventEntity
            {
                Id = servEvent.Id,
                HappenedAt = servEvent.HappenedAt,
                ServStatusId = (int)servEvent.ServStatus,
                ServerId = servEvent.ServerId
            };
        }

        internal static ServEvent EntityToServEvent(ServEventEntity serverEntity)
        {
            return ServEvent.Create(
                serverEntity.Id,
                serverEntity.HappenedAt,
                (NetStatus)serverEntity.ServStatusId,
                serverEntity.ServerId,
                serverEntity.Server?.NetAddress)
                .Value;
        }

        internal static TicketEntity TicketToEntity(Ticket ticket)
        {
            return new TicketEntity
            {
                Id = ticket.Id,
                Name = ticket.Name,
                Description = ticket.Description,
                Place = ticket.Place,
                CreatedAt = ticket.CreatedAt,
                NotificationsSent = ticket.NotificationsSent,
                IsCompleted = ticket.IsCompleted,
                UserId = ticket.AuthorId
            };
        }

        internal static Ticket EntityToTicket(TicketEntity ticketEntity)
        {
            return Ticket.Create(
                ticketEntity.Id,
                ticketEntity.Name,
                ticketEntity.Description,
                ticketEntity.Place,
                ticketEntity.CreatedAt,
                ticketEntity.NotificationsSent,
                ticketEntity.IsCompleted,
                ticketEntity.UserId,
                ticketEntity.User?.UserName)
                .Value;
        }
    }
}
