using MediatR;
using Storage.API.Infrastructure.Routing;

namespace Storage.API.Features.Images.UploadImages;

public class UploadImageEndpoint : IEndpoint
{
    record UploadImageDto(List<IFormFile> Files, Guid OwnerId);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/upload",
                async (UploadImageDto request, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new UploadImageCommand(
                       request.Files
                       , request.OwnerId))));
    }
}
