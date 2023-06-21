using Storage.API.Features.Images.DeleteImage;
using Storage.API.Features.Images.DeleteImages;
using Storage.API.Features.Images.GetImage;
using Storage.API.Features.Images.GetImages;
using Storage.API.Features.Images.UploadImages;
using Storage.API.Infrastructure.Routing;

namespace Storage.API.Features.Images;

public class ImagesEndpointRoot : IEndpointRoot
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/files/images")
            .WithTags("Изображения")
            .AddEndpoint<GetImageEndpoint>()
            .AddEndpoint<GetImagesEndpoint>()
            .AddEndpoint<UploadImageEndpoint>()
            .AddEndpoint<DeleteImageEndpoint>()
            .AddEndpoint<DeleteImagesEndpoint>();
    }
}