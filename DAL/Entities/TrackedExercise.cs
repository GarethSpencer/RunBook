using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class TrackedExercise : AuditableEntity, IRecording<int>
{
    public int TrackedExerciseId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required int Duration { get; set; }
    public required int ExerciseTypeId { get; set; }

    public ExerciseType? ExerciseType { get; set; }
    public User? User { get; set; }

    [NotMapped]
    public int RecordingId
    {
        get
        {
            return TrackedExerciseId;
        }

        set
        {
            TrackedExerciseId = value;
        }
    }

    [NotMapped]
    public int RecordedValue
    {
        get
        {
            return Duration;
        }
        set
        {
            Duration = value;
        }
    }
}
