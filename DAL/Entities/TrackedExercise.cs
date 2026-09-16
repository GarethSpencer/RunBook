using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class TrackedExercise: AuditableEntity, IRecording
{
    public int TrackedExerciseId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required int Duration { get; set; }
    public required int ExerciseTypeId { get; set; }

    public required ExerciseType ExerciseType { get; set; }
    public required User User { get; set; }

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
}
