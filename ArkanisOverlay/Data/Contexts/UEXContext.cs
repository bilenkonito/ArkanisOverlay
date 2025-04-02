using ArkanisOverlay.Data.Entities.UEX;
using Microsoft.EntityFrameworkCore;

namespace ArkanisOverlay.Data.Contexts;

public class UEXContext : DbContext
{
    private static readonly string DbPath = System.IO.Path.Join(
        Constants.LocalAppDataPath,
        "UEX.db"
    );
    
    public DbSet<CacheInfo> CacheInfos { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CommodityEntity> Commodities { get; set; }
    public DbSet<VehicleEntity> Vehicles { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<ItemsPricesAllEntity> ItemsPricesAll { get; set; }
    public DbSet<CommoditiesPricesAllEntity> CommoditiesPricesAll { get; set; }
    public DbSet<CommoditiesRawPricesAllEntity> CommoditiesRawPricesAll { get; set; }
    public DbSet<VehiclesPurchasesPricesAllEntity> VehiclesPurchasesPricesAll { get; set; }
    public DbSet<VehiclesRentalsPricesAllEntity> VehiclesRentalsPricesAll { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
        options.EnableSensitiveDataLogging();
    }
}