using FluentValidation;
using Utilities.Helpers;
using Utilities.Models.Requests;
using static Utilities.Helpers.ValidationHelpers;

namespace Utilities.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.PreferredName)
            .NotEmpty().WithMessage("Name is required.")
            .Must(name => name == name.Trim()).WithMessage("Name cannot have leading or trailing spaces.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(30).WithMessage("Name must not exceed 30 characters.")
            .Matches(RegexConstants.AlphanumericPlus).WithMessage("Name contains invalid characters.")
            .Must(name => CheckProfanity(name)).WithMessage("Name must not contain profanity.");

        RuleFor(x => x.Active)
            .NotNull().WithMessage("Active status is required.");

        RuleFor(x => x.DailyStepGoal)
            .GreaterThanOrEqualTo(1000).WithMessage("Daily step goal must be at least 1,000 steps.")
            .LessThanOrEqualTo(50000).WithMessage("Daily step goal must not exceed 50,000 steps.")
            .When(x => x.DailyStepGoal.HasValue);

        RuleFor(x => x.WeeklyExerciseGoal)
            .GreaterThanOrEqualTo(1).WithMessage("Weekly exercise goal must be a positive number.")
            .LessThanOrEqualTo(7).WithMessage("Weekly exercise goal must not exceed 7 days.")
            .When(x => x.WeeklyExerciseGoal.HasValue);

        RuleFor(x => x.TargetWeight)
            .GreaterThanOrEqualTo(30).WithMessage("Target weight must be at least 30 kg.")
            .LessThanOrEqualTo(300).WithMessage("Target weight must not exceed 300 kg.")
            .When(x => x.TargetWeight.HasValue);
    }
}
