using Microsoft.EntityFrameworkCore;
using Npgsql;
using Storage.API.Data;

namespace Storage.API.Configuration;

public static class ConfigureMigrations
{
    public static async Task TryMigrateDatabase(this WebApplication app)
    {
        try
        {
            await using var scope = app.Services.CreateAsyncScope();
            var sp = scope.ServiceProvider;
            var db = sp.GetRequiredService<StorageDbContext>();
    
            await db.Database.MigrateAsync();

            await using var conn = (NpgsqlConnection)db.Database.GetDbConnection();
            await conn.OpenAsync();
            await conn.ReloadTypesAsync();
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "Error while migrating the database");
            Environment.Exit(-1);
        }
    }
}