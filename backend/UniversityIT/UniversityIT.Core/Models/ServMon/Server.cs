using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.ServMon
{
    public class Server
    {
        public const int MAX_NAME_LENGTH = 50;
        public const int MAX_SHORT_DESCR_LENGTH = 250;
        public const int MAX_DESCR_LENGTH = 1000;

        private Server(Guid id, string name, string ipAddress, string shortDescription, string description, bool activity) 
        {
            Id = id;
            Name = name;
            IpAddress = ipAddress;
            ShortDescription = shortDescription;
            Description = description;
            Activity = activity;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string IpAddress { get; } = string.Empty;
        public string ShortDescription { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public bool Activity { get; }

        public static Result<Server> Create(Guid id, string name, string ipAddress, string shortDescription, string description, bool activity)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(ipAddress))
            {
                return Result.Failure<Server>($"'{nameof(name)}' and '{nameof(ipAddress)}' can't be empty at the same time");
            }
            if (name.Length > MAX_NAME_LENGTH)
            {
                return Result.Failure<Server>($"'{nameof(name)}' can't be longer then {MAX_NAME_LENGTH} symbols");
            }
            if (shortDescription.Length > MAX_SHORT_DESCR_LENGTH)
            {
                return Result.Failure<Server>($"'{nameof(shortDescription)}' can't be longer then {MAX_SHORT_DESCR_LENGTH} symbols");
            }
            if (description.Length > MAX_DESCR_LENGTH)
            {
                return Result.Failure<Server>($"'{nameof(description)}' can't be longer then {MAX_DESCR_LENGTH} symbols");
            }

            var server = new Server(id, name, ipAddress, shortDescription, description, activity);

            return Result.Success(server);
        }
    }
}
