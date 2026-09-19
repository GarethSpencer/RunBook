using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Abstractions.Generic;

public interface IRecordingRepository<TValue>
{
    Task<RecordingResult<TValue>?> GetByIdAsync(int dailyExerciseId, Guid userId, CancellationToken ct);
    Task<RecordingResult<TValue>?> GetByDayAsync(DateOnly date, Guid userId, CancellationToken ct);
    Task<IEnumerable<RecordingResult<TValue>>> GetLast30DaysAsync(Guid userId, CancellationToken ct);
    Task<IEnumerable<RecordingResult<TValue>>> GetByMonthAsync(DateOnly monthDate, Guid userId, CancellationToken ct);
    Task<IEnumerable<RecordingResult<TValue>>> GetByYearAsync(int year, Guid userId, CancellationToken ct);
    Task<IIdCreation> CreateAsync(CreateRecordingRequest<TValue> request, Guid userId, CancellationToken ct);
    Task UpdateAsync(int id, UpdateRecordingRequest<TValue> request, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}
