using DAL.Data.Configuration;
using DAL.Entities;
using DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class RunBookDbContext(DbContextOptions<RunBookDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ExerciseType> ExerciseTypes => Set<ExerciseType>();
    public DbSet<TrackedExercise> TrackedExercises => Set<TrackedExercise>();
    public DbSet<DailyExercise> DailyExercises => Set<DailyExercise>();
    public DbSet<Steps> Steps => Set<Steps>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<Weight> Weights => Set<Weight>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RunBookDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields(SeedConstants.AdminId);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        SetAuditFields(userId);

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields(Guid userId)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().Where(e => e.State == EntityState.Added).ToList())
        {
            entry.Entity.CreatedOn = now;
            entry.Entity.CreatedBy = userId;
        }

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().Where(e => e.State == EntityState.Modified).ToList())
        {
            entry.Entity.UpdatedOn = now;
            entry.Entity.UpdatedBy = userId;
        }
    }
}
