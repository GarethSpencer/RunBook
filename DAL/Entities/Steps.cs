using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Steps: AuditableEntity, IRecording
{
    public int StepsId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required int CompletedSteps { get; set; }

    public required User User { get; set; }

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
}
