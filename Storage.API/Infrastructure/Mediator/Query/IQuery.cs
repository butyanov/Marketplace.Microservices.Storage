using MediatR;

namespace Storage.API.Infrastructure.Mediator.Query;

public interface IQuery<out T> : IRequest<T>
{
}