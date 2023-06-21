using MediatR;
using Storage.API.Infrastructure.Routing;

namespace Storage.API.Features.Images.DeleteImage;

public class DeleteImageEndpoint : IEndpoint
{

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete(
                "/{id:guid}",
                async (Guid fileId, IMediator mediator)
                    => Results.Ok(
                        await mediator.Send(
                            new DeleteImageCommand(fileId))))
            .RequireAuthorization("User")
            .WithName("Удалить изображение по идентификатору");
    }
}
