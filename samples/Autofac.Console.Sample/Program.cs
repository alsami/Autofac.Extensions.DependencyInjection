using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Autofac.Sample.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Autofac.Console.Sample
{
    public sealed class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                /**
                 * This extension adds AutofacServiceProviderFactory to the `Host`
                 * Here we directly pass in a delegate to configure the container
                 * Since the regular Host does not provide a functionality to define a Startup class
                 */
                .UseAutofac(ConfigureContainer)
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<ConsoleWriterHostedService>();
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleWriterService>()
                .As<IConsoleWriterService>()
                .SingleInstance();
        }
    }
}