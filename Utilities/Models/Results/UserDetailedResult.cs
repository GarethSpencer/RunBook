namespace Utilities.Models.Results;

public record UserDetailedResult
{
    public required Guid UserId { get; init; }
    public required Guid AuthId { get; init; }
    public required string PreferredName { get; init; }
    public required bool Active { get; init; }
    public required bool Admin { get; init; }
    public bool? PrefersKg { get; init; }
    public int? WeeklyExerciseGoal { get; init; }
    public int? DailyStepGoal { get; init; }
    public decimal? TargetWeight { get; init; }
    public required DateTime CreatedOn { get; init; }
    public required Guid CreatedBy { get; init; }
    public required DateTime? UpdatedOn { get; init; }
    public required Guid? UpdatedBy { get; init; }
}
