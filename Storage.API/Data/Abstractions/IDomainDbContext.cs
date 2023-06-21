using Microsoft.EntityFrameworkCore;
using Storage.API.Models;

namespace Storage.API.Data.Abstractions;

public interface IDomainDbContext
{
    public DbSet<ImageFile> ImageFiles { get; set; }

    public Task<bool> SaveEntitiesAsync();
}