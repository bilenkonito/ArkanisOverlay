using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arkanis.Overlay.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddCacheInfo : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CacheInfos");
        }
    }
}
