using DAL.Data;
using RepositoryLayer.Abstractions.Generic;
using Utilities.Models.Token;

namespace RepositoryLayer.Infrastructure.Generic;

public class UnitOfWork(RunBookDbContext dbContext, ITokenData tokenData) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        if (tokenData?.UserId != null)
        {
            return await dbContext.SaveChangesAsync(tokenData.UserId.Value, ct);
        }

        return await dbContext.SaveChangesAsync(ct);
    }
}
