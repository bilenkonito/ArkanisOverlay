using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arkanis.Overlay.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
