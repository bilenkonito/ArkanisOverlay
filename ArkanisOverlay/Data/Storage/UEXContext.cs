using ArkanisOverlay.Data.UEX.DTO;
using Microsoft.EntityFrameworkCore;

namespace ArkanisOverlay.Data.Storage;

public class UEXContext : DbContext
{
    private static readonly string DbPath = System.IO.Path.Join(
        Constants.LocalAppDataPath,
        "UEX.db"
    );

    public DbSet<CommodityDto> Commodities { get; set; }
    public DbSet<VehicleDto> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}