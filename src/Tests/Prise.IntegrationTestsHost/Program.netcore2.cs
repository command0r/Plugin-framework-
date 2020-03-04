using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Prise.IntegrationTestsHost
{
    public partial class Program
    {

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var consoleConfig = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ICommandLineArguments>(new CommandLineArguments(consoleConfig));
                })
                .UseStartup<Startup>();
        }

    }
}
