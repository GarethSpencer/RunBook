using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Models.Requests;
using Utilities.Models.Requests.Generic;
using Utilities.Models.Token;
using Utilities.Validators;

namespace Utilities;

public static class UtilitiesServiceExtensions
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ITokenData, TokenData>();
        services.RegisterValidators();

        return services;
    }

    private static void RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<PaginationBaseRequest>, PaginationBaseRequestValidator>();
        services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();
    }
}
