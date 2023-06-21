namespace Storage.API.Models.Abstractions;

public abstract class BaseFile : BaseEntity
{
    public required string Route { get; set; }
    
    public DateTime UploadDate { get; set; }
    
    public Guid OwnerEntityId { get; set; }
}