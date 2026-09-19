using FluentValidation;
using Utilities.Models.Requests;

namespace Utilities.Validators;

public class CreateBoolRecordingRequestValidator : AbstractValidator<CreateRecordingRequest<bool>>
{
    public CreateBoolRecordingRequestValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .GreaterThanOrEqualTo(DateOnly.MinValue).WithMessage("Date is invalid.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.RecordedValue)
            .NotNull().WithMessage("Exercised value is required.");
    }
}
