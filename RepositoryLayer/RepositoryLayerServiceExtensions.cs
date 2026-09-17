using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Abstractions;
using RepositoryLayer.Abstractions.Generic;
using RepositoryLayer.Infrastructure;
using RepositoryLayer.Infrastructure.Generic;

namespace RepositoryLayer;

public static class RepositoryLayerServiceExtensions
{
    public static IServiceCollection AddRepositoryLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RunBookDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("Default"), sql => _ = sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

        services.AddScoped(typeof(IEFRepository<>), typeof(EFRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExerciseTypeRepository, ExerciseTypeRepository>();
        services.AddScoped<ITrackedExerciseRepository, TrackedExerciseRepository>();
        services.AddScoped<IDailyExerciseRepository, DailyExerciseRepository>();
        services.AddScoped<IMedicationRepository, MedicationRepository>();
        services.AddScoped<IStepsRepository, StepsRepository>();

        return services;
    }
}
