using Utilities.Models.Responses.Generic;
using Utilities.Models.Results;

namespace Utilities.Models.Responses;

public class GetExerciseTypesResponse : CommonResponse
{
    public IEnumerable<ExerciseTypeResult>? ExerciseTypes { get; set; }
}
