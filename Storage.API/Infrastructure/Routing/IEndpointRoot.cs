namespace Storage.API.Infrastructure.Routing;

public interface IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints);
}