using MediatR;
using Storage.API.Infrastructure.Routing;

namespace Storage.API.Features.Images.GetImage;

public class GetImageEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id:guid}",
            async (Guid fileId, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetImageQuery(fileId));
                return Results.File(
                    response.Stream,
                    response.ContentType,
                    response.FileId.ToString());
            }).WithName("Получить картинку по идентификатору");;
    }
}
