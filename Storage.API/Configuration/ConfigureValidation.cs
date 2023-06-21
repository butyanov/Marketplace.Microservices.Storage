using FluentValidation;
using FluentValidation.AspNetCore;

namespace Storage.API.Configuration;

public static class ConfigureValidation
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }
}