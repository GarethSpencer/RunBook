using DAL.Entities;
using RepositoryLayer.Abstractions.Generic;
using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Abstractions;

public interface ITrackedExerciseRepository : IEFRepository<TrackedExercise>
{
    Task<TrackedExerciseResult?> GetByIdAsync(int trackedExerciseId, Guid userId, CancellationToken ct);
    Task<IEnumerable<TrackedExerciseResult>> GetByDayAsync(DateOnly date, Guid userId, CancellationToken ct);
    Task<IEnumerable<TrackedExerciseResult>> GetLast30DaysAsync(Guid userId, CancellationToken ct);
    Task<IEnumerable<TrackedExerciseResult>> GetByMonthAsync(DateOnly monthDate, Guid userId, CancellationToken ct);
    Task<IEnumerable<TrackedExerciseResult>> GetByYearAsync(int year, Guid userId, CancellationToken ct);
    Task<IIdCreation> CreateAsync(CreateTrackedExerciseRequest request, Guid userId, CancellationToken ct);
    Task UpdateAsync(int id, UpdateTrackedExerciseRequest request, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}
