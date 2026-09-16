using Utilities.Models.Requests;
using Utilities.Models.Responses.Generic;

namespace ServiceLayer.Abstractions;

public interface IExerciseTypeService
{
    Task<CommonResponse> GetExerciseTypesAsync(CancellationToken ct);

    Task<CommonResponse> CreateExerciseTypeAsync(CreateExerciseTypeRequest request, CancellationToken ct);

    Task<CommonResponse> UpdateExerciseTypeAsync(int exerciseTypeId, UpdateExerciseTypeRequest request, CancellationToken ct);

    Task<CommonResponse> DeleteExerciseTypeAsync(int exerciseTypeId, CancellationToken ct);
}
