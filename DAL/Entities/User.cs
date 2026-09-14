using DAL.Entities.Base;

namespace DAL.Entities;

public class User : AuditableEntity
{
    public required Guid UserId { get; set; }
    public string? AuthId { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required bool Active { get; set; }
    public required bool Admin { get; set; }
    public required bool PerfersKg { get; set; }
    public int? WeeklyExerciseGoal { get; set; }
    public int? DailyStepGoal { get; set; }
    public decimal? TargetWeight { get; set; }
    public ICollection<DailyExercise> DailyExercises { get; set; } = [];
    public ICollection<Steps> Steps { get; set; } = [];
    public ICollection<Medication> Medications { get; set; } = [];
    public ICollection<Weight> Weights { get; set; } = [];
    public ICollection<TrackedExercise> TrackedExercises { get; set; } = [];
}