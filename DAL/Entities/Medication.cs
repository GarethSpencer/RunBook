using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Medication: AuditableEntity, IRecording
{
    public int MedicationId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public bool TakenMedication { get; set; }

    public User? User { get; set; }

    [NotMapped]
    public int RecordingId
    {
        get
        {
            return MedicationId;
        }

        set
        {
            MedicationId = value;
        }
    }

    [NotMapped]
    public bool RecordedValue
    {
        get
        {
            return TakenMedication;
        }
        set
        {
            TakenMedication = value;
        }
    }
}
