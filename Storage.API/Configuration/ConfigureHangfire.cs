using Hangfire;
using Hangfire.PostgreSql;
using Storage.API.HangfireJobs;

namespace Storage.API.Configuration;

public static class ConfigureHangfire
{
    public static IServiceCollection AddHangfireConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(configuration.GetConnectionString("HangfireConnection")));

        services.AddHangfireServer(opt =>
        {
            opt.Queues = new[] {"collect-garbage-images", "default"};
            opt.WorkerCount = 1;
        });
        
        services.AddTransient<FilesGcJob>();
        
        return services;
    }
    public static void AddHangfireJobs()
    {
        RecurringJob.AddOrUpdate<FilesGcJob>(FilesGcJob.Id, service =>
            service.CollectGarbageImages(1000, 0), Cron.Daily);
    }
}