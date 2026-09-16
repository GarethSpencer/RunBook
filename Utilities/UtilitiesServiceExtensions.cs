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
        services.AddScoped<IValidator<CreateExerciseTypeRequest>, CreateExerciseTypeRequestValidator>();
        services.AddScoped<IValidator<UpdateExerciseTypeRequest>, UpdateExerciseTypeRequestValidator>();
        services.AddScoped<IValidator<CreateTrackedExerciseRequest>, CreateTrackedExerciseRequestValidator>();
        services.AddScoped<IValidator<UpdateTrackedExerciseRequest>, UpdateTrackedExerciseRequestValidator>();
        services.AddScoped<IValidator<DateOnly>, GetTrackedExerciseByDayRequestValidator>();
        services.AddScoped<IValidator<DateOnly>, GetTrackedExerciseByMonthRequestValidator>();
        services.AddScoped<IValidator<int>, GetTrackedExerciseByYearRequestValidator>();
    }
}
