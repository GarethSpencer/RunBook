using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using RepositoryLayer.Infrastructure.Generic;
using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Infrastructure;

public sealed class TrackedExerciseRepository(RunBookDbContext dbContext) : EFRepository<TrackedExercise>(dbContext), ITrackedExerciseRepository
{
    public async Task<TrackedExerciseResult?> GetByIdAsync(int trackedExerciseId, Guid userId, CancellationToken ct)
    {
        var trackedExercise = await _dbSet.FirstOrDefaultAsync(te => te.TrackedExerciseId == trackedExerciseId && te.UserId == userId, ct);

        if (trackedExercise == null)
        {
            return null;
        }

        return new TrackedExerciseResult
        {
            TrackedExerciseId = trackedExercise.TrackedExerciseId,
            ExerciseTypeId = trackedExercise.ExerciseTypeId,
            Date = trackedExercise.Date,
            Duration = trackedExercise.Duration
        };
    }

    public async Task<IEnumerable<TrackedExerciseResult>> GetByDayAsync(DateOnly date, Guid userId, CancellationToken ct)
    {
        var trackedExercises = await _dbSet.Where(te => te.Date == date && te.UserId == userId).ToListAsync(ct);

        return trackedExercises.Select(te => new TrackedExerciseResult
        {
            TrackedExerciseId = te.TrackedExerciseId,
            ExerciseTypeId = te.ExerciseTypeId,
            Date = te.Date,
            Duration = te.Duration
        });
    }

    public async Task<IEnumerable<TrackedExerciseResult>> GetByMonthAsync(DateOnly monthDate, Guid userId, CancellationToken ct)
    {
        var trackedExercises = await _dbSet.Where(te => te.Date.Month == monthDate.Month && te.Date.Year == monthDate.Year && te.UserId == userId).ToListAsync(ct);

        return trackedExercises.Select(te => new TrackedExerciseResult
        {
            TrackedExerciseId = te.TrackedExerciseId,
            ExerciseTypeId = te.ExerciseTypeId,
            Date = te.Date,
            Duration = te.Duration
        });
    }

    public async Task<IEnumerable<TrackedExerciseResult>> GetByYearAsync(int year, Guid userId, CancellationToken ct)
    {
        var trackedExercises = await _dbSet.Where(te => te.Date.Year == year && te.UserId == userId).ToListAsync(ct);

        return trackedExercises.Select(te => new TrackedExerciseResult
        {
            TrackedExerciseId = te.TrackedExerciseId,
            ExerciseTypeId = te.ExerciseTypeId,
            Date = te.Date,
            Duration = te.Duration
        });
    }

    public async Task<IIdCreation> CreateAsync(CreateTrackedExerciseRequest request, Guid userId, CancellationToken ct)
    {
        var newTrackedExercise = new TrackedExercise
        {
            UserId = userId,
            ExerciseTypeId = request.ExerciseTypeId,
            Date = request.Date,
            Duration = request.Duration
        };

        await _dbSet.AddAsync(newTrackedExercise, ct);
        return new TrackedExerciseIdCreation(newTrackedExercise);
    }

    public async Task UpdateAsync(int id, UpdateTrackedExerciseRequest request, CancellationToken ct)
    {
        var trackedExercise = await _dbSet.FindAsync([id], ct);

        trackedExercise?.ExerciseTypeId = request.ExerciseTypeId;
        trackedExercise?.Date = request.Date;
        trackedExercise?.Duration = request.Duration;
    }

    public async Task DeleteAsync(int trackedExerciseId, CancellationToken ct)
    {
        var trackedExercise = await _dbSet.FindAsync([trackedExerciseId], ct);

        if (trackedExercise == null)
        {
            return;
        }

        _dbSet.Remove(trackedExercise);
    }
}
