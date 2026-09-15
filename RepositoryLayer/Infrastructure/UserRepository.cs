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
        var query = _dbSet.OrderBy(u => u.PreferredName);

        var total = await query.CountAsync(ct);

        var results = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(u => new UserAdminResult
            {
                UserId = u.UserId,
                AuthId = u.AuthId,
                PreferredName = u.PreferredName,
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
            PreferredName = user.PreferredName,
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

    public async Task<UserDetailedResult?> GetDetailsByAuthIdAsync(Guid authId, CancellationToken ct)
    {
        var user = await _dbSet.FirstOrDefaultAsync(u => u.AuthId == authId, ct);

        if (user == null)
        {
            return null;
        }

        return new UserDetailedResult
        {
            UserId = user.UserId,
            AuthId = user.AuthId,
            PreferredName = user.PreferredName,
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
            AuthId = createRequest.AuthId,
            PreferredName = createRequest.PreferredName
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
            user.PreferredName = userRequest.PreferredName;
            user.Active = userRequest.Active;
            user.PrefersKg = userRequest.PrefersKg ?? user.PrefersKg;
            user.WeeklyExerciseGoal = userRequest.WeeklyExerciseGoal ?? user.WeeklyExerciseGoal;
            user.DailyStepGoal = userRequest.DailyStepGoal ?? user.DailyStepGoal;
            user.TargetWeight = userRequest.TargetWeight ?? user.TargetWeight;
        }
        else
        {
            user?.Active = userRequest.Active;
        }
    }

    public async Task SetActiveAsync(Guid userId, CancellationToken ct)
    {
        var user = await _dbSet.FindAsync([userId], ct);
        user?.Active = true;
    }

    public async Task SetAdminAsync(Guid userId, bool isAdmin, CancellationToken ct)
    {
        var user = await _dbSet.FindAsync([userId], ct);
        user?.Admin = isAdmin;
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
                PreferredName = u.PreferredName,
                UpdatedBy = u.UpdatedBy,
                UpdatedOn = u.UpdatedOn
            }).ToListAsync(ct);
    }
}
