using Microsoft.AspNetCore.Mvc;
using UniversityIT.API.Contracts.ServMon.Servers;
using UniversityIT.API.Extentions;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Enums.ServMon;
using UniversityIT.Core.Models.ServMon;

namespace UniversityIT.API.Endpoints.ServMon
{
    public static class ServersEndpoint
    {
        public static IEndpointRouteBuilder MapServersEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("Servers")
                .RequireAuthorization();

            endpoints.MapPost(string.Empty, CreateServer).RequirePermissions(Permission.Create);

            endpoints.MapGet(string.Empty, GetServers).RequirePermissions(Permission.Read);

            endpoints.MapPut("{id:guid}", UpdateServer).RequirePermissions(Permission.Update);

            endpoints.MapDelete("{id:guid}", DeleteServer).RequirePermissions(Permission.Delete);

            return endpoints;
        }

        private static async Task<IResult> GetServers(IServersService serversService)
        {
            var servers = await serversService.GetAllServers();

            var response = servers.Select(s => new ServersResponse(s.Id, s.Name, s.IpAddress, s.ShortDescription, s.Description, s.Activity, s.CurrentStatus.ToString()));

            return Results.Ok(response);
        }

        private static async Task<IResult> CreateServer(
            [FromBody] ServersRequest request,
            IServersService serversService)
        {
            var server = Server.Create(
            Guid.NewGuid(),
            request.Name,
            request.IpAddress,
            request.Description,
            request.ShortDescription,
            request.Activity,
            ServStatus.Undefined);

            if (server.IsFailure)
            {
                return Results.Problem(server.Error);
            }

            var serverId = await serversService.CreateServer(server.Value);

            return Results.Ok(serverId);
        }

        private static async Task<IResult> UpdateServer(Guid id, [FromBody] ServersRequest request, IServersService serversService)
        {
            var serverId = await serversService.UpdateServer(id, request.Name, request.IpAddress, request.ShortDescription, request.Description, request.Activity);

            return Results.Ok(serverId);
        }

        private static async Task<IResult> DeleteServer(Guid id, IServersService serversService)
        {
            return Results.Ok(await serversService.DeleteServer(id));
        }
    }
}