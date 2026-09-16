using DAL.Entities.Base;

namespace DAL.Entities;

public class Weight : AuditableEntity
{
    public int WeightId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required decimal RecordedWeight { get; set; }

    public User? User { get; set; }
}
