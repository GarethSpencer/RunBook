using DAL.Data;
using DAL.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstractions.Generic;
using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Infrastructure.Generic;

public abstract class RecordingRepository<TRecording>(RunBookDbContext dbContext) : EFRepository<TRecording>(dbContext), IRecordingRepository
    where TRecording : class, IRecording, new()
{
    public async Task<RecordingResult?> GetByIdAsync(int recordingId, Guid userId, CancellationToken ct)
    {
        var recordings = await _dbSet.Where(de => de.UserId == userId).ToListAsync(ct);
        var recording = recordings.FirstOrDefault(de => de.RecordingId == recordingId);

        if (recording == null)
        {
            return null;
        }

        return new RecordingResult
        {
            RecordingId = recording.RecordingId,
            Date = recording.Date,
            RecordedValue = recording.RecordedValue
        };
    }

    public async Task<RecordingResult?> GetByDayAsync(DateOnly date, Guid userId, CancellationToken ct)
    {
        var recordings = await _dbSet.Where(de => de.Date == date && de.UserId == userId).FirstOrDefaultAsync(ct);

        if (recordings == null)
        {
            return null;
        }

        return new RecordingResult
        {
            RecordingId = recordings.RecordingId,
            Date = recordings.Date,
            RecordedValue = recordings.RecordedValue
        };
    }

    public async Task<IEnumerable<RecordingResult>> GetLast30DaysAsync(Guid userId, CancellationToken ct)
    {
        var recordings = await _dbSet.Where(de => de.UserId == userId && de.Date >= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30))).ToListAsync(ct);

        return recordings.Select(de => new RecordingResult
        {
            RecordingId = de.RecordingId,
            Date = de.Date,
            RecordedValue = de.RecordedValue
        });
    }

    public async Task<IEnumerable<RecordingResult>> GetByMonthAsync(DateOnly monthDate, Guid userId, CancellationToken ct)
    {
        var recordings = await _dbSet.Where(de => de.Date.Month == monthDate.Month && de.Date.Year == monthDate.Year && de.UserId == userId).ToListAsync(ct);

        return recordings.Select(de => new RecordingResult
        {
            RecordingId = de.RecordingId,
            Date = de.Date,
            RecordedValue = de.RecordedValue
        });
    }

    public async Task<IEnumerable<RecordingResult>> GetByYearAsync(int year, Guid userId, CancellationToken ct)
    {
        var recordings = await _dbSet.Where(de => de.Date.Year == year && de.UserId == userId).ToListAsync(ct);

        return recordings.Select(de => new RecordingResult
        {
            RecordingId = de.RecordingId,
            Date = de.Date,
            RecordedValue = de.RecordedValue
        });
    }

    public async Task<IIdCreation> CreateAsync(CreateRecordingRequest request, Guid userId, CancellationToken ct)
    {
        var newRecording = new TRecording
        {
            UserId = userId,
            Date = request.Date,
            RecordedValue = request.RecordedValue
        };

        await _dbSet.AddAsync(newRecording, ct);
        return new RecordingIdCreation(newRecording);
    }

    public async Task UpdateAsync(int id, UpdateRecordingRequest request, CancellationToken ct)
    {
        var recording = await _dbSet.FindAsync([id], ct);

        recording?.Date = request.Date;
        recording?.RecordedValue = request.RecordedValue;
    }

    public async Task DeleteAsync(int recordingId, CancellationToken ct)
    {
        var recording = await _dbSet.FindAsync([recordingId], ct);

        if (recording == null)
        {
            return;
        }

        _dbSet.Remove(recording);
    }
}
