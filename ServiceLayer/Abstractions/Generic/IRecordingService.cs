using Utilities.Models.Requests;
using Utilities.Models.Responses.Generic;

namespace ServiceLayer.Abstractions.Generic;

public interface IRecordingService
{
    Task<CommonResponse> GetMyRecordingByDayAsync(DateOnly date, CancellationToken ct);

    Task<CommonResponse> GetMyRecordingsByMonthAsync(DateOnly monthDate, CancellationToken ct);

    Task<CommonResponse> GetMyRecordingsByYearAsync(int year, CancellationToken ct);

    Task<CommonResponse> CreateMyRecordingAsync(CreateRecordingRequest request, CancellationToken ct);

    Task<CommonResponse> UpdateMyRecordingAsync(int recordingId, UpdateRecordingRequest request, CancellationToken ct);

    Task<CommonResponse> DeleteMyRecordingAsync(int recordingId, CancellationToken ct);
}
