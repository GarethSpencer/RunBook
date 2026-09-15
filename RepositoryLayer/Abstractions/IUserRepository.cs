using DAL.Entities;
using RepositoryLayer.Abstractions.Generic;
using Utilities.Models.Requests.Generic;
using Utilities.Models.Requests;
using Utilities.Models.Results;

namespace RepositoryLayer.Abstractions;

public interface IUserRepository : IEFRepository<User>
{
    Task<(IEnumerable<UserAdminResult>, int)> GetAllAsync(PaginationBaseRequest request, CancellationToken ct);
    Task<UserDetailedResult?> GetDetailsByIdAsync(Guid id, CancellationToken ct);
    Task<bool> IsUserAdminAsync(Guid userId, CancellationToken ct);
    Task<Guid> CreateAsync(CreateUserRequest createRequest, CancellationToken ct);
    Task DeleteAsync(Guid userId, CancellationToken ct);
    Task UpdateAsync(Guid userId, Guid callindUserId, UpdateUserRequest userRequest, CancellationToken ct);
    Task SetActiveAsync(Guid userId, CancellationToken ct);
    Task SetAuthIdAsync(Guid userId, string authId, CancellationToken ct);
    Task<IEnumerable<UserAdminResult>> GetAllLongTermInactiveAsync(int minimumDaysInactive, CancellationToken ct);
}
