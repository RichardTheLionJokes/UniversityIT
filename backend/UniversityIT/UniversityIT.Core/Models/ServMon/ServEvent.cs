using CSharpFunctionalExtensions;
using UniversityIT.Core.Enums.Common;

namespace UniversityIT.Core.Models.ServMon
{
    public class ServEvent
    {
        private ServEvent(Guid id, DateTime happenedAt, NetStatus servStatus, Server? server)
        {
            Id = id;
            HappenedAt = happenedAt;
            ServStatus = servStatus;
            Server = server;
        }

        public Guid Id { get; }
        public DateTime HappenedAt { get; }
        public NetStatus ServStatus { get; }
        public Server? Server { get; }

        public static Result<ServEvent> Create(Guid id, DateTime happenedAt, NetStatus servStatus, Server? server)
        {
            var servEvent = new ServEvent(id, happenedAt, servStatus, server);

            return Result.Success(servEvent);
        }
    }
}