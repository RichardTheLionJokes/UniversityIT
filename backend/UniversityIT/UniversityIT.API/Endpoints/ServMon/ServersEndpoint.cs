﻿using Microsoft.AspNetCore.Mvc;
using UniversityIT.API.Contracts.ServMon.Servers;
using UniversityIT.API.Extentions;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Enums.Common;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.Core.ValueObjects;

namespace UniversityIT.API.Endpoints.ServMon
{
    public static class ServersEndpoint
    {
        public static IEndpointRouteBuilder MapServersEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("servers")
                .RequireAuthorization();

            endpoints.MapPost(string.Empty, CreateServer).RequirePermissions(Permission.Create);

            endpoints.MapGet(string.Empty, GetServers).RequirePermissions(Permission.Read);

            endpoints.MapPut("{id:guid}", UpdateServer).RequirePermissions(Permission.Update);

            endpoints.MapDelete("{id:guid}", DeleteServer).RequirePermissions(Permission.Delete);

            endpoints.MapGet("ping/{id:guid}", PingServerById).RequirePermissions(Permission.Read);

            return endpoints;
        }

        private static async Task<IResult> CreateServer(
            [FromBody] ServersRequest request,
            IServersService serversService)
        {
            var netAddress = NetAddress.Create(request.Name, request.IpAddress);

            if (netAddress.IsFailure)
                return Results.Problem(netAddress.Error);

            var server = Server.Create(
                Guid.NewGuid(),
                netAddress.Value,
                request.ShortDescription,
                request.Description,
                request.Activity,
                NetStatus.Undefined);

            if (server.IsFailure)
                return Results.Problem(server.Error);

            var serverId = await serversService.CreateServer(server.Value);

            if (serverId.IsFailure)
                return Results.Problem(serverId.Error);

            return Results.Ok(serverId.Value);
        }

        private static async Task<IResult> GetServers(IServersService serversService)
        {
            var servers = await serversService.GetAllServers();

            var response = servers
                .Select(s => new ServersResponse(
                    s.Id,
                    s.NetAddress.NetName,
                    s.NetAddress.IpAddress,
                    s.ShortDescription,
                    s.Description,
                    s.Activity,
                    s.CurrentStatus.ToString()));

            return Results.Ok(response);
        }

        private static async Task<IResult> UpdateServer(Guid id, [FromBody] ServersRequest request, IServersService serversService)
        {
            var serverId = await serversService.UpdateServer(
                id,
                NetAddress.Create(request.Name, request.IpAddress).Value,
                request.ShortDescription,
                request.Description,
                request.Activity);

            return Results.Ok(serverId);
        }

        private static async Task<IResult> DeleteServer(Guid id, IServersService serversService)
        {
            return Results.Ok(await serversService.DeleteServer(id));
        }

        private static async Task<IResult> PingServerById(Guid id, IServersService serversService)
        {
            NetStatus servStatus = await serversService.PingServerById(id);
            return Results.Ok(servStatus.ToString());
        }
    }
}