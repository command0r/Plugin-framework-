using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prise.IntegrationTestsHost;

namespace Prise.IntegrationTests
{
    public partial class AppHostWebApplicationFactory
       : WebApplicationFactory<Prise.IntegrationTestsHost.Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                    services.AddSingleton<ICommandLineArguments>((s) => this.commandLineArguments))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Prise.IntegrationTestsHost.Startup>();
                    webBuilder.ConfigureAppConfiguration(builder => builder.AddInMemoryCollection(this.settings));
                });
        }
    }
}