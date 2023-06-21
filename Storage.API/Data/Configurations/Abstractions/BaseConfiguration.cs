﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.API.Models.Abstractions;
using TrashGrounds.Template.Database.Postgres.Configurations.Abstractions;

namespace Storage.API.Data.Configurations.Abstractions;

public abstract class BaseConfiguration<TEntity> : DependencyInjectedEntityConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public sealed override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureBase(builder);
        ConfigureChild(builder);
    }
    
    public abstract void ConfigureChild(EntityTypeBuilder<TEntity> typeBuilder);

    public static void ConfigureBase(EntityTypeBuilder<TEntity> typeBuilder)
    {
        typeBuilder.HasKey(x => x.Id);

        typeBuilder.Property(x => x.Id)
            .HasDefaultValueSql("gen_random_uuid()");
    }
      
}