using Storage.API.Infrastructure.Mediator.Command;

namespace Storage.API.Features.Images.UploadImages;

public record UploadImageCommand(List<IFormFile> Files, Guid OwnerEntityId) : ICommand<List<Guid>>;