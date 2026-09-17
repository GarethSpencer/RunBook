using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using ServiceLayer.Abstractions.Generic;
using System.Net;
using Utilities.Helpers;
using Utilities.Models.Requests;
using Utilities.Models.Responses;
using Utilities.Models.Responses.Generic;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure.Generic;

public class RecordingService<TRecordingRepository>(ITokenData tokenData,
    ILogger<RecordingService<TRecordingRepository>> logger,
    TRecordingRepository recordingRepository,
    IUnitOfWork unitOfWork) : IRecordingService where TRecordingRepository : IRecordingRepository
{
    public async Task<CommonResponse> GetMyRecordingByDayAsync(DateOnly date, CancellationToken ct)
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
        var recording = await recordingRepository.GetByDayAsync(date, callingUserId, ct);

        return new GetRecordingResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Recordings returned successfully.",
            Recording = recording
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> GetMyRecordingsByMonthAsync(DateOnly monthDate, CancellationToken ct)
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
        var recordings = await recordingRepository.GetByMonthAsync(monthDate, callingUserId, ct);

        return new GetRecordingsResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Recordings returned successfully.",
            Recordings = recordings
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> GetMyRecordingsByYearAsync(int year, CancellationToken ct)
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
        var recordings = await recordingRepository.GetByYearAsync(year, callingUserId, ct);

        return new GetRecordingsResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Recordings returned successfully.",
            Recordings = recordings
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> CreateMyRecordingAsync(CreateRecordingRequest request, CancellationToken ct)
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

        var existingRecording = await recordingRepository.GetByDayAsync(request.Date, callingUserId, ct);
        if (existingRecording != null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Conflict,
                Message = "A recording for this date already exists."
            }.WithResponseLog(logger, callingUserId);
        }

        var createdRecording = await recordingRepository.CreateAsync(request, callingUserId, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CreateRecordingResponse
        {
            StatusCode = HttpStatusCode.Created,
            Message = "Recording created successfully.",
            Id = createdRecording.Id
        }.WithResponseLog(logger, callingUserId, $"Recording [{createdRecording.Id}] created successfully.");
    }

    public async Task<CommonResponse> UpdateMyRecordingAsync(int recordingId, UpdateRecordingRequest request, CancellationToken ct)
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
        var recordingToUpdate = await recordingRepository.GetByIdAsync(recordingId, callingUserId, ct);
        if (recordingToUpdate == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Recording not found."
            }.WithResponseLog(logger, callingUserId);
        }

        await recordingRepository.UpdateAsync(recordingId, request, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Recording updated successfully."
        }.WithResponseLog(logger, callingUserId, $"Recording [{recordingId}] updated successfully.");
    }

    public async Task<CommonResponse> DeleteMyRecordingAsync(int recordingId, CancellationToken ct)
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
        var recordingToDelete = await recordingRepository.GetByIdAsync(recordingId, callingUserId, ct);
        if (recordingToDelete == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Recording not found."
            }.WithResponseLog(logger, callingUserId);
        }

        await recordingRepository.DeleteAsync(recordingId, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Recording deleted successfully."
        }.WithResponseLog(logger, callingUserId, $"Recording [{recordingId}] deleted successfully.");
    }
}
