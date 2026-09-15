namespace Utilities.Models.Requests;

public record UpdateUserRequest
{
    public required string DisplayName { get; set; }
    public required bool Admin { get; set; }
    public required bool Active { get; set; }
    public bool? PrefersKg { get; set; }
    public int? WeeklyExerciseGoal { get; set; }
    public int? DailyStepGoal { get; set; }
    public decimal? TargetWeight { get; set; }
}
