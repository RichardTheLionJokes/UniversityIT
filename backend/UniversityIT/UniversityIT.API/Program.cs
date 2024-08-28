using Microsoft.EntityFrameworkCore;
using UniversityIT.API.Extentions;
using UniversityIT.Application.Services.ServMon;
using UniversityIT.Infrastructure.Auth;
using UniversityIT.DataAccess;
using UniversityIT.DataAccess.Repositories.ServMon;
using Microsoft.AspNetCore.CookiePolicy;
using UniversityIT.Application.Abstractions.Auth;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.DataAccess.Repositories.Auth;
using UniversityIT.Core.Abstractions.Auth.Users;
using UniversityIT.Application.Services.Auth;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

configuration.AddJsonFile("settings.json", false, true);
services.Configure<JwtOptions>(configuration.GetSection("UniversityIT").GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection("UniversityIT").GetSection(nameof(AuthorizationOptions)));

services.AddApiAuthentication(configuration);

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddDbContext<UniversityITDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString(nameof(UniversityITDbContext)));
});

services.AddScoped<IServersRepository, ServersRepository>();
services.AddScoped<IUsersRepository, UsersRepository>();

services.AddScoped<IServersService, ServersService>();
services.AddScoped<IUsersService, UsersService>();

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.AddMappedEndpoints();

app.Run();