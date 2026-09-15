using Microsoft.Extensions.Logging;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using ServiceLayer.Abstractions;
using Utilities.Models.Requests;
using Utilities.Models.Token;

namespace ServiceLayer.Infrastructure;

public class AuthService(ITokenData tokenData, IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<AuthService> logger) : IAuthService
{
    public async Task<Guid?> ResolveUserAsync(Guid authId, string preferredName, CancellationToken ct)
    {
        var user = await userRepository.GetDetailsByAuthIdAsync(authId, ct);

        if (user is not null)
        {
            if (!user.Active)
            {
                await userRepository.SetActiveAsync(user.UserId, ct);
                await unitOfWork.SaveChangesAsync(ct);
                logger.LogInformation("[ResolveUserAsync] user [{UserId}] reactivated as part of auth resolution process.", user.UserId);
            }

            if (user.Admin != tokenData.IsAdmin)
            {
                await userRepository.SetAdminAsync(user.UserId, tokenData.IsAdmin, ct);
                await unitOfWork.SaveChangesAsync(ct);
                logger.LogInformation("[ResolveUserAsync] user [{UserId}] admin status updated as part of auth resolution process.", user.UserId);
            }

            return user.UserId;
        }

        var newUser = new CreateUserRequest
        {
            AuthId = authId,
            PreferredName = preferredName.Split('@')[0]
        };

        var newUserId = await userRepository.CreateAsync(newUser, ct);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("[ResolveUserAsync] resolved for AuthId [{authId}] and new user [{newUserId}] created.", authId, newUserId);

        return newUserId;
    }
}
