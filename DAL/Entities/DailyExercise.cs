using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class DailyExercise : AuditableEntity, IRecording
{
    public int DailyExerciseId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public bool Exercised { get; set; }

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

    [NotMapped]
    public bool RecordedValue
    {
        get
        {
            return Exercised;
        }
        set
        {
            Exercised = value;
        }
    }
}
