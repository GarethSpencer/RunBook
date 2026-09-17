using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Abstractions.Generic;

public interface IRecordingRepository
{
    Task<RecordingResult?> GetByIdAsync(int dailyExerciseId, Guid userId, CancellationToken ct);
    Task<RecordingResult?> GetByDayAsync(DateOnly date, Guid userId, CancellationToken ct);
    Task<IEnumerable<RecordingResult>> GetLast30DaysAsync(Guid userId, CancellationToken ct);
    Task<IEnumerable<RecordingResult>> GetByMonthAsync(DateOnly monthDate, Guid userId, CancellationToken ct);
    Task<IEnumerable<RecordingResult>> GetByYearAsync(int year, Guid userId, CancellationToken ct);
    Task<IIdCreation> CreateAsync(CreateRecordingRequest request, Guid userId, CancellationToken ct);
    Task UpdateAsync(int id, UpdateRecordingRequest request, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}
