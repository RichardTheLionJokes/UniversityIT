using Microsoft.AspNetCore.Mvc;
using UniversityIT.API.Contracts.Auth.Users;
using UniversityIT.API.Extentions;
using UniversityIT.Core.Abstractions.Auth.Users;
using UniversityIT.Core.Enums.Auth;

namespace UniversityIT.API.Endpoints.Auth
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);

            app.MapPost("login", Login);

            app.MapGet("logout", Logout).RequireAuthorization().RequirePermissions(Permission.Read);

            app.MapPost("changePassword", ChangePassword).RequirePermissions(Permission.Read);

            app.MapGet("resetPassword/{email}", ResetPassword);

            var endpoints = app.MapGroup("users")
                .RequireAuthorization();

            endpoints.MapGet("{id:guid}", GetUser).RequirePermissions(Permission.Read);

            endpoints.MapGet("{email}", GetUserByEmail).RequirePermissions(Permission.Read);

            endpoints.MapPut("{id:guid}", UpdateUser).RequirePermissions(Permission.Read);

            return app;
        }

        private static async Task<IResult> Register(
            [FromBody] RegisterUsersRequest request,
            IUsersService usersService)
        {
            await usersService.Register(request.UserName, request.Email, request.FullName, request.Position, request.PhoneNumber);

            return Results.Ok();
        }

        private static async Task<IResult> Login(
            [FromBody] LoginUsersRequest request,
            IUsersService usersService,
            HttpContext context)
        {
            var (token,user) = await usersService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("tasty-cookies", token);

            var response = new LoginUsersResponse(user.UserName, user.Email, user.Id);

            return Results.Ok(response);
        }

        private static IResult Logout(HttpContext context)
        {
            context.Response.Cookies.Delete("tasty-cookies");

            return Results.Ok();
        }

        private static async Task<IResult> GetUser(Guid id, IUsersService usersService)
        {
            var user = await usersService.GetUserById(id);

            return Results.Ok(user);
        }

        private static async Task<IResult> GetUserByEmail(string email, IUsersService usersService)
        {
            var user = await usersService.GetUserByEmail(email);

            return Results.Ok(user);
        }

        private static async Task<IResult> UpdateUser(
            Guid id,
            [FromBody] UsersRequest request,
            IUsersService usersService)
        {
            var userId = await usersService.UpdateUser(
                id,
                request.UserName,
                request.Email,
                request.FullName,
                request.Position,
                request.PhoneNumber);

            return Results.Ok(userId);
        }

        private static async Task<IResult> ChangePassword(
            [FromBody] ChangePasswordRequest request,
            IUsersService usersService)
        {
            await usersService.ChangePassword(
                request.Email,
                request.OldPassword,
                request.NewPassword);

            return Results.Ok();
        }

        private static async Task<IResult> ResetPassword(string email, IUsersService usersService)
        {
            await usersService.ResetPassword(email);

            return Results.Ok();
        }
    }
}