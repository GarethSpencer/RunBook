using DAL.Entities;
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

internal sealed class DailyExerciseIdCreation(DailyExercise entity) : IIdCreation
{
    public int Id => entity.DailyExerciseId;
}

internal sealed class StepsIdCreation(Steps entity) : IIdCreation
{
    public int Id => entity.StepsId;
}

internal sealed class MedicationIdCreation(Medication entity) : IIdCreation
{
    public int Id => entity.MedicationId;
}

internal sealed class WeightIdCreation(Weight entity) : IIdCreation
{
    public int Id => entity.WeightId;
}
