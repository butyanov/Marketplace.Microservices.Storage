using FluentValidation;
using MediatR;
using Storage.API.Infrastructure.Routing;
using Storage.API.Infrastructure.ValidationSetup;

namespace Storage.API.Features.Images.DeleteImages;

public class DeleteImagesEndpoint : IEndpoint
{
    public record DeleteImagesDto();

    public class DtoValidator : AbstractValidator<DeleteImagesDto>
    {
        public DtoValidator()
        {
           // Insert your validator
        }
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut(
                "/by-owner/{id:guid}",
                async (Guid ownerId, IMediator mediator)
                    => Results.Ok(
                        await mediator.Send(
                            new DeleteImagesCommand(ownerId))))
            .RequireAuthorization()
            .AddValidation(c => c.AddFor<DeleteImagesDto>())
            .WithName("DeleteImages");
    }
}
