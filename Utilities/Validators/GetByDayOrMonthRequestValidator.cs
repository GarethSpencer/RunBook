using FluentValidation;

namespace Utilities.Validators;

public class GetByDayOrMonthRequestValidator : AbstractValidator<DateOnly>
{
    public GetByDayOrMonthRequestValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Date is required.")
            .GreaterThanOrEqualTo(DateOnly.MinValue).WithMessage("Date is invalid.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date cannot be in the future.")
            .OverridePropertyName("Date");
    }
}
