using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkanisOverlay.Migrations
{
    /// <inheritdoc />
    public partial class AddVehiclesPurchasesPricesAllEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateModified",
                table: "CommoditiesPricesAll",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateAdded",
                table: "CommoditiesPricesAll",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "VehiclesPurchasesPricesAll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiclesPurchasesPricesAll");

            migrationBuilder.AlterColumn<int>(
                name: "DateModified",
                table: "CommoditiesPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "DateAdded",
                table: "CommoditiesPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");
        }
    }
}
