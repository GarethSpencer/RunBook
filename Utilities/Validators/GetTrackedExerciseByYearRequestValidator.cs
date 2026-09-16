using FluentValidation;

namespace Utilities.Validators;

public class GetTrackedExerciseByYearRequestValidator : AbstractValidator<int>
{
    public GetTrackedExerciseByYearRequestValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Year is required.")
            .GreaterThanOrEqualTo(DateOnly.MinValue.Year).WithMessage("Year is invalid.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow).Year).WithMessage("Year cannot be in the future.")
            .OverridePropertyName("Year");
    }
}
