using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Enums;
using Utilities.Models.Results;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
[AllowAnonymous]
public class LookupController : ControllerBase
{
    private static readonly Lazy<List<EnumResult>> Statuses = new(() =>
        [.. Enum.GetValues<ExerciseTypeIntensity>()
            .Cast<ExerciseTypeIntensity>()
            .Select(s => new EnumResult
            {
                Value = (int)s,
                Name = s.ToString()
            })]);

    [HttpGet("exercise-type-intensities")]
    [ProducesResponseType(typeof(List<EnumResult>), StatusCodes.Status200OK)]
    public IActionResult GetExerciseTypeIntensities() => Ok(Statuses.Value);
}
