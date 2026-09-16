using Asp.Versioning;
using Azure.Core;
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
public class TrackedExerciseController(
    ITrackedExerciseService trackedExerciseService,
    IValidator<DateOnly> getTrackedExerciseByDayRequestValidator,
    IValidator<DateOnly> getTrackedExerciseByMonthRequestValidator,
    IValidator<int> getTrackedExerciseByYearRequestValidator,
    IValidator<CreateTrackedExerciseRequest> createTrackedExerciseRequestValidator,
    IValidator<UpdateTrackedExerciseRequest> updateTrackedExerciseRequestValidator) : ControllerBase
{

    [HttpGet("date")]
    [ProducesResponseType(typeof(GetTrackedExercisesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyTrackedExercisesByDay([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getTrackedExerciseByDayRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await trackedExerciseService.GetMyTrackedExercisesByDayAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("month")]
    [ProducesResponseType(typeof(GetTrackedExercisesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyTrackedExercisesByMonth([FromQuery] DateOnly monthDate, CancellationToken ct)
    {
        var validation = await getTrackedExerciseByMonthRequestValidator.ValidateAsync(monthDate, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await trackedExerciseService.GetMyTrackedExercisesByMonthAsync(monthDate, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("year")]
    [ProducesResponseType(typeof(GetTrackedExercisesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyTrackedExercisesByYear([FromQuery] int year, CancellationToken ct)
    {
        var validation = await getTrackedExerciseByYearRequestValidator.ValidateAsync(year, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await trackedExerciseService.GetMyTrackedExercisesByYearAsync(year, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateTrackedExerciseResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMyTrackedExercise([FromBody] CreateTrackedExerciseRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await createTrackedExerciseRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await trackedExerciseService.CreateMyTrackedExerciseAsync(request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{trackedExerciseId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMyTrackedExercise([FromRoute] int trackedExerciseId, [FromBody] UpdateTrackedExerciseRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await updateTrackedExerciseRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await trackedExerciseService.UpdateMyTrackedExerciseAsync(trackedExerciseId, request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{trackedExerciseId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMyTrackedExercise([FromRoute] int trackedExerciseId, CancellationToken ct)
    {
        var response = await trackedExerciseService.DeleteMyTrackedExerciseAsync(trackedExerciseId, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
