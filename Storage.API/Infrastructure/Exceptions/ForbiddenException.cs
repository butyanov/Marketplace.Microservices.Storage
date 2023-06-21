using System.Net;

namespace Storage.API.Infrastructure.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException(string message) : base(message, (int)HttpStatusCode.Forbidden)
    {
    }
}