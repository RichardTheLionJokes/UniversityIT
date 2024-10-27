using CSharpFunctionalExtensions;
using UniversityIT.Core.Enums.Common;

namespace UniversityIT.Core.Models.ServMon
{
    public class ServEvent
    {
        private ServEvent(Guid id, DateTime happenedAt, NetStatus servStatus, Guid serverId, string? serverName, string? serverIp)
        {
            Id = id;
            HappenedAt = happenedAt;
            ServStatus = servStatus;
            ServerId = serverId;
            ServerName = serverName;
            ServerIp = serverIp;
        }

        public Guid Id { get; }
        public DateTime HappenedAt { get; }
        public NetStatus ServStatus { get; }
        public Guid ServerId { get; }
        public string? ServerName { get; } = string.Empty;
        public string? ServerIp { get; } = string.Empty;

        public static Result<ServEvent> Create(Guid id, DateTime happenedAt, NetStatus servStatus, Guid serverId, string? serverName, string? serverIp)
        {
            var servEvent = new ServEvent(id, happenedAt, servStatus, serverId, serverName, serverIp);

            return Result.Success(servEvent);
        }
    }
}