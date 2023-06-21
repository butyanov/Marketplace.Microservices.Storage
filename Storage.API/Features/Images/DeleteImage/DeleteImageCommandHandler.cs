using Storage.API.Data.Abstractions;
using Storage.API.Infrastructure;
using Storage.API.Infrastructure.Mediator.Command;

namespace Storage.API.Features.Images.DeleteImage;

public class DeleteImageCommandHandler : ICommandHandler<DeleteImageCommand, bool>
{
    private readonly IDomainDbContext _dbContext;

    public DeleteImageCommandHandler(IDomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var imageFile = await _dbContext.ImageFiles.FirstOrNotFoundAsync(x => x.Id == request.FileId, cancellationToken: cancellationToken);
        _dbContext.ImageFiles.Remove(imageFile);
        
        if (File.Exists(imageFile.Route))
            File.Delete(imageFile.Route);
        
        return await _dbContext.SaveEntitiesAsync();
    }
}
