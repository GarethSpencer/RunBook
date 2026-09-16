using Utilities.Models.Responses.Generic;
using Utilities.Models.Results;

namespace Utilities.Models.Responses;

public class GetTrackedExercisesResponse : CommonResponse
{
    public IEnumerable<TrackedExerciseResult>? TrackedExercises { get; set; }
}
