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
            var endpoints = app.MapGroup("servEvents")
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
                se.ServerId,
                se.ServerAddress?.NetName,
                se.ServerAddress?.IpAddress));

            return Results.Ok(response);
        }
    }
}