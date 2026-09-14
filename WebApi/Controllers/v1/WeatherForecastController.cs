using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Utilities.Helpers;
using Utilities.Models.Responses.Generic;

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private readonly ILogger _logger = logger;

        [HttpGet(Name = "Test")]
        public async Task<CommonResponse> Get()
        {
            var response = new CommonResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Success",
            };

            return response.WithResponseLog(_logger);
        }
    }
}
