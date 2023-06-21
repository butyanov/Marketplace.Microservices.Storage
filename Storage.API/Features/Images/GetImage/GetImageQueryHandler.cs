using Storage.API.Data.Abstractions;
using Storage.API.Dto;
using Storage.API.Infrastructure;
using Storage.API.Infrastructure.Exceptions;
using Storage.API.Infrastructure.Mediator.Query;
using Storage.API.Models;
using Storage.API.Services.Utils;

namespace Storage.API.Features.Images.GetImage;

public class GetImageQueryHandler : IQueryHandler<GetImageQuery, FileResponse>
{
    private readonly IDomainDbContext _dbContext;

    public GetImageQueryHandler(IDomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileResponse> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var imageFile = await _dbContext.ImageFiles.FirstOrNotFoundAsync(file => file.Id == request.FileId,
            cancellationToken: cancellationToken);

        var filePath = imageFile.Route;

        if (!File.Exists(filePath))
            throw new NotFoundException<ImageFile>();

        var memory = new MemoryStream();
        await using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory, cancellationToken);
        }
        memory.Position = 0;

        return new FileResponse(memory, ContentExtensions.GetExtension(filePath), imageFile.Id);
    }
}
