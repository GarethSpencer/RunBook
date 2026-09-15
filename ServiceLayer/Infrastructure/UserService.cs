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
    private readonly ITokenData _tokenData = tokenData;
    private readonly ILogger<UserService> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CommonResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request, CancellationToken ct)
    {
        if (!_tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(_logger);
        }

        var callingUserId = _tokenData.UserId!.Value;
        var isAdmin = await _userRepository.IsUserAdminAsync(callingUserId, ct);
        if (!isAdmin && callingUserId != userId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "You do not have permission to update this user."
            }.WithResponseLog(_logger, callingUserId);
        }

        var userToUpdate = await _userRepository.GetDetailsByIdAsync(userId, ct);
        if (userToUpdate == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "User not found."
            }.WithResponseLog(_logger, callingUserId);
        }

        var userToUpdateIsAdmin = await _userRepository.IsUserAdminAsync(userId, ct);
        if (userToUpdateIsAdmin && userId != callingUserId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "You do not have permission to update this user."
            }.WithResponseLog(_logger, callingUserId);
        }

        var userDeactivated = false;
        if (userToUpdate.Active && !request.Active)
        {
            userDeactivated = true;
        }

        await _userRepository.UpdateAsync(userId, callingUserId, request, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = userDeactivated ? "User deactivated successfully." : "User updated successfully."
        }.WithResponseLog(_logger, callingUserId, $"User [{userId}] {(userDeactivated ? "deactivated" : "updated")} successfully.");
    }

    public async Task<CommonResponse> GetCurrentUserAsync(CancellationToken ct)
    {
        if (!_tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(_logger);
        }

        var callingUserId = _tokenData.UserId!.Value;
        var user = await _userRepository.GetDetailsByIdAsync(callingUserId, ct);
        if (user == null)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "User was not found."
            }.WithResponseLog(_logger, callingUserId);
        }

        return new GetUserDetailedResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "User returned successfully.",
            User = user
        }.WithResponseLog(_logger, callingUserId);
    }

    public async Task<CommonResponse> DeleteUserAsync(Guid userId, CancellationToken ct)
    {
        if (!_tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(_logger);
        }

        var callingUserId = _tokenData.UserId!.Value;
        var userExists = await _userRepository.ExistsAsync(x => x.UserId == userId, ct);
        if (!userExists)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "User not found."
            }.WithResponseLog(_logger, callingUserId);
        }

        var isAdmin = await _userRepository.IsUserAdminAsync(callingUserId, ct);
        if (!isAdmin && callingUserId != userId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You cannot delete other users."
            }.WithResponseLog(_logger, callingUserId);
        }

        var isUserToDeleteAdmin = await _userRepository.IsUserAdminAsync(userId, ct);
        if (isUserToDeleteAdmin && callingUserId != userId)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You cannot delete other admins."
            }.WithResponseLog(_logger, callingUserId);
        }

        await _userRepository.DeleteAsync(userId, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return new CommonResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "User deleted successfully."
        }.WithResponseLog(_logger, callingUserId, $"User [{userId}] deleted successfully.");
    }

    public async Task<CommonResponse> GetAllUsersAsync(PaginationBaseRequest request, CancellationToken ct)
    {
        if (!_tokenData.UserId.HasValue)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Unauthorized."
            }.WithResponseLog(_logger);
        }

        var callingUserId = _tokenData.UserId!.Value;
        var isAdmin = await _userRepository.IsUserAdminAsync(callingUserId, ct);
        if (!isAdmin)
        {
            return new CommonResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "You are not an admin."
            }.WithResponseLog(_logger, callingUserId);
        }

        var (users, totalCount) = await _userRepository.GetAllAsync(request, ct);

        return new GetUsersDetailedResponse
        {
            StatusCode = totalCount > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            Message = "Users returned successfully.",
            Users = users,
            TotalCount = totalCount,
        }.WithResponseLog(_logger, callingUserId);
    }
}
