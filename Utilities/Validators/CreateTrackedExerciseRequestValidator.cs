using FluentValidation;
using Utilities.Models.Requests;

namespace Utilities.Validators;

public class CreateTrackedExerciseRequestValidator : AbstractValidator<CreateTrackedExerciseRequest>
{
    public CreateTrackedExerciseRequestValidator()
    {
        RuleFor(x => x.ExerciseTypeId) //Validate against database Ids in service layer
            .NotEmpty().WithMessage("Exercise Type is required.")
            .GreaterThanOrEqualTo(1).WithMessage("Exercise Type Id must be a positive integer.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .GreaterThanOrEqualTo(DateOnly.MinValue).WithMessage("Date is invalid.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Duration is required.")
            .GreaterThanOrEqualTo(1).WithMessage("Duration must be a positive number.")
            .LessThanOrEqualTo(1000).WithMessage("Duration must not exceed 1,000.");
    }
}
