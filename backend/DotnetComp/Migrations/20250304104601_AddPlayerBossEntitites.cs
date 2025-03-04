using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerBossEntitites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerBossKillEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    BossId = table.Column<int>(type: "INTEGER", nullable: false),
                    Kills = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlayerEntityPlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBossKillEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerBossKillEntity_Players_PlayerEntityPlayerId",
                        column: x => x.PlayerEntityPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateTable(
                name: "PlayerBossRankEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    BossId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlayerEntityPlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBossRankEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerBossRankEntity_Players_PlayerEntityPlayerId",
                        column: x => x.PlayerEntityPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBossKillEntity_PlayerEntityPlayerId",
                table: "PlayerBossKillEntity",
                column: "PlayerEntityPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBossRankEntity_PlayerEntityPlayerId",
                table: "PlayerBossRankEntity",
                column: "PlayerEntityPlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerBossKillEntity");

            migrationBuilder.DropTable(
                name: "PlayerBossRankEntity");
        }
    }
}
