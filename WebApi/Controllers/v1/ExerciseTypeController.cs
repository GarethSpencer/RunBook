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
public class ExerciseTypeController(
    IExerciseTypeService exerciseTypeService,
    IValidator<CreateExerciseTypeRequest> createExerciseTypeRequestValidator,
    IValidator<UpdateExerciseTypeRequest> updateExerciseTypeRequestValidator) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(GetExerciseTypesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExerciseTypes(CancellationToken ct)
    {
        var response = await exerciseTypeService.GetExerciseTypesAsync(ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateExerciseTypeResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateExerciseType([FromBody] CreateExerciseTypeRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await createExerciseTypeRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await exerciseTypeService.CreateExerciseTypeAsync(request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{exerciseTypeId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateExerciseType([FromRoute] int exerciseTypeId, [FromBody] UpdateExerciseTypeRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await updateExerciseTypeRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await exerciseTypeService.UpdateExerciseTypeAsync(exerciseTypeId, request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{exerciseTypeId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteExerciseType([FromRoute] int exerciseTypeId, CancellationToken ct)
    {
        var response = await exerciseTypeService.DeleteExerciseTypeAsync(exerciseTypeId, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
