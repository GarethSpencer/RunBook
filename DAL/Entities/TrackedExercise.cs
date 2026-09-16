using DAL.Entities.Base;

namespace DAL.Entities;

public class TrackedExercise : AuditableEntity
{
    public int TrackedExerciseId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required int Duration { get; set; }
    public required int ExerciseTypeId { get; set; }

    public ExerciseType? ExerciseType { get; set; }
    public User? User { get; set; }
}
