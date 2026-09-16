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

public class TrackedExerciseService(ITokenData tokenData,
    ILogger<TrackedExerciseService> logger,
    ITrackedExerciseRepository trackedExerciseRepository,
    IExerciseTypeRepository exerciseTypeRepository,
    IUnitOfWork unitOfWork) : ITrackedExerciseService
{
    public async Task<CommonResponse> GetMyTrackedExercisesByDayAsync(DateOnly date, CancellationToken ct)
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
        var trackedExercises = await trackedExerciseRepository.GetByDayAsync(date, callingUserId, ct);

        return new GetTrackedExercisesResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Tracked exercises returned successfully.",
            TrackedExercises = trackedExercises
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> GetMyTrackedExercisesByMonthAsync(DateOnly monthDate, CancellationToken ct)
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
        var trackedExercises = await trackedExerciseRepository.GetByMonthAsync(monthDate, callingUserId, ct);

        return new GetTrackedExercisesResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Tracked exercises returned successfully.",
            TrackedExercises = trackedExercises
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> GetMyTrackedExercisesByYearAsync(int year, CancellationToken ct)
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
        var trackedExercises = await trackedExerciseRepository.GetByYearAsync(year, callingUserId, ct);

        return new GetTrackedExercisesResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Tracked exercises returned successfully.",
            TrackedExercises = trackedExercises
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> CreateMyTrackedExerciseAsync(CreateTrackedExerciseRequest request, CancellationToken ct)
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
        var exerciseType = await exerciseTypeRepository.ExistsAsync(x => x.ExerciseTypeId == request.ExerciseTypeId, ct);
        if (!exerciseType)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid exercise type."
            }.WithResponseLog(logger, callingUserId);
        }

        var createdTrackedExercise = await trackedExerciseRepository.CreateAsync(request, callingUserId, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CreateRecordingResponse
        {
            StatusCode = HttpStatusCode.Created,
            Message = $"Tracked exercise created successfully.",
            Id = createdTrackedExercise.Id
        }.WithResponseLog(logger, callingUserId, $"Tracked exercise [{createdTrackedExercise.Id}] created successfully.");
    }

    public async Task<CommonResponse> UpdateMyTrackedExerciseAsync(int trackedExerciseId, UpdateTrackedExerciseRequest request, CancellationToken ct)
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
        var trackedExerciseToUpdate = await trackedExerciseRepository.GetByIdAsync(trackedExerciseId, callingUserId, ct);
        if (trackedExerciseToUpdate == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Tracked exercise not found."
            }.WithResponseLog(logger, callingUserId);
        }

        var exerciseType = await exerciseTypeRepository.ExistsAsync(x => x.ExerciseTypeId == request.ExerciseTypeId, ct);
        if (!exerciseType)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid exercise type."
            }.WithResponseLog(logger, callingUserId);
        }

        await trackedExerciseRepository.UpdateAsync(trackedExerciseId, request, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Tracked exercise updated successfully."
        }.WithResponseLog(logger, callingUserId, $"Tracked exercise [{trackedExerciseId}] updated successfully.");
    }

    public async Task<CommonResponse> DeleteMyTrackedExerciseAsync(int trackedExerciseId, CancellationToken ct)
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
        var trackedExerciseToDelete = await trackedExerciseRepository.GetByIdAsync(trackedExerciseId, callingUserId, ct);
        if (trackedExerciseToDelete == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Tracked exercise not found."
            }.WithResponseLog(logger, callingUserId);
        }

        await trackedExerciseRepository.DeleteAsync(trackedExerciseId, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Tracked exercise deleted successfully."
        }.WithResponseLog(logger, callingUserId, $"Tracked exercise [{trackedExerciseId}] deleted successfully.");
    }
}
