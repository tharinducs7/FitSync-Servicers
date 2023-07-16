using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutActivityService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkoutActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    WorkoutType = table.Column<string>(nullable: false),
                    DurationInMinutes = table.Column<int>(nullable: false),
                    CaloriesBurnedPerMinute = table.Column<double>(nullable: false),
                    DistanceInKm = table.Column<double>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutActivities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutActivities");
        }
    }
}
