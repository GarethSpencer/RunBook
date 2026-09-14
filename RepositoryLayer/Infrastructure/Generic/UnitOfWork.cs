using DAL.Data;
using RepositoryLayer.Abstractions.Generic;

namespace RepositoryLayer.Infrastructure.Generic;

public class UnitOfWork(RunBookDbContext dbContext) : IUnitOfWork
{
    protected readonly RunBookDbContext _dbContext = dbContext;

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _dbContext.SaveChangesAsync(ct);
    }
}
