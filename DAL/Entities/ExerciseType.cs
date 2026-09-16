using DAL.Entities.Base;
using Utilities.Enums;

namespace DAL.Entities;

public class ExerciseType : AuditableEntity
{
    public int ExerciseTypeId { get; set; }
    public required string Name { get; set; }
    public required ExerciseTypeIntensity Intensity { get; set; }
    public required int CaloriesPerMinute { get; set; }

    public ICollection<TrackedExercise> TrackedExercises { get; set; } = [];
}
