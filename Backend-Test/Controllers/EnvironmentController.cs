using Backend_Test.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Backend_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvironmentController(
        IWebHostEnvironment environment,
        IOptions<ApiInfoOptions> apiInfo) : ControllerBase
    {
        [HttpGet("isproduction")]
        public ActionResult<bool> GetIsProduction() =>
            Ok(environment.IsProduction());

        [HttpGet("apiversion")]
        public ActionResult<string> GetApiVersion() =>
            Ok(apiInfo.Value.ApiVersion);

        [HttpGet("uiversion")]
        public ActionResult<string> GetUIVersion() =>
            Ok(apiInfo.Value.UiVersion);
    }
}
