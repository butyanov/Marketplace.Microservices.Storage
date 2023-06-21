using Hangfire;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Storage.API.Configuration;
using Storage.API.Data;
using Storage.API.Data.Abstractions;
using Storage.API.Infrastructure;
using Storage.API.Infrastructure.Routing;
using Storage.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddCustomLogging();

var services = builder.Services;

services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 20971520; // 20MB limit
});

services.AddDbContext<IDomainDbContext, StorageDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

services
    .AddEndpointsApiExplorer()
    .AddCustomSwaggerConfiguration(builder.Configuration);

services
    .AddCustomAuthentication(builder.Configuration)
    .AddCustomAuthorization();

services
    .AddHelperServices()
    .AddFluentValidation()
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>())
    .AddHangfireConfiguration(builder.Configuration);

var app = builder.Build();
await app.TryMigrateDatabaseAsync();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<DbTransactionsMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCustomEndpoints();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

ConfigureHangfire.AddHangfireJobs();

app.Run();