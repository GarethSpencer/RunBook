using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ServiceLayer.Abstractions;
using ServiceLayer.Infrastructure;
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
public class StepsController(
    IStepsService stepsService,
    IValidator<DateOnly> getByDayOrMonthRequestValidator,
    IValidator<int> getByYearRequestValidator,
    IValidator<CreateRecordingRequest> createStepsRequestValidator,
    IValidator<UpdateRecordingRequest> updateStepsRequestValidator) : ControllerBase
{

    [HttpGet("date")]
    [ProducesResponseType(typeof(GetRecordingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyStepsByDay([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await stepsService.GetMyRecordingByDayAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(GetRecordingsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyLast30DaysDailyExercises(CancellationToken ct)
    {
        var response = await stepsService.GetMyLast30DaysRecordingsAsync(ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("month")]
    [ProducesResponseType(typeof(GetRecordingsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyStepssByMonth([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await stepsService.GetMyRecordingsByMonthAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("year")]
    [ProducesResponseType(typeof(GetRecordingsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyStepssByYear([FromQuery] int year, CancellationToken ct)
    {
        var validation = await getByYearRequestValidator.ValidateAsync(year, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await stepsService.GetMyRecordingsByYearAsync(year, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateRecordingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMySteps([FromBody] CreateRecordingRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await createStepsRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await stepsService.CreateMyRecordingAsync(request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{stepsId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMySteps([FromRoute] int stepsId, [FromBody] UpdateRecordingRequest request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await updateStepsRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await stepsService.UpdateMyRecordingAsync(stepsId, request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{stepsId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMySteps([FromRoute] int stepsId, CancellationToken ct)
    {
        var response = await stepsService.DeleteMyRecordingAsync(stepsId, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
