using Storage.API.Data.Abstractions;
using Storage.API.Dto;
using Storage.API.Infrastructure.Exceptions;
using Storage.API.Infrastructure.Mediator.Query;
using Storage.API.Models;
using Storage.API.Services.Utils;

namespace Storage.API.Features.Images.GetImages;

public class GetImagesQueryHandler : IQueryHandler<GetImagesQuery, List<FileResponse>>
{
    private readonly IDomainDbContext _dbContext;

    public GetImagesQueryHandler(IDomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<FileResponse>> Handle(
        GetImagesQuery request,
        CancellationToken cancellationToken)
    {
        var imageFiles = _dbContext.ImageFiles
                            .Where(file => file.OwnerEntityId == request.OwnerId)
                            .AsEnumerable()
                            .Where(file => File.Exists(file.Route))
                        ?? throw new NotFoundException<ImageFile>();
        
        var fileResponses = new List<FileResponse>();
        foreach (var file in imageFiles)
        {
            var memory = new MemoryStream();
            await using (var stream = new FileStream(file.Route, FileMode.Open))
            {
                await stream.CopyToAsync(memory, cancellationToken);
            }
            memory.Position = 0;
            fileResponses.Add(new FileResponse(memory, ContentExtensions.GetExtension(file.Route), file.Id));
        }

        return fileResponses;
    }
}
