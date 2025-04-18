using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arkanis.Overlay.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CacheInfos",
                columns: table => new
                {
                    TypeName = table.Column<string>(type: "TEXT", nullable: false),
                    ApiPath = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheInfos", x => x.TypeName);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Section = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IsGameRelated = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsMining = table.Column<bool>(type: "INTEGER", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commodities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdParent = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    Kind = table.Column<string>(type: "TEXT", nullable: true),
                    WeightScu = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuy = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSell = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAvailableLive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRaw = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRefined = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsMineral = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsHarvestable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBuyable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSellable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTemporary = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsIllegal = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFuel = table.Column<bool>(type: "INTEGER", nullable: false),
                    Wiki = table.Column<string>(type: "TEXT", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commodities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdParent = table.Column<int>(type: "INTEGER", nullable: true),
                    IdCategory = table.Column<int>(type: "INTEGER", nullable: true),
                    IdCompany = table.Column<int>(type: "INTEGER", nullable: true),
                    IdVehicle = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Section = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    VehicleName = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    Uuid = table.Column<string>(type: "TEXT", nullable: true),
                    UrlStore = table.Column<string>(type: "TEXT", nullable: true),
                    IsExclusivePledge = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsExclusiveSubscriber = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsExclusiveConcierge = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCompany = table.Column<int>(type: "INTEGER", nullable: false),
                    IdParent = table.Column<int>(type: "INTEGER", nullable: false),
                    IdsVehiclesLoaners = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NameFull = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    Uuid = table.Column<string>(type: "TEXT", nullable: true),
                    Scu = table.Column<decimal>(type: "TEXT", nullable: false),
                    Crew = table.Column<string>(type: "TEXT", nullable: true),
                    Mass = table.Column<decimal>(type: "TEXT", nullable: false),
                    Width = table.Column<decimal>(type: "TEXT", nullable: false),
                    Height = table.Column<decimal>(type: "TEXT", nullable: false),
                    Length = table.Column<decimal>(type: "TEXT", nullable: false),
                    FuelQuantum = table.Column<decimal>(type: "TEXT", nullable: false),
                    FuelHydrogen = table.Column<decimal>(type: "TEXT", nullable: false),
                    ContainerSizes = table.Column<string>(type: "TEXT", nullable: true),
                    IsAddon = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBoarding = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBomber = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCargo = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCarrier = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCivilian = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsConcept = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsConstruction = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDatarunner = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDocking = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEmp = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsExploration = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsGroundVehicle = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsHangar = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsIndustrial = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInterdiction = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLoadingDock = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsMedical = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsMilitary = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsMining = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPassenger = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsQed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRacing = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRefinery = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRefuel = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRepair = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsResearch = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSalvage = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsScanning = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsScience = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsShowdownWinner = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSpaceship = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsStarter = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsStealth = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTractorBeam = table.Column<bool>(type: "INTEGER", nullable: false),
                    UrlStore = table.Column<string>(type: "TEXT", nullable: true),
                    UrlBrochure = table.Column<string>(type: "TEXT", nullable: true),
                    UrlHotsite = table.Column<string>(type: "TEXT", nullable: true),
                    UrlVideo = table.Column<string>(type: "TEXT", nullable: true),
                    UrlPhotos = table.Column<string>(type: "TEXT", nullable: true),
                    PadType = table.Column<string>(type: "TEXT", nullable: true),
                    GameVersion = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommoditiesPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCommodity = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTerminal = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceBuy = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSell = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScuBuy = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScuBuyAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScuSellStock = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScuSellStockAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScuSell = table.Column<decimal>(type: "TEXT", nullable: false),
                    ScuSellAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    StatusBuy = table.Column<int>(type: "INTEGER", nullable: true),
                    StatusSell = table.Column<int>(type: "INTEGER", nullable: true),
                    CommodityName = table.Column<string>(type: "TEXT", nullable: true),
                    CommodityCode = table.Column<string>(type: "TEXT", nullable: true),
                    CommoditySlug = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalName = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalCode = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalSlug = table.Column<string>(type: "TEXT", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommoditiesPricesAll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommoditiesPricesAll_Commodities_IdCommodity",
                        column: x => x.IdCommodity,
                        principalTable: "Commodities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommoditiesRawPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCommodity = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTerminal = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceSell = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    CommodityName = table.Column<string>(type: "TEXT", nullable: true),
                    CommodityCode = table.Column<string>(type: "TEXT", nullable: true),
                    CommoditySlug = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalName = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalCode = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalSlug = table.Column<string>(type: "TEXT", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommoditiesRawPricesAll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommoditiesRawPricesAll_Commodities_IdCommodity",
                        column: x => x.IdCommodity,
                        principalTable: "Commodities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdItem = table.Column<int>(type: "INTEGER", nullable: false),
                    IdParent = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCategory = table.Column<int>(type: "INTEGER", nullable: false),
                    IdVehicle = table.Column<int>(type: "INTEGER", nullable: false),
                    IdStarSystem = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPlanet = table.Column<int>(type: "INTEGER", nullable: false),
                    IdOrbit = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMoon = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCity = table.Column<int>(type: "INTEGER", nullable: false),
                    IdOutpost = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPoi = table.Column<int>(type: "INTEGER", nullable: false),
                    IdFaction = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTerminal = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceBuy = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyMin = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyMinWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyMinMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyMaxWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyMaxMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyAvgWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBuyAvgMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSell = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellMin = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellMinWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellMinMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellMaxWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellMaxMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellAvgWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceSellAvgMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    Durability = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityMin = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityMinWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityMinMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityMax = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityMaxWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityMaxMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityAvg = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityAvgWeek = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurabilityAvgMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    FactionAffinity = table.Column<int>(type: "INTEGER", nullable: false),
                    GameVersion = table.Column<string>(type: "TEXT", nullable: true),
                    ItemName = table.Column<string>(type: "TEXT", nullable: true),
                    StarSystemName = table.Column<string>(type: "TEXT", nullable: true),
                    PlanetName = table.Column<string>(type: "TEXT", nullable: true),
                    OrbitName = table.Column<string>(type: "TEXT", nullable: true),
                    MoonName = table.Column<string>(type: "TEXT", nullable: true),
                    SpaceStationName = table.Column<string>(type: "TEXT", nullable: true),
                    OutpostName = table.Column<string>(type: "TEXT", nullable: true),
                    CityName = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalName = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalCode = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalIsPlayerOwned = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPricesAll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsPricesAll_Items_IdItem",
                        column: x => x.IdItem,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehiclesPurchasesPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdVehicle = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTerminal = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceBuy = table.Column<decimal>(type: "TEXT", nullable: false),
                    VehicleName = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalName = table.Column<string>(type: "TEXT", nullable: true),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesPurchasesPricesAll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclesPurchasesPricesAll_Vehicles_IdVehicle",
                        column: x => x.IdVehicle,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehiclesRentalsPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IdVehicle = table.Column<int>(type: "INTEGER", nullable: false),
                    IdTerminal = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceRent = table.Column<decimal>(type: "TEXT", nullable: false),
                    VehicleName = table.Column<string>(type: "TEXT", nullable: false),
                    TerminalName = table.Column<string>(type: "TEXT", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesRentalsPricesAll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclesRentalsPricesAll_Vehicles_IdVehicle",
                        column: x => x.IdVehicle,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesPricesAll_IdCommodity",
                table: "CommoditiesPricesAll",
                column: "IdCommodity");

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesRawPricesAll_IdCommodity",
                table: "CommoditiesRawPricesAll",
                column: "IdCommodity");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPricesAll_IdItem",
                table: "ItemsPricesAll",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesPurchasesPricesAll_IdVehicle",
                table: "VehiclesPurchasesPricesAll",
                column: "IdVehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesRentalsPricesAll_IdVehicle",
                table: "VehiclesRentalsPricesAll",
                column: "IdVehicle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheInfos");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CommoditiesPricesAll");

            migrationBuilder.DropTable(
                name: "CommoditiesRawPricesAll");

            migrationBuilder.DropTable(
                name: "ItemsPricesAll");

            migrationBuilder.DropTable(
                name: "VehiclesPurchasesPricesAll");

            migrationBuilder.DropTable(
                name: "VehiclesRentalsPricesAll");

            migrationBuilder.DropTable(
                name: "Commodities");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
