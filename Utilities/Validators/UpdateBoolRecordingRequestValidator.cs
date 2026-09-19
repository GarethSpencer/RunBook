using FluentValidation;
using Utilities.Models.Requests;

namespace Utilities.Validators;

public class UpdateBoolRecordingRequestValidator : AbstractValidator<UpdateRecordingRequest<bool>>
{
    public UpdateBoolRecordingRequestValidator()
    {
        RuleFor(x => x.RecordedValue)
            .NotNull().WithMessage("Exercised value is required.");
    }
}
