using Microsoft.EntityFrameworkCore;

namespace ArkanisOverlay.Data.Entities.UEX;

[PrimaryKey("TypeName")]
public class CacheInfo
{
    public required string TypeName { get; set; }
    public required string ApiPath { get; set; }
    public DateTime LastUpdated { get; set; }
}