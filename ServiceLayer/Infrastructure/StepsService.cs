using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using ServiceLayer.Infrastructure.Generic;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure;

public class StepsService(ITokenData tokenData,
    ILogger<StepsService> logger,
    IStepsRepository stepsRepository,
    IUnitOfWork unitOfWork) : RecordingService<IStepsRepository>(tokenData, logger, stepsRepository, unitOfWork), IStepsService { }
