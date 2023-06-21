namespace Storage.API.Services.Abstractions;

public interface IUserService
{
    public Guid GetUserIdOrThrow();
}