using DAL.Entities;
using RepositoryLayer.Abstractions.Generic;

namespace RepositoryLayer.Abstractions;

public interface IDailyExerciseRepository : IEFRepository<DailyExercise>, IRecordingRepository<bool> { }
