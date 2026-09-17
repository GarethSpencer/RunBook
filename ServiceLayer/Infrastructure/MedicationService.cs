using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using ServiceLayer.Infrastructure.Generic;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure;

public class MedicationService(ITokenData tokenData,
    ILogger<MedicationService> logger,
    IMedicationRepository medicationRepository,
    IUnitOfWork unitOfWork) : RecordingService<IMedicationRepository>(tokenData, logger, medicationRepository, unitOfWork), IMedicationService { }
