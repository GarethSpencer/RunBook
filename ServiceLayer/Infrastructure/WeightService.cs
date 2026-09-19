using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using ServiceLayer.Infrastructure.Generic;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure;

public class WeightService(ITokenData tokenData,
    ILogger<WeightService> logger,
    IWeightRepository weightRepository,
    IUnitOfWork unitOfWork) : RecordingService<IWeightRepository, decimal>(tokenData, logger, weightRepository, unitOfWork), IWeightService
{ }
