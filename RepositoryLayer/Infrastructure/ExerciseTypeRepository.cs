using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using RepositoryLayer.Infrastructure.Generic;
using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Infrastructure;

public sealed class ExerciseTypeRepository(RunBookDbContext dbContext) : EFRepository<ExerciseType>(dbContext), IExerciseTypeRepository
{
    public async Task<IEnumerable<ExerciseTypeResult>> GetAllAsync(CancellationToken ct)
    {
        var query = _dbSet.OrderBy(u => u.Name);

        var results = await query
            .Select(u => new ExerciseTypeResult
            {
                ExerciseTypeId = u.ExerciseTypeId,
                Name = u.Name,
                Intensity = u.Intensity,
                CaloriesPerMinute = u.CaloriesPerMinute
            })
            .ToListAsync(ct);

        return results;
    }

    public async Task<ExerciseTypeResult?> GetDetailsByIdAsync(int id, CancellationToken ct)
    {
        var exerciseType = await _dbSet.FirstOrDefaultAsync(e => e.ExerciseTypeId == id, ct);

        if (exerciseType == null)
        {
            return null;
        }

        return new ExerciseTypeResult
        {
            ExerciseTypeId = exerciseType.ExerciseTypeId,
            Name = exerciseType.Name,
            Intensity = exerciseType.Intensity,
            CaloriesPerMinute = exerciseType.CaloriesPerMinute
        };
    }

    public async Task<IIdCreation> CreateAsync(CreateExerciseTypeRequest request, CancellationToken ct)
    {
        var newExerciseType = new ExerciseType
        {
            Name = request.Name,
            Intensity = request.Intensity,
            CaloriesPerMinute = request.CaloriesPerMinute
        };

        await _dbSet.AddAsync(newExerciseType, ct);
        return new ExerciseTypeIdCreation(newExerciseType);
    }

    public async Task UpdateAsync(int id, UpdateExerciseTypeRequest request, CancellationToken ct)
    {
        var exerciseType = await _dbSet.FindAsync([id], ct);

        exerciseType?.Name = request.Name;
        exerciseType?.Intensity = request.Intensity;
        exerciseType?.CaloriesPerMinute = request.CaloriesPerMinute;
    }

    public async Task DeleteAsync(int exerciseTypeId, CancellationToken ct)
    {
        var exerciseType = await _dbSet.FindAsync([exerciseTypeId], ct);

        if (exerciseType == null)
        {
            return;
        }

        _dbSet.Remove(exerciseType);
    }
}
