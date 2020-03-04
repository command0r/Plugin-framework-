using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prise.IntegrationTestsContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prise.IntegrationTestsHost.Controllers
{
    [ApiController]
    [Route("data")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> logger;
        private readonly ITokenService tokenService;
        private readonly IAuthenticatedDataService dataService;
        private readonly ICalculationPlugin calculationPlugin;
        private readonly ITranslationPlugin translationPlugin;
        public DataController(
            ILogger<DataController> logger,
            ITokenService tokenService,
            IAuthenticatedDataService dataService,
            ITranslationPlugin translationPlugin,
            ICalculationPlugin calculationPlugin
            )
        {
            this.logger = logger;
            this.tokenService = tokenService;
            this.dataService = dataService;
            this.translationPlugin = translationPlugin;
            this.calculationPlugin = calculationPlugin;
        }

        [HttpGet]
        public async Task<string> GetToken()
        {
            return await this.tokenService.GenerateToken();
        }

        [HttpGet("{token}")]
        public async Task<IEnumerable<Data>> GetData(string token)
        {
            await this.calculationPlugin.CalculateMutipleAsync(new ComplexCalculationContext { Calculations = new[] { new CalculationContext { A = 10, B = 15 } } });
            await this.translationPlugin.Translate(new TranslationInput { ContentToTranslate = "dog" });
            return await this.dataService.GetData(token);
        }

        [HttpGet("{token}/V1")]
        public async Task<IEnumerable<Data>> GetDataV1(string token)
        {
            await this.calculationPlugin.CalculateMutipleAsync(new ComplexCalculationContext { Calculations = new[] { new CalculationContext { A = 10, B = 15 } } });
            return await this.dataService.GetData(token);
        }
    }
}
