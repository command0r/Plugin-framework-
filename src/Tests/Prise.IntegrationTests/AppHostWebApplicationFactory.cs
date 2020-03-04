using Microsoft.AspNetCore.Mvc.Testing;
using Prise.IntegrationTestsHost;
using System.Collections.Generic;

namespace Prise.IntegrationTests
{
    public class CommandLineArgumentsLazy : ICommandLineArguments
    {
        public bool UseLazyService { get; set; }
        public bool UseCollectibleAssemblies { get; set; }
    }

    public partial class AppHostWebApplicationFactory
       : WebApplicationFactory<Prise.IntegrationTestsHost.Startup>
    {
        internal static AppHostWebApplicationFactory Default() => new AppHostWebApplicationFactory(new CommandLineArgumentsLazy(), null);

        private readonly Dictionary<string, string> settings;
        private readonly ICommandLineArguments commandLineArguments;
        public AppHostWebApplicationFactory(ICommandLineArguments commandLineArguments, Dictionary<string, string> settings = null)
        {
            this.commandLineArguments = commandLineArguments;
            this.settings = settings;
        }
    }
}