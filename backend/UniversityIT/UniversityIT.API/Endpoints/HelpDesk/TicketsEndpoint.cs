using Microsoft.AspNetCore.Mvc;
using UniversityIT.API.Contracts.HelpDesk.Tickets;
using UniversityIT.API.Extentions;
using UniversityIT.Core.Abstractions.Auth.Users;
using UniversityIT.Core.Abstractions.HelpDesk.Tickets;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Models.HelpDesk;

namespace UniversityIT.API.Endpoints.HelpDesk
{
    public static class TicketsEndpoint
    {
        public static IEndpointRouteBuilder MapTicketsEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("tickets")
                .RequireAuthorization();

            endpoints.MapPost(string.Empty, CreateTicket);

            endpoints.MapGet(string.Empty, GetTickets).RequirePermissions(Permission.Read);

            endpoints.MapPut("{id:guid}", UpdateTicket).RequirePermissions(Permission.Read);

            endpoints.MapDelete("{id:guid}", DeleteServer).RequirePermissions(Permission.Delete);

            return endpoints;
        }

        private static async Task<IResult> CreateTicket(
            [FromBody] TicketsRequest request,
            ITicketsService ticketsService,
            IUsersService usersService,
            HttpContext context)
        {
            string? token = context.Request.Cookies["tasty-cookies"];

            if (String.IsNullOrEmpty(token))
                return Results.Problem();

            Guid userId = usersService.GetIdByToken(token);

            if (userId == Guid.Empty)
                return Results.Problem();

            var ticket = Ticket.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.Place,
                DateTime.Now.ToUniversalTime(),
                false,
                request.IsCompleted,
                userId,
                "");

            if (ticket.IsFailure)
                return Results.Problem(ticket.Error);

            var ticketId = await ticketsService.CreateTicket(ticket.Value);

            return Results.Ok(ticketId);
        }

        private static async Task<IResult> GetTickets(
            ITicketsService ticketsService,
            IUsersService usersService,
            HttpContext context,
            bool getAll = false)
        {
            var tickets = new List<Ticket>();

            if (getAll)
            {
                tickets = await ticketsService.GetAllTickets();
            }
            else
            {
                string? token = context.Request.Cookies["tasty-cookies"];

                if (String.IsNullOrEmpty(token))
                {
                    return Results.Problem();
                }

                Guid userId = usersService.GetIdByToken(token);

                if (userId == Guid.Empty)
                {
                    return Results.Problem();
                }

                tickets = await ticketsService.GetTicketsByUserId(userId);
            }

            var response = tickets.Select(t => new TicketsResponse(
                t.Id,
                t.Name,
                t.Description,
                t.Place,
                t.CreatedAt,
                t.NotificationsSent,
                t.IsCompleted,
                t.AuthorId,
                t.Author));

            return Results.Ok(response);
        }

        private static async Task<IResult> UpdateTicket(Guid id, [FromBody] TicketsRequest request, ITicketsService ticketsService)
        {
            var ticketId = await ticketsService.UpdateTicket(
                id,
                request.Name,
                request.Description,
                request.Place,
                request.IsCompleted);

            return Results.Ok(ticketId);
        }

        private static async Task<IResult> DeleteServer(Guid id, ITicketsService ticketsService)
        {
            return Results.Ok(await ticketsService.DeleteTicket(id));
        }
    }
}