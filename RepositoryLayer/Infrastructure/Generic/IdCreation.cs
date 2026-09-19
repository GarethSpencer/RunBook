using DAL.Entities;
using DAL.Entities.Abstractions;
using RepositoryLayer.Abstractions.Generic;

namespace RepositoryLayer.Infrastructure.Generic;

internal sealed class ExerciseTypeIdCreation(ExerciseType entity) : IIdCreation
{
    public int Id => entity.ExerciseTypeId;
}

internal sealed class TrackedExerciseIdCreation(TrackedExercise entity) : IIdCreation
{
    public int Id => entity.TrackedExerciseId;
}

internal sealed class RecordingIdCreation<TValue>(IRecording<TValue> entity) : IIdCreation
{
    public int Id => entity.RecordingId;
}
