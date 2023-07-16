using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheatMealLogsService.Migrations
{
    public partial class cheatmeals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheatMealLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Meal = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Calories = table.Column<double>(nullable: false),
                    Qty = table.Column<double>(nullable: false),
                    RecordDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheatMealLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheatMealLogs");
        }
    }
}
