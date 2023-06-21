using Storage.API.Services;
using Storage.API.Services.Abstractions;

namespace Storage.API.Configuration;

public static class ConfigureHelperServices
{
    public static IServiceCollection AddHelperServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}