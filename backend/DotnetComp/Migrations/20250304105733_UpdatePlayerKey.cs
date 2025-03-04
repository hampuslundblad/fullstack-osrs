using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayerKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEntityPlayerEntity_Groups_GroupsGroupId",
                table: "GroupEntityPlayerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupEntityPlayerEntity_Players_PlayersPlayerId",
                table: "GroupEntityPlayerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerBossKillEntity_Players_PlayerEntityPlayerId",
                table: "PlayerBossKillEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerBossRankEntity_Players_PlayerEntityPlayerId",
                table: "PlayerBossRankEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerExperienceEntity_Players_PlayerEntityPlayerId",
                table: "PlayerExperienceEntity");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Players",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlayerEntityPlayerId",
                table: "PlayerExperienceEntity",
                newName: "PlayerEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerExperienceEntity_PlayerEntityPlayerId",
                table: "PlayerExperienceEntity",
                newName: "IX_PlayerExperienceEntity_PlayerEntityId");

            migrationBuilder.RenameColumn(
                name: "PlayerEntityPlayerId",
                table: "PlayerBossRankEntity",
                newName: "PlayerEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerBossRankEntity_PlayerEntityPlayerId",
                table: "PlayerBossRankEntity",
                newName: "IX_PlayerBossRankEntity_PlayerEntityId");

            migrationBuilder.RenameColumn(
                name: "PlayerEntityPlayerId",
                table: "PlayerBossKillEntity",
                newName: "PlayerEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerBossKillEntity_PlayerEntityPlayerId",
                table: "PlayerBossKillEntity",
                newName: "IX_PlayerBossKillEntity_PlayerEntityId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Groups",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlayersPlayerId",
                table: "GroupEntityPlayerEntity",
                newName: "PlayersId");

            migrationBuilder.RenameColumn(
                name: "GroupsGroupId",
                table: "GroupEntityPlayerEntity",
                newName: "GroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEntityPlayerEntity_PlayersPlayerId",
                table: "GroupEntityPlayerEntity",
                newName: "IX_GroupEntityPlayerEntity_PlayersId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEntityPlayerEntity_Groups_GroupsId",
                table: "GroupEntityPlayerEntity",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEntityPlayerEntity_Players_PlayersId",
                table: "GroupEntityPlayerEntity",
                column: "PlayersId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerBossKillEntity_Players_PlayerEntityId",
                table: "PlayerBossKillEntity",
                column: "PlayerEntityId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerBossRankEntity_Players_PlayerEntityId",
                table: "PlayerBossRankEntity",
                column: "PlayerEntityId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerExperienceEntity_Players_PlayerEntityId",
                table: "PlayerExperienceEntity",
                column: "PlayerEntityId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEntityPlayerEntity_Groups_GroupsId",
                table: "GroupEntityPlayerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupEntityPlayerEntity_Players_PlayersId",
                table: "GroupEntityPlayerEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerBossKillEntity_Players_PlayerEntityId",
                table: "PlayerBossKillEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerBossRankEntity_Players_PlayerEntityId",
                table: "PlayerBossRankEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerExperienceEntity_Players_PlayerEntityId",
                table: "PlayerExperienceEntity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Players",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerEntityId",
                table: "PlayerExperienceEntity",
                newName: "PlayerEntityPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerExperienceEntity_PlayerEntityId",
                table: "PlayerExperienceEntity",
                newName: "IX_PlayerExperienceEntity_PlayerEntityPlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerEntityId",
                table: "PlayerBossRankEntity",
                newName: "PlayerEntityPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerBossRankEntity_PlayerEntityId",
                table: "PlayerBossRankEntity",
                newName: "IX_PlayerBossRankEntity_PlayerEntityPlayerId");

            migrationBuilder.RenameColumn(
                name: "PlayerEntityId",
                table: "PlayerBossKillEntity",
                newName: "PlayerEntityPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerBossKillEntity_PlayerEntityId",
                table: "PlayerBossKillEntity",
                newName: "IX_PlayerBossKillEntity_PlayerEntityPlayerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "PlayersId",
                table: "GroupEntityPlayerEntity",
                newName: "PlayersPlayerId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupEntityPlayerEntity",
                newName: "GroupsGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEntityPlayerEntity_PlayersId",
                table: "GroupEntityPlayerEntity",
                newName: "IX_GroupEntityPlayerEntity_PlayersPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEntityPlayerEntity_Groups_GroupsGroupId",
                table: "GroupEntityPlayerEntity",
                column: "GroupsGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEntityPlayerEntity_Players_PlayersPlayerId",
                table: "GroupEntityPlayerEntity",
                column: "PlayersPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerBossKillEntity_Players_PlayerEntityPlayerId",
                table: "PlayerBossKillEntity",
                column: "PlayerEntityPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerBossRankEntity_Players_PlayerEntityPlayerId",
                table: "PlayerBossRankEntity",
                column: "PlayerEntityPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerExperienceEntity_Players_PlayerEntityPlayerId",
                table: "PlayerExperienceEntity",
                column: "PlayerEntityPlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }
    }
}
