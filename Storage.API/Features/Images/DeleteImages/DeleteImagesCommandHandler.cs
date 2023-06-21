using Storage.API.Data.Abstractions;
using Storage.API.Infrastructure.Mediator.Command;

namespace Storage.API.Features.Images.DeleteImages;

public class DeleteImagesCommandHandler : ICommandHandler<DeleteImagesCommand, bool>
{
    private readonly IDomainDbContext _dbContext;

    public DeleteImagesCommandHandler(IDomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
    {	
        var imageFiles = _dbContext.ImageFiles.Where(x => x.OwnerEntityId == request.OwnerId);
        if (!imageFiles.Any())
            return true;
        _dbContext.ImageFiles.RemoveRange(imageFiles);

        foreach (var imageFile in imageFiles)
            if (File.Exists(imageFile.Route))
                File.Delete(imageFile.Route);
        
        return await _dbContext.SaveEntitiesAsync();
    }
}
