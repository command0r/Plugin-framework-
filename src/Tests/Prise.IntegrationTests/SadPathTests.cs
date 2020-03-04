using System;
using System.Net.Http;
using System.Threading.Tasks;
using Prise.IntegrationTestsHost.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Prise.IntegrationTests
{
    public class SadPathTests : CalculationPluginTestsBase
    {
        public SadPathTests() : base(AppHostWebApplicationFactory.Default()) { }

        [Fact]
        public async Task PluginG_DoesNotExists()
        {
            // Arrange
            var payload = new CalculationRequestModel
            {
                A = 100,
                B = 150
            };

            //Act
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Post<CalculationResponseModel>(_client, "PluginG", "/eager", payload));
        }

        [Fact]
        public async Task PluginA_Description_Does_Not_Work()
        {
            // Arrange, Act
            await Assert.ThrowsAsync<Prise.Proxy.Exceptions.PriseProxyException>(async () => await GetRaw(_client, "PluginB", "/disco/description"));
        }
    }
}
