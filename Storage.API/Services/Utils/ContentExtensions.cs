namespace Storage.API.Services.Utils;

public static class ContentExtensions
{
    public static string GetExtension(string path)
    {
        var types = GetMimeExtensions();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    private static Dictionary<string, string> GetMimeExtensions()
    {
        return new Dictionary<string, string>
        {
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".png", "image/png"},
            {".gif", "image/gif"}
        };
    }
}