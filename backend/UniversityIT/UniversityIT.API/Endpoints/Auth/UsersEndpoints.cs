using UniversityIT.API.Contracts.Auth.Users;
using UniversityIT.Core.Abstractions.Auth.Users;

namespace UniversityIT.API.Endpoints.Auth
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);

            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(
            RegisterUsersRequest request,
            IUsersService usersService)
        {
            await usersService.Register(request.UserName, request.Password, request.Email, request.FullName, request.Position, request.Phone);

            return Results.Ok();
        }

        private static async Task<IResult> Login(
            LoginUsersRequest request,
            IUsersService usersService,
            HttpContext context)
        {
            var (token,user) = await usersService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("tasty-cookies", token);

            var response = new LoginUsersResponse(user.UserName, user.Email);

            return Results.Ok(response);
        }
    }
}