using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arkanis.Overlay.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VehiclesRentalsPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VehiclesPurchasesPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemsPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CommoditiesRawPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CommoditiesPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Commodities",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesRentalsPricesAll_IdVehicle",
                table: "VehiclesRentalsPricesAll",
                column: "IdVehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesPurchasesPricesAll_IdVehicle",
                table: "VehiclesPurchasesPricesAll",
                column: "IdVehicle");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPricesAll_IdItem",
                table: "ItemsPricesAll",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesRawPricesAll_IdCommodity",
                table: "CommoditiesRawPricesAll",
                column: "IdCommodity");

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesPricesAll_IdCommodity",
                table: "CommoditiesPricesAll",
                column: "IdCommodity");

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesPricesAll_Commodities_IdCommodity",
                table: "CommoditiesPricesAll",
                column: "IdCommodity",
                principalTable: "Commodities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesRawPricesAll_Commodities_IdCommodity",
                table: "CommoditiesRawPricesAll",
                column: "IdCommodity",
                principalTable: "Commodities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsPricesAll_Items_IdItem",
                table: "ItemsPricesAll",
                column: "IdItem",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclesPurchasesPricesAll_Vehicles_IdVehicle",
                table: "VehiclesPurchasesPricesAll",
                column: "IdVehicle",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclesRentalsPricesAll_Vehicles_IdVehicle",
                table: "VehiclesRentalsPricesAll",
                column: "IdVehicle",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesPricesAll_Commodities_IdCommodity",
                table: "CommoditiesPricesAll");

            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesRawPricesAll_Commodities_IdCommodity",
                table: "CommoditiesRawPricesAll");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsPricesAll_Items_IdItem",
                table: "ItemsPricesAll");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclesPurchasesPricesAll_Vehicles_IdVehicle",
                table: "VehiclesPurchasesPricesAll");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclesRentalsPricesAll_Vehicles_IdVehicle",
                table: "VehiclesRentalsPricesAll");

            migrationBuilder.DropIndex(
                name: "IX_VehiclesRentalsPricesAll_IdVehicle",
                table: "VehiclesRentalsPricesAll");

            migrationBuilder.DropIndex(
                name: "IX_VehiclesPurchasesPricesAll_IdVehicle",
                table: "VehiclesPurchasesPricesAll");

            migrationBuilder.DropIndex(
                name: "IX_ItemsPricesAll_IdItem",
                table: "ItemsPricesAll");

            migrationBuilder.DropIndex(
                name: "IX_CommoditiesRawPricesAll_IdCommodity",
                table: "CommoditiesRawPricesAll");

            migrationBuilder.DropIndex(
                name: "IX_CommoditiesPricesAll_IdCommodity",
                table: "CommoditiesPricesAll");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VehiclesRentalsPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VehiclesPurchasesPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemsPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CommoditiesRawPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CommoditiesPricesAll",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Commodities",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
