using Utilities.Models.Requests;
using Utilities.Models.Responses.Generic;

namespace ServiceLayer.Abstractions;

public interface ITrackedExerciseService
{
    Task<CommonResponse> GetMyTrackedExercisesByDayAsync(DateOnly date, CancellationToken ct);

    Task<CommonResponse> GetMyTrackedExercisesByMonthAsync(DateOnly monthDate, CancellationToken ct);

    Task<CommonResponse> GetMyTrackedExercisesByYearAsync(int year, CancellationToken ct);

    Task<CommonResponse> CreateMyTrackedExerciseAsync(CreateTrackedExerciseRequest request, CancellationToken ct);

    Task<CommonResponse> UpdateMyTrackedExerciseAsync(int trackedExerciseId, UpdateTrackedExerciseRequest request, CancellationToken ct);

    Task<CommonResponse> DeleteMyTrackedExerciseAsync(int trackedExerciseId, CancellationToken ct);
}
