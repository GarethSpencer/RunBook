namespace Utilities.Models.Requests;

public record UpdateTrackedExerciseRequest
{
    public required int ExerciseTypeId { get; set; }
    public required int Duration { get; set; }
}
