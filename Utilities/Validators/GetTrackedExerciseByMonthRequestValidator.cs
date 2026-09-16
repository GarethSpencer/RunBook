using FluentValidation;

namespace Utilities.Validators;

public class GetTrackedExerciseByMonthRequestValidator : AbstractValidator<DateOnly>
{
    public GetTrackedExerciseByMonthRequestValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Date is required.")
            .GreaterThanOrEqualTo(DateOnly.MinValue).WithMessage("Date is invalid.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date cannot be in the future.")
            .OverridePropertyName("MonthDate");
    }
}
