using Utilities.Models.Requests;
using Utilities.Models.Requests.Generic;
using Utilities.Models.Responses.Generic;

namespace ServiceLayer.Abstractions;

public interface IUserService
{
    Task<CommonResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request, CancellationToken ct);

    Task<CommonResponse> GetCurrentUserAsync(CancellationToken ct);

    Task<CommonResponse> DeleteUserAsync(Guid userId, CancellationToken ct);

    Task<CommonResponse> GetAllUsersAsync(PaginationBaseRequest request, CancellationToken ct);
}
