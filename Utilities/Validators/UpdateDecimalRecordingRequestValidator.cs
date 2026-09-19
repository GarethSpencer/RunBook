using FluentValidation;
using Utilities.Models.Requests;

namespace Utilities.Validators;

public class UpdateDecimalRecordingRequestValidator : AbstractValidator<UpdateRecordingRequest<decimal>>
{
    public UpdateDecimalRecordingRequestValidator()
    {
        RuleFor(x => x.RecordedValue)
            .NotNull().WithMessage("Exercise value is required.")
            .GreaterThan(0).WithMessage("Exercise value must be a positive number.")
            .LessThanOrEqualTo(1000).WithMessage("Exercise value cannot exceed 1,000.");
    }
}
