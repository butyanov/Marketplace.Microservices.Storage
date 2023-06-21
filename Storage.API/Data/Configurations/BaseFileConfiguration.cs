using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.API.Data.Configurations.Abstractions;
using Storage.API.Models.Abstractions;

namespace Storage.API.Data.Configurations;

public abstract class BaseFileConfiguration<TEntity> : BaseConfiguration<TEntity>
    where TEntity : BaseFile
{
    public override void ConfigureChild(EntityTypeBuilder<TEntity> typeBuilder)
    {
        typeBuilder.Property(file => file.UploadDate)
            .HasDefaultValueSql("NOW()");
    }
}