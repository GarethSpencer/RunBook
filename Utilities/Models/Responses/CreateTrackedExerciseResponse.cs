using Utilities.Models.Responses.Generic;

namespace Utilities.Models.Responses;

public class CreateTrackedExerciseResponse : CommonResponse
{
    public int? TrackedExerciseId { get; set; }
}
