using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareDesignExam.Migrations
{
    public partial class HighScoreDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "HighScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "HighScores",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "HighScores");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "HighScores");
        }
    }
}
