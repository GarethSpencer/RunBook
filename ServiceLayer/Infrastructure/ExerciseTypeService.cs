using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using System.Net;
using Utilities.Helpers;
using Utilities.Models.Requests;
using Utilities.Models.Responses;
using Utilities.Models.Responses.Generic;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure;

public class ExerciseTypeService(ITokenData tokenData,
    ILogger<ExerciseTypeService> logger,
    IExerciseTypeRepository exerciseTypeRepository,
    IUnitOfWork unitOfWork) : IExerciseTypeService
{
    public async Task<CommonResponse> GetExerciseTypesAsync(CancellationToken ct)
    {
        if (!tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(logger);
        }

        var callingUserId = tokenData.UserId.Value;
        var exerciseTypes = await exerciseTypeRepository.GetAllAsync(ct);

        return new GetExerciseTypesResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Exercise types returned successfully.",
            ExerciseTypes = exerciseTypes
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> CreateExerciseTypeAsync(CreateExerciseTypeRequest request, CancellationToken ct)
    {
        if (!tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(logger);
        }

        var callingUserId = tokenData.UserId.Value;
        if (!tokenData.IsAdmin)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You cannot create exercise types."
            }.WithResponseLog(logger, callingUserId);
        }

        var nameExists = await exerciseTypeRepository.ExistsAsync(x => x.Name == request.Name, ct);
        if (nameExists)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Conflict,
                Message = $"Exercise type with name {request.Name} already exists."
            }.WithResponseLog(logger, callingUserId);
        }

        var createdExerciseType = await exerciseTypeRepository.CreateAsync(request, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CreateExerciseTypeResponse
        {
            StatusCode = HttpStatusCode.Created,
            Message = $"Exercise Type with name {request.Name} created successfully.",
            ExerciseTypeId = createdExerciseType.Id
        }.WithResponseLog(logger, callingUserId, $"Exercise Type [{createdExerciseType.Id}] created successfully with name [{request.Name}].");
    }

    public async Task<CommonResponse> UpdateExerciseTypeAsync(int exerciseTypeId, UpdateExerciseTypeRequest request, CancellationToken ct)
    {
        if (!tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(logger);
        }

        var callingUserId = tokenData.UserId.Value;
        if (!tokenData.IsAdmin)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You cannot update exercise types."
            }.WithResponseLog(logger, callingUserId);
        }

        var exerciseTypeToUpdate = await exerciseTypeRepository.GetDetailsByIdAsync(exerciseTypeId, ct);
        if (exerciseTypeToUpdate == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Exercise type not found."
            }.WithResponseLog(logger, callingUserId);
        }

        await exerciseTypeRepository.UpdateAsync(exerciseTypeId, request, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Exercise type updated successfully."
        }.WithResponseLog(logger, callingUserId, $"Exercise type [{exerciseTypeId}] updated successfully.");
    }

    public async Task<CommonResponse> DeleteExerciseTypeAsync(int exerciseTypeId, CancellationToken ct)
    {
        if (!tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(logger);
        }

        var callingUserId = tokenData.UserId!.Value;
        if (!tokenData.IsAdmin)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You cannot delete exercise types."
            }.WithResponseLog(logger, callingUserId);
        }

        var exerciseTypeExists = await exerciseTypeRepository.ExistsAsync(x => x.ExerciseTypeId == exerciseTypeId, ct);
        if (!exerciseTypeExists)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Exercise type not found."
            }.WithResponseLog(logger, callingUserId);
        }

        await exerciseTypeRepository.DeleteAsync(exerciseTypeId, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Exercise type deleted successfully."
        }.WithResponseLog(logger, callingUserId, $"Exercise type [{exerciseTypeId}] deleted successfully.");
    }
}
