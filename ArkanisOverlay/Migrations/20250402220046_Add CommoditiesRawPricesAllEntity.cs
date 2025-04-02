using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkanisOverlay.Migrations
{
    /// <inheritdoc />
    public partial class AddCommoditiesRawPricesAllEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommoditiesRawPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommoditiesRawPricesAll");
        }
    }
}
