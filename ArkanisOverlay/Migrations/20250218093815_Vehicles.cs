using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArkanisOverlay.Migrations
{
    /// <inheritdoc />
    public partial class Vehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdCompany = table.Column<int>(type: "INTEGER", nullable: false),
                    IdParent = table.Column<int>(type: "INTEGER", nullable: false),
                    IdsVehiclesLoaners = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NameFull = table.Column<string>(type: "TEXT", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    Uuid = table.Column<string>(type: "TEXT", nullable: true),
                    Scu = table.Column<int>(type: "INTEGER", nullable: false),
                    Crew = table.Column<string>(type: "TEXT", nullable: true),
                    Mass = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    Length = table.Column<int>(type: "INTEGER", nullable: false),
                    FuelQuantum = table.Column<int>(type: "INTEGER", nullable: false),
                    FuelHydrogen = table.Column<int>(type: "INTEGER", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
