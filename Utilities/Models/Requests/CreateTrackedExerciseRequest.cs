namespace Utilities.Models.Requests;

public record CreateTrackedExerciseRequest
{
    public required int ExerciseTypeId { get; set; }
    public required DateOnly Date { get; set; }
    public required int Duration { get; set; }
}
