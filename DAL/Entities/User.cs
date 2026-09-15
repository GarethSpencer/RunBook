using DAL.Entities.Base;

namespace DAL.Entities;

public class User : AuditableEntity
{
    public Guid UserId { get; set; }
    public required Guid AuthId { get; set; }
    public required string PreferredName { get; set; }
    public bool Active { get; set; }
    public bool Admin { get; set; }
    public bool PrefersKg { get; set; }
    public int? WeeklyExerciseGoal { get; set; }
    public int? DailyStepGoal { get; set; }
    public decimal? TargetWeight { get; set; }
    public ICollection<DailyExercise> DailyExercises { get; set; } = [];
    public ICollection<Steps> Steps { get; set; } = [];
    public ICollection<Medication> Medications { get; set; } = [];
    public ICollection<Weight> Weights { get; set; } = [];
    public ICollection<TrackedExercise> TrackedExercises { get; set; } = [];
}