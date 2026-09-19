using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Weight : AuditableEntity, IRecording<decimal>
{
    public int WeightId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public decimal RecordedWeight { get; set; }

    public User? User { get; set; }

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

    [NotMapped]
    public decimal RecordedValue
    {
        get
        {
            return RecordedWeight;
        }
        set
        {
            RecordedWeight = value;
        }
    }
}
