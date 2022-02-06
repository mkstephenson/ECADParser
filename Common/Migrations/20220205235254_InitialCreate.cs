using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECADParser.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CloudCover = table.Column<int>(type: "int", nullable: false),
                    CloudCoverQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WindDirection = table.Column<int>(type: "int", nullable: false),
                    WindDirectionQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    ElementId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.ElementId);
                });

            migrationBuilder.CreateTable(
                name: "FG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    WindSpeedQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WindGust = table.Column<int>(type: "int", nullable: false),
                    WindGustQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FX", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    HumidityQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HU", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeaLevelPressure = table.Column<int>(type: "int", nullable: false),
                    SeaLevelPressureQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalRadiation = table.Column<int>(type: "int", nullable: false),
                    GlobalRadiationQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precipitation = table.Column<int>(type: "int", nullable: false),
                    PrecipitationQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SnowDepth = table.Column<int>(type: "int", nullable: false),
                    SnowDepthQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    SourceName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    ParticipantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.SourceId);
                });

            migrationBuilder.CreateTable(
                name: "SS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sunshine = table.Column<int>(type: "int", nullable: false),
                    SunshineQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "TG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeanTemperature = table.Column<int>(type: "int", nullable: false),
                    MeanTemperatureQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinTemperature = table.Column<int>(type: "int", nullable: false),
                    MinTemperatureQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxTemperature = table.Column<int>(type: "int", nullable: false),
                    MaxTemperatureQuality = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TX", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CC");

            migrationBuilder.DropTable(
                name: "DD");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "FG");

            migrationBuilder.DropTable(
                name: "FX");

            migrationBuilder.DropTable(
                name: "HU");

            migrationBuilder.DropTable(
                name: "PP");

            migrationBuilder.DropTable(
                name: "QQ");

            migrationBuilder.DropTable(
                name: "RR");

            migrationBuilder.DropTable(
                name: "SD");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "SS");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "TG");

            migrationBuilder.DropTable(
                name: "TN");

            migrationBuilder.DropTable(
                name: "TX");
        }
    }
}
