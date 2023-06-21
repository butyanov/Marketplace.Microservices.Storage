using Storage.API.Services.Abstractions;

namespace Storage.API.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}