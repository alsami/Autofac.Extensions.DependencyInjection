using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Autofac.WebApi.Sample
{
    public class Program
    {
        public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                /**
                 * This extension adds AutofacServiceProviderFactory to the `Host` and allows
                 * The `ConfigureContainer<TContainerBuilder>(TContainerBuilder builder)` to be added as a delegate
                 * Within the Startup class
                 */
                .UseAutofac()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}