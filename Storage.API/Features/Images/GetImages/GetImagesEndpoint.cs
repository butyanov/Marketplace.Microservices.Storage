using MediatR;
using Storage.API.Infrastructure.Routing;

namespace Storage.API.Features.Images.GetImages;

public class GetImagesEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("by-owner/{id:guid}",
            (IMediator mediatr, Guid ownerId) => mediatr.Send(new GetImagesQuery(ownerId)))
            .RequireAuthorization()
            .WithDescription("Получить изображения по идентификатору сущности-владельца");
    }
}
