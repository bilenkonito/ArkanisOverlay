using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arkanis.Overlay.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commodities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdParent = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    Kind = table.Column<string>(type: "TEXT", nullable: true),
                    WeightScu = table.Column<float>(type: "REAL", nullable: false),
                    PriceBuy = table.Column<float>(type: "REAL", nullable: false),
                    PriceSell = table.Column<float>(type: "REAL", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commodities");
        }
    }
}
