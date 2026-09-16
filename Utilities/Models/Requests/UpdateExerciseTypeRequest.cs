using Utilities.Enums;

namespace Utilities.Models.Requests;

public record UpdateExerciseTypeRequest
{
    public required string Name { get; set; }
    public required ExerciseTypeIntensity Intensity { get; set; }
    public required int CaloriesPerMinute { get; set; }
}
