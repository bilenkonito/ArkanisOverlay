namespace Arkanis.Overlay.Infrastructure.Data;

using Entities;
using Microsoft.EntityFrameworkCore;

public class UEXContext : DbContext
{
    public UEXContext(DbContextOptions options) : base(options)
    {
    }

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
}
