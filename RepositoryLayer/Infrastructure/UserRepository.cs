using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Infrastructure.Generic;
using Utilities.Models.Requests;
using Utilities.Models.Requests.Generic;
using Utilities.Models.Results;

namespace RepositoryLayer.Infrastructure;

public sealed class UserRepository(RunBookDbContext dbContext) : EFRepository<User>(dbContext), IUserRepository
{
    public async Task<(IEnumerable<UserAdminResult>, int)> GetAllAsync(PaginationBaseRequest request, CancellationToken ct)
    {
        var query = _dbSet.OrderBy(u => u.DisplayName);

        var total = await query.CountAsync(ct);

        var results = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(u => new UserAdminResult
            {
                UserId = u.UserId,
                AuthId = u.AuthId,
                DisplayName = u.DisplayName,
                Active = u.Active,
                Admin = u.Admin,
                CreatedOn = u.CreatedOn,
                CreatedBy = u.CreatedBy,
                UpdatedOn = u.UpdatedOn,
                UpdatedBy = u.UpdatedBy
            })
            .ToListAsync(ct);

        return (results, total);
    }

    public async Task<UserDetailedResult?> GetDetailsByIdAsync(Guid id, CancellationToken ct)
    {
        var user = await _dbSet.FirstOrDefaultAsync(u => u.UserId == id, ct);

        if (user == null)
        {
            return null;
        }

        return new UserDetailedResult
        {
            UserId = user.UserId,
            AuthId = user.AuthId,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Active = user.Active,
            Admin = user.Admin,
            PrefersKg = user.PrefersKg,
            WeeklyExerciseGoal = user.WeeklyExerciseGoal,
            DailyStepGoal = user.DailyStepGoal,
            TargetWeight = user.TargetWeight,
            CreatedOn = user.CreatedOn,
            CreatedBy = user.CreatedBy,
            UpdatedOn = user.UpdatedOn,
            UpdatedBy = user.UpdatedBy
        };
    }

    public async Task<bool> IsUserAdminAsync(Guid userId, CancellationToken ct)
    {
        var user = await _dbSet.FindAsync([userId], ct);
        return user != null && user.Admin;
    }

    public async Task<Guid> CreateAsync(CreateUserRequest createRequest, CancellationToken ct)
    {
        var newUser = new User
        {
            DisplayName = createRequest.DisplayName,
            Email = createRequest.Email,
            Admin = createRequest.Admin,
            Active = true,
            PrefersKg = true
        };

        await _dbSet.AddAsync(newUser, ct);
        return newUser.UserId;
    }

    public async Task DeleteAsync(Guid userId, CancellationToken ct)
    {
        var user = await _dbSet.FindAsync([userId], ct);

        if (user == null)
        {
            return;
        }

        _dbSet.Remove(user);
    }

    public async Task UpdateAsync(Guid userId, Guid callingUserId, UpdateUserRequest userRequest, CancellationToken ct)
    {
        var user = await _dbSet.FindAsync([userId], ct);

        if (user != null && user.UserId == callingUserId)
        {
            user.DisplayName = userRequest.DisplayName;
            user.Admin = userRequest.Admin;
            user.Active = userRequest.Active;
            user.PrefersKg = userRequest.PrefersKg ?? user.PrefersKg;
            user.WeeklyExerciseGoal = userRequest.WeeklyExerciseGoal ?? user.WeeklyExerciseGoal;
            user.DailyStepGoal = userRequest.DailyStepGoal ?? user.DailyStepGoal;
            user.TargetWeight = userRequest.TargetWeight ?? user.TargetWeight;
        }
        else if (user != null)
        {
            user.Admin = userRequest.Admin;
            user.Active = userRequest.Active;
        }
    }

    public async Task SetActiveAsync(Guid userId, CancellationToken ct)
    {
        var user = await _dbSet.FindAsync([userId], ct);
        user?.Active = true;
    }

    public async Task SetAuthIdAsync(Guid userId, string authId, CancellationToken ct)
    {
        var user = await _dbSet.FindAsync([userId], ct);
        user?.AuthId = authId;
    }

    public async Task<IEnumerable<UserAdminResult>> GetAllLongTermInactiveAsync(int minimumDaysInactive, CancellationToken ct)
    {
        var requiredDeactivationDate = DateTime.UtcNow.AddDays(-minimumDaysInactive);
        return await _dbSet.Where(x => !x.Active && x.UpdatedOn <= requiredDeactivationDate)
            .Select(u => new UserAdminResult
            {
                UserId = u.UserId,
                Active = u.Active,
                AuthId = u.AuthId,
                Admin = u.Admin,
                CreatedBy = u.CreatedBy,
                CreatedOn = u.CreatedOn,
                DisplayName = u.DisplayName,
                UpdatedBy = u.UpdatedBy,
                UpdatedOn = u.UpdatedOn
            }).ToListAsync(ct);
    }
}
