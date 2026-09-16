using DAL.Entities;
using RepositoryLayer.Abstractions.Generic;

namespace RepositoryLayer.Infrastructure.Generic;

internal sealed class IdCreation(ExerciseType entity) : IIdCreation
{
    public int Id => entity.ExerciseTypeId;
}
