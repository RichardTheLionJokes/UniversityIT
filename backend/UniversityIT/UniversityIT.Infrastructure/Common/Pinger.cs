using System.Net.NetworkInformation;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.Infrastructure.Common
{
    public class Pinger : IPinger
    {
        public async Task<NetStatus> AddressStatus(Server server, int timeout)
        {
            try
            {
                string address = !string.IsNullOrEmpty(server.Name) ? server.Name : server.IpAddress;
                Ping pingSender = new();
                PingReply reply = await pingSender.SendPingAsync(address, timeout);

                if (reply.Status == IPStatus.Success) return NetStatus.Available;
                else return NetStatus.NotAvailable;
            }
            catch
            {
                return NetStatus.Undefined;
            }
        }
    }
}
