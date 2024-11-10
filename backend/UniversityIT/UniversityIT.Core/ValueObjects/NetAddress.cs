using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace UniversityIT.Core.ValueObjects
{
    public class NetAddress : ValueObject
    {
        public const int MAX_NAME_LENGTH = 50;
        private const string ipRegex = @"([0-9]{1,3}[\.]){3}[0-9]{1,3}";

        public string NetName { get; }
        public string IpAddress { get; }

        public static Result<NetAddress> Create(string netName, string ipAddress)
        {
            if (netName.Length > MAX_NAME_LENGTH)
            {
                return Result.Failure<NetAddress>($"'{nameof(netName)}' can't be longer then {MAX_NAME_LENGTH} symbols");
            }

            if (string.IsNullOrWhiteSpace(netName) && string.IsNullOrWhiteSpace(ipAddress))
            {
                return Result.Failure<NetAddress>($"'{nameof(netName)}' and '{nameof(ipAddress)}' can't be empty at the same time");
            }

            if (!Regex.IsMatch(ipAddress, ipRegex))
                return Result.Failure<NetAddress>($"Invalid '{nameof(ipAddress)}' value");

            return new NetAddress(netName, ipAddress);
        }

        private NetAddress(string netName, string ipAddress)
        {
            NetName = netName;
            IpAddress = ipAddress;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return NetName;
            yield return IpAddress;
        }
    }
}