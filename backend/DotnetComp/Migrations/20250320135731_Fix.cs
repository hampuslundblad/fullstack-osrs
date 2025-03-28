﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
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

            migrationBuilder.CreateTable(
                name: "Bosses",
                columns: table => new
                {
                    BossId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bosses", x => x.BossId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerBossStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BossId = table.Column<int>(type: "INTEGER", nullable: false),
                    Kills = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerEntityPlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBossStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerBossStats_Players_PlayerEntityPlayerId",
                        column: x => x.PlayerEntityPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bosses_Name",
                table: "Bosses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBossStats_PlayerEntityPlayerId",
                table: "PlayerBossStats",
                column: "PlayerEntityPlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bosses");

            migrationBuilder.DropTable(
                name: "PlayerBossStats");

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
