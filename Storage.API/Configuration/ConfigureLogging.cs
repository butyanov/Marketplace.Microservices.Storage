using Serilog;

namespace Storage.API.Configuration;

public static class ConfigureLogging
{
    public static void AddCustomLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, _, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
            configuration.Enrich.FromLogContext();
            configuration.Enrich.WithProperty("Application", "TrashGrounds.User");
            configuration.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
            configuration.WriteTo.Console();
        });
    }
}