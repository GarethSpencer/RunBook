using Microsoft.Extensions.DependencyInjection;

namespace Utilities;

public static class UtilitiesServiceExtensions
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.RegisterValidators();

        return services;
    }

    private static void RegisterValidators(this IServiceCollection services)
    {

    }
}
