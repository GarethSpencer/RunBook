using FluentValidation;
using Utilities.Helpers;
using Utilities.Models.Requests;
using static Utilities.Helpers.ValidationHelpers;

namespace Utilities.Validators;

public class CreateExerciseTypeRequestValidator : AbstractValidator<CreateExerciseTypeRequest>
{
    public CreateExerciseTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Must(name => name == name.Trim()).WithMessage("Name cannot have leading or trailing spaces.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(60).WithMessage("Name must not exceed 60 characters.")
            .Matches(RegexConstants.AlphanumericPlus).WithMessage("Name contains invalid characters.")
            .Must(name => CheckProfanity(name)).WithMessage("Name must not contain profanity.");

        RuleFor(x => x.Intensity)
            .IsInEnum().WithMessage("Invalid intensity level.");

        RuleFor(x => x.CaloriesPerMinute)
            .NotEmpty().WithMessage("Calories per minute value is required.")
            .GreaterThanOrEqualTo(1).WithMessage("Calories per minute must be a positive number.")
            .LessThanOrEqualTo(1000).WithMessage("Calories per minute must not exceed 1,000.");
    }
}
