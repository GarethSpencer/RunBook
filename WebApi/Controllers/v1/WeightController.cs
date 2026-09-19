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
public class WeightController(
    IWeightService weightService,
    IValidator<DateOnly> getByDayOrMonthRequestValidator,
    IValidator<int> getByYearRequestValidator,
    IValidator<CreateRecordingRequest<decimal>> createWeightRequestValidator,
    IValidator<UpdateRecordingRequest<decimal>> updateWeightRequestValidator) : ControllerBase
{

    [HttpGet("date")]
    [ProducesResponseType(typeof(GetRecordingResponse<decimal>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyWeightByDay([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await weightService.GetMyRecordingByDayAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(GetRecordingsResponse<decimal>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyLast30DaysDailyExercises(CancellationToken ct)
    {
        var response = await weightService.GetMyLast30DaysRecordingsAsync(ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("month")]
    [ProducesResponseType(typeof(GetRecordingsResponse<decimal>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyWeightByMonth([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await weightService.GetMyRecordingsByMonthAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("year")]
    [ProducesResponseType(typeof(GetRecordingsResponse<decimal>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyWeightByYear([FromQuery] int year, CancellationToken ct)
    {
        var validation = await getByYearRequestValidator.ValidateAsync(year, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await weightService.GetMyRecordingsByYearAsync(year, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateRecordingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMyWeight([FromBody] CreateRecordingRequest<decimal> request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await createWeightRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await weightService.CreateMyRecordingAsync(request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{weightId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMyWeight([FromRoute] int weightId, [FromBody] UpdateRecordingRequest<decimal> request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await updateWeightRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await weightService.UpdateMyRecordingAsync(weightId, request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{weightId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMyWeight([FromRoute] int weightId, CancellationToken ct)
    {
        var response = await weightService.DeleteMyRecordingAsync(weightId, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
