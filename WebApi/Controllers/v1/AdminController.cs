using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ServiceLayer.Abstractions;
using Utilities.Models.Requests.Generic;
using Utilities.Models.Responses;
using Utilities.Validators;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class AdminController(
    IUserService userService,
    IValidator<PaginationBaseRequest> paginationBaseRequestValidator
    ) : ControllerBase
{
    [HttpGet("users")]
    [ProducesResponseType(typeof(GetUsersDetailedResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers([FromQuery] PaginationBaseRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await paginationBaseRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await userService.GetAllUsersAsync(request, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
