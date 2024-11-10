using CSharpFunctionalExtensions;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.ValueObjects;

namespace UniversityIT.Core.Models.ServMon
{
    public class Server
    {
        public const int MAX_SHORT_DESCR_LENGTH = 250;
        public const int MAX_DESCR_LENGTH = 1000;

        private Server(Guid id, NetAddress netAddress, string shortDescription, string description, bool activity, NetStatus currentStatus) 
        {
            Id = id;
            NetAddress = netAddress;
            ShortDescription = shortDescription;
            Description = description;
            Activity = activity;
            CurrentStatus = currentStatus;
        }

        public Guid Id { get; }
        public NetAddress NetAddress { get; }
        public string ShortDescription { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public bool Activity { get; }
        public NetStatus CurrentStatus { get; }

        public static Result<Server> Create(Guid id, NetAddress netAddress, string shortDescription, string description, bool activity, NetStatus currentStatus)
        {
            if (shortDescription.Length > MAX_SHORT_DESCR_LENGTH)
            {
                return Result.Failure<Server>($"'{nameof(shortDescription)}' can't be longer then {MAX_SHORT_DESCR_LENGTH} symbols");
            }
            if (description.Length > MAX_DESCR_LENGTH)
            {
                return Result.Failure<Server>($"'{nameof(description)}' can't be longer then {MAX_DESCR_LENGTH} symbols");
            }

            var server = new Server(id, netAddress, shortDescription, description, activity, currentStatus);

            return Result.Success(server);
        }
    }
}