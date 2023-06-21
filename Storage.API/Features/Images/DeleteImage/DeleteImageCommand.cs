using Storage.API.Infrastructure.Mediator.Command;

namespace Storage.API.Features.Images.DeleteImage;

public record DeleteImageCommand(Guid FileId) : ICommand<bool>;