using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkanisOverlay.Migrations
{
    /// <inheritdoc />
    public partial class AddCommoditiesPricesAllEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommoditiesPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    DateAdded = table.Column<int>(type: "INTEGER", nullable: false),
                    DateModified = table.Column<int>(type: "INTEGER", nullable: false),
                    CommodityName = table.Column<string>(type: "TEXT", nullable: true),
                    CommodityCode = table.Column<string>(type: "TEXT", nullable: true),
                    CommoditySlug = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalName = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalCode = table.Column<string>(type: "TEXT", nullable: true),
                    TerminalSlug = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommoditiesPricesAll", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommoditiesPricesAll");
        }
    }
}
