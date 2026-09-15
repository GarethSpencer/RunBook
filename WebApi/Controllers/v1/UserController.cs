using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ServiceLayer.Abstractions;
using Utilities.Models.Requests;
using Utilities.Models.Responses;
using Utilities.Models.Responses.Generic;
using Utilities.Validators;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class UserController(
    IUserService userService,
    IValidator<UpdateUserRequest> updateUserRequestValidator) : ControllerBase
{

    [HttpGet("me")]
    [ProducesResponseType(typeof(GetUserDetailedResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken ct)
    {
        var response = await userService.GetCurrentUserAsync(ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{userId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await updateUserRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await userService.UpdateUserAsync(userId, request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId, CancellationToken ct)
    {
        var response = await userService.DeleteUserAsync(userId, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
