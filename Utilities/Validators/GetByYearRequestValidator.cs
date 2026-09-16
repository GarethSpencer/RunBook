using FluentValidation;

namespace Utilities.Validators;

public class GetByYearRequestValidator : AbstractValidator<int>
{
    public GetByYearRequestValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Year is required.")
            .GreaterThanOrEqualTo(DateOnly.MinValue.Year).WithMessage("Year is invalid.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow).Year).WithMessage("Year cannot be in the future.")
            .OverridePropertyName("Year");
    }
}
