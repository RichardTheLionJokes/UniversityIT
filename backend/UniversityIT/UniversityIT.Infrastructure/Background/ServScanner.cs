using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Core.Abstractions.ServMon.Servers;

namespace UniversityIT.Infrastructure.Background
{
    public class ServScanner : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServScanner(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _serversService = scope.ServiceProvider
                .GetRequiredService<IServersService>();
            var _pinger = scope.ServiceProvider
                .GetRequiredService<IPinger>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var servers = await _serversService.GetAllServers();
                    foreach (var server in servers)
                    {
                        await _serversService.PingServer(server);
                    }
                }
                catch
                {
                    // обработка ошибки однократного неуспешного выполнения фоновой задачи
                }

                await Task.Delay(5 * 60000, stoppingToken);
            }
        }
    }
}