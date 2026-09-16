using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Steps: AuditableEntity, IRecording
{
    public int StepsId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public bool CompletedSteps { get; set; }

    public User? User { get; set; }

    [NotMapped]
    public int RecordingId
    {
        get
        {
            return StepsId;
        }

        set
        {
            StepsId = value;
        }
    }

    [NotMapped]
    public bool RecordedValue
    {
        get
        {
            return CompletedSteps;
        }
        set
        {
            CompletedSteps = value;
        }
    }
}
