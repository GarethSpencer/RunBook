using FluentValidation;
using Utilities.Models.Requests;

namespace Utilities.Validators;

public class CreateDecimalRecordingRequestValidator : AbstractValidator<CreateRecordingRequest<decimal>>
{
    public CreateDecimalRecordingRequestValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .GreaterThanOrEqualTo(DateOnly.MinValue).WithMessage("Date is invalid.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Date cannot be in the future.");

        RuleFor(x => x.RecordedValue)
            .NotNull().WithMessage("Exercise value is required.")
            .GreaterThan(0).WithMessage("Exercise value must be a positive number.")
            .LessThanOrEqualTo(1000).WithMessage("Exercise value cannot exceed 1,000.");
    }
}
