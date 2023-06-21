namespace Storage.API.Services.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}