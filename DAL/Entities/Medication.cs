using DAL.Entities.Abstractions;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities;

public class Medication: AuditableEntity, IRecording
{
    public int MedicationId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required bool TakenMedication { get; set; }

    public required User User { get; set; }

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
}
