using ArkanisOverlay.Data.Entities.UEX;
using Microsoft.EntityFrameworkCore;

namespace ArkanisOverlay.Data.Contexts;

public class UEXContext : DbContext
{
    private static readonly string DbPath = System.IO.Path.Join(
        Constants.LocalAppDataPath,
        "UEX.db"
    );

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CommodityEntity> Commodities { get; set; }
    public DbSet<VehicleEntity> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
        options.EnableSensitiveDataLogging();
    }
}