using CSharpFunctionalExtensions;
using UniversityIT.Core.Enums.ServMon;

namespace UniversityIT.Core.Models.ServMon
{
    public class ServEvent
    {
        private ServEvent(Guid id, DateTime happenedAt, ServStatus servStatus, Server server)
        {
            Id = id;
            HappenedAt = happenedAt;
            ServStatus = servStatus;
            //ServerId = serverId;
            Server = server;
        }

        public Guid Id { get; }
        public DateTime HappenedAt { get; }
        public ServStatus ServStatus { get; }
        //public Guid ServerId { get; }
        public Server? Server { get; }

        public static Result<ServEvent> Create(Guid id, DateTime happenedAt, ServStatus servStatus, Server? server)
        {
            //var error = string.Empty;

            var servEvent = new ServEvent(id, happenedAt, servStatus, server);

            return Result.Success(servEvent);
        }
    }
}