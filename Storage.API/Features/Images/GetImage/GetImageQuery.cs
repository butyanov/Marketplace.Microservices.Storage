using Storage.API.Dto;
using Storage.API.Infrastructure.Mediator.Query;

namespace Storage.API.Features.Images.GetImage;

public record GetImageQuery(Guid FileId) : IQuery<FileResponse>;