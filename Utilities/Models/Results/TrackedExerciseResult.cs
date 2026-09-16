namespace Utilities.Models.Results;

public record TrackedExerciseResult
{
    public required int TrackedExerciseId { get; init; }
    public required int ExerciseTypeId { get; init; }
    public required DateOnly Date { get; init; }
    public required int Duration { get; init; }
}
