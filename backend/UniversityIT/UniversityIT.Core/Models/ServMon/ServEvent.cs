using CSharpFunctionalExtensions;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.ValueObjects;

namespace UniversityIT.Core.Models.ServMon
{
    public class ServEvent
    {
        private ServEvent(Guid id, DateTime happenedAt, NetStatus servStatus, Guid serverId, NetAddress? serverAddress)
        {
            Id = id;
            HappenedAt = happenedAt;
            ServStatus = servStatus;
            ServerId = serverId;
            ServerAddress = serverAddress;
        }

        public Guid Id { get; }
        public DateTime HappenedAt { get; }
        public NetStatus ServStatus { get; }
        public Guid ServerId { get; }
        public NetAddress? ServerAddress { get; }

        public static Result<ServEvent> Create(Guid id, DateTime happenedAt, NetStatus servStatus, Guid serverId, NetAddress? serverAddress)
        {
            var servEvent = new ServEvent(id, happenedAt, servStatus, serverId, serverAddress);

            return Result.Success(servEvent);
        }
    }
}