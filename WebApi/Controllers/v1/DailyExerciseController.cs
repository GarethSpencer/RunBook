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
public class DailyExerciseController(
    IDailyExerciseService dailyExerciseService,
    IValidator<DateOnly> getByDayOrMonthRequestValidator,
    IValidator<int> getByYearRequestValidator,
    IValidator<CreateRecordingRequest<bool>> createDailyExerciseRequestValidator,
    IValidator<UpdateRecordingRequest<bool>> updateDailyExerciseRequestValidator) : ControllerBase
{

    [HttpGet("date")]
    [ProducesResponseType(typeof(GetRecordingResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyDailyExerciseByDay([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await dailyExerciseService.GetMyRecordingByDayAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetRecordingsResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyLast30DaysDailyExercises(CancellationToken ct)
    {
        var response = await dailyExerciseService.GetMyLast30DaysRecordingsAsync(ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("month")]
    [ProducesResponseType(typeof(GetRecordingsResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyDailyExercisesByMonth([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await dailyExerciseService.GetMyRecordingsByMonthAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("year")]
    [ProducesResponseType(typeof(GetRecordingsResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyDailyExercisesByYear([FromQuery] int year, CancellationToken ct)
    {
        var validation = await getByYearRequestValidator.ValidateAsync(year, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await dailyExerciseService.GetMyRecordingsByYearAsync(year, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateRecordingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMyDailyExercise([FromBody] CreateRecordingRequest<bool> request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await createDailyExerciseRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await dailyExerciseService.CreateMyRecordingAsync(request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{dailyExerciseId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMyDailyExercise([FromRoute] int dailyExerciseId, [FromBody] UpdateRecordingRequest<bool> request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await updateDailyExerciseRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await dailyExerciseService.UpdateMyRecordingAsync(dailyExerciseId, request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{dailyExerciseId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMyDailyExercise([FromRoute] int dailyExerciseId, CancellationToken ct)
    {
        var response = await dailyExerciseService.DeleteMyRecordingAsync(dailyExerciseId, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
