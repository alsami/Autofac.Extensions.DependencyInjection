using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Autofac.Sample.Shared
{
    public class ConsoleWriterHostedService : BackgroundService
    {
        private readonly IConsoleWriterService _consoleWriterService;

        public ConsoleWriterHostedService(IConsoleWriterService consoleWriterService) =>
            _consoleWriterService = consoleWriterService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _consoleWriterService.WriteToConsole($"Current time is {DateTime.UtcNow:dd.MM.yyyy hh:mm:sss}");
                _consoleWriterService.WriteToConsole(
                    $"Next run at {DateTime.UtcNow.AddSeconds(5):dd.MM.yyyy hh:mm:sss}");

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _consoleWriterService.WriteToConsole($"Hosting canceled at {DateTime.UtcNow:dd.MM.yyyy hh:mm:sss}");
        }
    }
}