using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using System.Net;
using Utilities.Helpers;
using Utilities.Models.Requests;
using Utilities.Models.Requests.Generic;
using Utilities.Models.Responses;
using Utilities.Models.Responses.Generic;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure;

public class UserService(ITokenData tokenData,
    ILogger<UserService> logger,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<CommonResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request, CancellationToken ct)
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
        if (!tokenData.IsAdmin && callingUserId != userId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "You do not have permission to update this user."
            }.WithResponseLog(logger, callingUserId);
        }

        var userToUpdate = await userRepository.GetDetailsByIdAsync(userId, ct);
        if (userToUpdate == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "User not found."
            }.WithResponseLog(logger, callingUserId);
        }

        var userToUpdateIsAdmin = await userRepository.IsUserAdminAsync(userId, ct);
        if (userToUpdateIsAdmin && userId != callingUserId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "You do not have permission to update this user."
            }.WithResponseLog(logger, callingUserId);
        }

        var userDeactivated = false;
        if (userToUpdate.Active && !request.Active)
        {
            userDeactivated = true;
        }

        await userRepository.UpdateAsync(userId, callingUserId, request, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = userDeactivated ? "User deactivated successfully." : "User updated successfully."
        }.WithResponseLog(logger, callingUserId, $"User [{userId}] {(userDeactivated ? "deactivated" : "updated")} successfully.");
    }

    public async Task<CommonResponse> GetCurrentUserAsync(CancellationToken ct)
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
        var user = await userRepository.GetDetailsByIdAsync(callingUserId, ct);
        if (user == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "User was not found."
            }.WithResponseLog(logger, callingUserId);
        }

        return new GetUserDetailedResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "User returned successfully.",
            User = user
        }.WithResponseLog(logger, callingUserId);
    }

    public async Task<CommonResponse> DeleteUserAsync(Guid userId, CancellationToken ct)
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
        var userExists = await userRepository.ExistsAsync(x => x.UserId == userId, ct);
        if (!userExists)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "User not found."
            }.WithResponseLog(logger, callingUserId);
        }

        if (!tokenData.IsAdmin && callingUserId != userId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You cannot delete other users."
            }.WithResponseLog(logger, callingUserId);
        }

        var isUserToDeleteAdmin = await userRepository.IsUserAdminAsync(userId, ct);
        if (isUserToDeleteAdmin && callingUserId != userId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You cannot delete other admins."
            }.WithResponseLog(logger, callingUserId);
        }

        await userRepository.DeleteAsync(userId, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "User deleted successfully."
        }.WithResponseLog(logger, callingUserId, $"User [{userId}] deleted successfully.");
    }

    public async Task<CommonResponse> GetAllUsersAsync(PaginationBaseRequest request, CancellationToken ct)
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
                Message = "You are not an admin."
            }.WithResponseLog(logger, callingUserId);
        }

        var (users, totalCount) = await userRepository.GetAllAsync(request, ct);

        return new GetUsersDetailedResponse
        {
            StatusCode = totalCount > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            Message = "Users returned successfully.",
            Users = users,
            TotalCount = totalCount,
        }.WithResponseLog(logger, callingUserId);
    }
}
