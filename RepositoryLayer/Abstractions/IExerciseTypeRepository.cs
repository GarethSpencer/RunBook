using DAL.Entities;
using RepositoryLayer.Abstractions.Generic;
using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Abstractions;

public interface IExerciseTypeRepository : IEFRepository<ExerciseType>
{
    Task<IEnumerable<ExerciseTypeResult>> GetAllAsync(CancellationToken ct);
    Task<ExerciseTypeResult?> GetDetailsByIdAsync(int id, CancellationToken ct);
    Task<IIdCreation> CreateAsync(CreateExerciseTypeRequest request, CancellationToken ct);
    Task UpdateAsync(int id, UpdateExerciseTypeRequest request, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}
