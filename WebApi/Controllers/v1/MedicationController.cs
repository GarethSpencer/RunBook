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
public class MedicationController(
    IMedicationService medicationService,
    IValidator<DateOnly> getByDayOrMonthRequestValidator,
    IValidator<int> getByYearRequestValidator,
    IValidator<CreateRecordingRequest<bool>> createMedicationRequestValidator,
    IValidator<UpdateRecordingRequest<bool>> updateMedicationRequestValidator) : ControllerBase
{

    [HttpGet("date")]
    [ProducesResponseType(typeof(GetRecordingResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyMedicationByDay([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await medicationService.GetMyRecordingByDayAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetRecordingsResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyLast30DaysDailyExercises(CancellationToken ct)
    {
        var response = await medicationService.GetMyLast30DaysRecordingsAsync(ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("month")]
    [ProducesResponseType(typeof(GetRecordingsResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyMedicationsByMonth([FromQuery] DateOnly date, CancellationToken ct)
    {
        var validation = await getByDayOrMonthRequestValidator.ValidateAsync(date, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await medicationService.GetMyRecordingsByMonthAsync(date, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("year")]
    [ProducesResponseType(typeof(GetRecordingsResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyMedicationsByYear([FromQuery] int year, CancellationToken ct)
    {
        var validation = await getByYearRequestValidator.ValidateAsync(year, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await medicationService.GetMyRecordingsByYearAsync(year, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateRecordingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMyMedication([FromBody] CreateRecordingRequest<bool> request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await createMedicationRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await medicationService.CreateMyRecordingAsync(request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPatch("{medicationId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMyMedication([FromRoute] int medicationId, [FromBody] UpdateRecordingRequest<bool> request, CancellationToken ct)
    {
        if (request == null)
        {
            return BadRequest("Request body is required.");
        }

        var validation = await updateMedicationRequestValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return BadRequest(ValidationErrorFormatter.FormatErrors(validation));
        }

        var response = await medicationService.UpdateMyRecordingAsync(medicationId, request, ct);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{medicationId}")]
    [ProducesResponseType(typeof(CommonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMyMedication([FromRoute] int medicationId, CancellationToken ct)
    {
        var response = await medicationService.DeleteMyRecordingAsync(medicationId, ct);
        return StatusCode((int)response.StatusCode, response);
    }
}
