using Storage.API.Data.Abstractions;
using Storage.API.Infrastructure.Mediator.Command;
using Storage.API.Models;
using Storage.API.Services.Abstractions;

namespace Storage.API.Features.Images.UploadImages;

public class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, List<Guid>>
{
    private readonly IDomainDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UploadImageCommandHandler(IDomainDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<List<Guid>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var filesDict = CreateFileDictionary(request.Files);
        
        var imageFiles = filesDict.Select(fp => new ImageFile
        {
            Route = fp.Key,
            UploadDate = _dateTimeProvider.UtcNow,
            OwnerEntityId = request.OwnerEntityId
        }).ToList();
        
        await _dbContext.ImageFiles.AddRangeAsync(imageFiles, cancellationToken);

        foreach (var file in filesDict)
        {
            await using var stream = new FileStream(file.Key, FileMode.Create);
            await file.Value.CopyToAsync(stream, cancellationToken);
        }

        try
        {
            await _dbContext.SaveEntitiesAsync();
        }
        catch (Exception)
        {
            foreach (var file in filesDict.Where(file => File.Exists(file.Key)))
                File.Delete(file.Key);
            
            throw;
        }
        
        return imageFiles.Select(x => x.Id).ToList();
    }
    
    Dictionary<string, IFormFile> CreateFileDictionary(IEnumerable<IFormFile> files)
    {
        var fileDictionary = new Dictionary<string, IFormFile>();

        foreach (var file in files)
        {
            var filePath = Path.Combine(StoragePaths.ImagePath,
                $"{Guid.NewGuid()}_{_dateTimeProvider.UtcNow.ToShortDateString()}{Path.GetExtension(file.FileName)}");
            
            fileDictionary.Add(filePath, file);
        }

        return fileDictionary;
    }
}

