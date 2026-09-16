using Utilities.Enums;

namespace Utilities.Models.Results;

public record ExerciseTypeResult
{
    public required int ExerciseTypeId { get; init; }
    public required string Name { get; init; }
    public required ExerciseTypeIntensity Intensity { get; init; }
    public required int CaloriesPerMinute { get; init; }
}
