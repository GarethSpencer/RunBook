using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Weight: AuditableEntity, IRecording
{
    public required int WeightId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required decimal RecordedWeight { get; set; }

    public required User User { get; set; }

    [NotMapped]
    public int RecordingId
    {
        get
        {
            return WeightId;
        }

        set
        {
            WeightId = value;
        }
    }
}
