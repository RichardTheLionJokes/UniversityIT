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
using UniversityIT.Core.Abstractions.ServMon.ServEvents;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Infrastructure.Common;
using UniversityIT.Infrastructure.Background;
using UniversityIT.Core.Abstractions.HelpDesk.Tickets;
using UniversityIT.DataAccess.Repositories.HelpDesk;
using UniversityIT.Application.Services.HelpDesk;
using UniversityIT.Infrastructure.Common.Telegram;
using UniversityIT.Infrastructure.Common.Email;
using UniversityIT.DataAccess.Repositories.FileStructure;
using UniversityIT.Application.Services.FileStructure;
using UniversityIT.Application.Abstractions.FileStructure;
using UniversityIT.Infrastructure.FileStructure;
using UniversityIT.Core.Abstractions.FileStructure.Files;
using UniversityIT.Core.Abstractions.FileStructure.Folders;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

configuration.AddJsonFile("settings.json", false, true);
services.Configure<EMailOptions>(configuration.GetSection("UniversityIT").GetSection(nameof(EMailOptions)));
services.Configure<TelegramOptions>(configuration.GetSection("UniversityIT").GetSection(nameof(TelegramOptions)));
services.Configure<JwtOptions>(configuration.GetSection("UniversityIT").GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection("UniversityIT").GetSection(nameof(AuthorizationOptions)));
services.Configure<PasswordPolicyOptions>(configuration.GetSection("UniversityIT").GetSection(nameof(PasswordPolicyOptions)));

services.AddApiAuthentication(configuration);

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddDbContext<UniversityITDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(UniversityITDbContext)));
});

services.AddSingleton<TelegramService>();

services.AddScoped<IMessageService, EmailService>();
services.AddKeyedScoped<IMessageService, TelegramService>("telegram");
services.AddScoped<IPinger, Pinger>();
services.AddScoped<IFileManagementService, FileManagementService>();

services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IServersRepository, ServersRepository>();
services.AddScoped<IServEventsRepository, ServEventsRepository>();
services.AddScoped<ITicketsRepository, TicketsRepository>();
services.AddScoped<IFoldersRepository, FoldersRepository>();
services.AddScoped<IFilesRepository, FilesRepository>();

services.AddScoped<IUsersService, UsersService>();
services.AddScoped<IServersService, ServersService>();
services.AddScoped<IServEventsService, ServEventsService>();
services.AddScoped<ITicketsService, TicketsService>();
services.AddScoped<IFoldersService, FoldersService>();
services.AddScoped<IFilesService, FilesService>();

services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IPasswordGenerator, PasswordGenerator>();

services.AddHostedService<TelegramBotReceiver>();
services.AddHostedService<ServScanner>();

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowCredentials();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

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