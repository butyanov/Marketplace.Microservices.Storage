using Storage.API.Dto;
using Storage.API.Infrastructure.Mediator.Query;

namespace Storage.API.Features.Images.GetImages;

public record GetImagesQuery(Guid OwnerId) : IQuery<List<FileResponse>>;