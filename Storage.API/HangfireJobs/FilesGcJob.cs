using Hangfire;
using Storage.API.Data.Abstractions;
using Storage.API.Models;

namespace Storage.API.HangfireJobs;

public class FilesGcJob
{
    public const string Id = "FilesGCJob";
    private readonly IDomainDbContext _dbContext;

    public FilesGcJob(IDomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [Queue("collect-garbage-images")]
    public async Task CollectGarbageImages(int toTake, int toSkip = 0)
    {
        var imagesPaths = GetStorageFilesPaths(StoragePaths.ImagePath);
        var imageDbRecords = _dbContext.ImageFiles
            .Skip(toSkip)
            .Take(toTake)
            .ToList();

        var garbageImages = 
            imagesPaths.Where(i =>
            imageDbRecords.All(r =>
                r.Id.ToString() != i[..i.IndexOf('_', StringComparison.InvariantCulture)]))
                .ToList();

        if (!garbageImages.Any())
        {
            if (toSkip < 5 && (toSkip * toTake < _dbContext.ImageFiles.Count()))
                await CollectGarbageImages(toTake, toSkip + 1);
            else
                return;
        }

        foreach (var imagePath in garbageImages.Where(File.Exists))
            File.Delete(imagePath);
    }

    private IEnumerable<string> GetStorageFilesPaths(string folderPath) =>
        Directory.GetFiles(folderPath).Select(f => Path.GetFileName(f)!).ToList();
}