using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class DailyExercise : AuditableEntity, IRecording
{
    public int DailyExerciseId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required bool Exercised { get; set; }

    public User? User { get; set; }

    [NotMapped]
    public int RecordingId
    {
        get
        {
            return DailyExerciseId;
        }

        set
        {
            DailyExerciseId = value;
        }
    }
}
