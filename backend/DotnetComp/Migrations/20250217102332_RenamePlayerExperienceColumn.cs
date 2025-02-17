using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class RenamePlayerExperienceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerExperience");

            migrationBuilder.CreateTable(
                name: "PlayerExperienceEntity",
                columns: table => new
                {
                    PlayerExperienceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlayerEntityPlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerExperienceEntity", x => x.PlayerExperienceId);
                    table.ForeignKey(
                        name: "FK_PlayerExperienceEntity_Players_PlayerEntityPlayerId",
                        column: x => x.PlayerEntityPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerExperienceEntity_PlayerEntityPlayerId",
                table: "PlayerExperienceEntity",
                column: "PlayerEntityPlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerExperienceEntity");

            migrationBuilder.CreateTable(
                name: "PlayerExperience",
                columns: table => new
                {
                    PlayerExperienceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerEntityPlayerId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerExperience", x => x.PlayerExperienceId);
                    table.ForeignKey(
                        name: "FK_PlayerExperience_Players_PlayerEntityPlayerId",
                        column: x => x.PlayerEntityPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerExperience_PlayerEntityPlayerId",
                table: "PlayerExperience",
                column: "PlayerEntityPlayerId");
        }
    }
}
