using Utilities.Models.Responses.Generic;

namespace Utilities.Models.Responses;

public class CreateExerciseTypeResponse : CommonResponse
{
    public int? ExerciseTypeId { get; set; }
}
