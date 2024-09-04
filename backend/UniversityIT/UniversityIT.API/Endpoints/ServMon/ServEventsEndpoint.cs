using UniversityIT.API.Contracts.ServMon.Servers;
using UniversityIT.API.Contracts.ServMon.ServEvents;
using UniversityIT.API.Extentions;
using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Core.Enums.Auth;

namespace UniversityIT.API.Endpoints.ServMon
{
    public static class ServEventsEndpoint
    {
        public static IEndpointRouteBuilder MapServEventsEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("ServEvents")
                .RequireAuthorization();

            endpoints.MapGet(string.Empty, GetServEvents).RequirePermissions(Permission.Read);

            return app;
        }

        private static async Task<IResult> GetServEvents(IServEventsService servEventsService)
        {
            var servEvents = await servEventsService.GetAllServEvents();

            var response = servEvents.Select(se => new ServEventsResponse(
                se.Id,
                se.HappenedAt,
                se.ServStatus.ToString(),
                se.Server != null ? new ServersResponse(
                    se.Server.Id,
                    se.Server.Name,
                    se.Server.IpAddress,
                    se.Server.ShortDescription,
                    se.Server.Description,
                    se.Server.Activity,
                    se.Server.CurrentStatus.ToString()) : null));

            return Results.Ok(response);
        }
    }
}