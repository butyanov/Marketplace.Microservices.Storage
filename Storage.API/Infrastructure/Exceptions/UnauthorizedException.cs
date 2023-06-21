using System.Net;

namespace Storage.API.Infrastructure.Exceptions;

public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message) : base(message, (int)HttpStatusCode.Unauthorized)
    {
    }
}