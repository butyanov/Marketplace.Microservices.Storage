using Microsoft.EntityFrameworkCore;
using Storage.API.Data.Abstractions;
using Storage.API.Models;
using TrashGrounds.Template.Database.Postgres.Configurations.Abstractions;

namespace Storage.API.Data;

public class StorageDbContext : DbContext, IDomainDbContext
{
    private readonly IEnumerable<DependencyInjectedEntityConfiguration> _configurations;
    
    public DbSet<ImageFile> ImageFiles { get; set; }

    public StorageDbContext(DbContextOptions<StorageDbContext> options, 
        IEnumerable<DependencyInjectedEntityConfiguration> configurations) : base(options)
    {
        _configurations = configurations;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var configuration in _configurations)
            configuration.Configure(builder);
    }
    
    public async Task<bool> SaveEntitiesAsync()
    {
        await base.SaveChangesAsync();
        return true;
    }
}