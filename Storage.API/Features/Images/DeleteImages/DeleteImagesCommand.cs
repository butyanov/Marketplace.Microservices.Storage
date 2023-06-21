using Storage.API.Infrastructure.Mediator.Command;

namespace Storage.API.Features.Images.DeleteImages;

public record DeleteImagesCommand(Guid OwnerId) : ICommand<bool>;