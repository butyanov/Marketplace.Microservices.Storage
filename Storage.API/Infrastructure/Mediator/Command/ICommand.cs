using MediatR;

namespace Storage.API.Infrastructure.Mediator.Command;

public interface ICommand<out T> : IRequest<T>
{
}