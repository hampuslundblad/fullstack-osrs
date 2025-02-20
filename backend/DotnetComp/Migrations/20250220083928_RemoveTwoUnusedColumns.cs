using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTwoUnusedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceGainedLast24H",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ExperienceGainedLastWeek",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExperienceGainedLast24H",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceGainedLastWeek",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
