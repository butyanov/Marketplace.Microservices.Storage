namespace Storage.API.Dto;

public record FileResponse(MemoryStream Stream, string ContentType, Guid FileId);