using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkanisOverlay.Migrations
{
    /// <inheritdoc />
    public partial class FixVehilcesRentalsPricesAllPriceRentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceRent",
                table: "VehiclesRentalsPricesAll",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PriceRent",
                table: "VehiclesRentalsPricesAll",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
