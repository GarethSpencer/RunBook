using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using ServiceLayer.Infrastructure.Generic;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure;

public class DailyExerciseService(ITokenData tokenData,
    ILogger<DailyExerciseService> logger,
    IDailyExerciseRepository dailyExerciseRepository,
    IUnitOfWork unitOfWork) : RecordingService<IDailyExerciseRepository>(tokenData, logger, dailyExerciseRepository, unitOfWork), IDailyExerciseService { }
