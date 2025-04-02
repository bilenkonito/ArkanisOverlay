using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkanisOverlay.Migrations
{
    /// <inheritdoc />
    public partial class AddItemPricesAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemsPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsPricesAll");
        }
    }
}
