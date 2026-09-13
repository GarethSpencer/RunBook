using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Utilities.Models.Responses.Generic;
using Utilities.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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
