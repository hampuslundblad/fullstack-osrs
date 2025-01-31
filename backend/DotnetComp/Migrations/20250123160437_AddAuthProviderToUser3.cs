using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthProviderToUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserEntityUserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Groups_GroupEntityGroupId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthProviderEntities_AuthProviderAuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuthProviderAuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Players_GroupEntityGroupId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Groups_UserEntityUserId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthProviderEntities",
                table: "AuthProviderEntities");

            migrationBuilder.DropColumn(
                name: "AuthProviderAuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupEntityGroupId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "UserEntityUserId",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "AuthProviderEntities",
                newName: "AuthProviders");

            migrationBuilder.RenameColumn(
                name: "AuthenticationProviderId",
                table: "AuthProviders",
                newName: "AuthProviderId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Groups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AuthProviders",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "AuthProviderUserId",
                table: "AuthProviders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthProviders",
                table: "AuthProviders",
                column: "AuthProviderId");

            migrationBuilder.CreateTable(
                name: "GroupEntityPlayerEntity",
                columns: table => new
                {
                    GroupsGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayersPlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEntityPlayerEntity", x => new { x.GroupsGroupId, x.PlayersPlayerId });
                    table.ForeignKey(
                        name: "FK_GroupEntityPlayerEntity_Groups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupEntityPlayerEntity_Players_PlayersPlayerId",
                        column: x => x.PlayersPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthProviders_UserId",
                table: "AuthProviders",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntityPlayerEntity_PlayersPlayerId",
                table: "GroupEntityPlayerEntity",
                column: "PlayersPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthProviders_Users_UserId",
                table: "AuthProviders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthProviders_Users_UserId",
                table: "AuthProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UserId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupEntityPlayerEntity");

            migrationBuilder.DropIndex(
                name: "IX_Groups_UserId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthProviders",
                table: "AuthProviders");

            migrationBuilder.DropIndex(
                name: "IX_AuthProviders_UserId",
                table: "AuthProviders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AuthProviderUserId",
                table: "AuthProviders");

            migrationBuilder.RenameTable(
                name: "AuthProviders",
                newName: "AuthProviderEntities");

            migrationBuilder.RenameColumn(
                name: "AuthProviderId",
                table: "AuthProviderEntities",
                newName: "AuthenticationProviderId");

            migrationBuilder.AddColumn<int>(
                name: "AuthProviderAuthenticationProviderId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupEntityGroupId",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserEntityUserId",
                table: "Groups",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuthProviderEntities",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthProviderEntities",
                table: "AuthProviderEntities",
                column: "AuthenticationProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthProviderAuthenticationProviderId",
                table: "Users",
                column: "AuthProviderAuthenticationProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GroupEntityGroupId",
                table: "Players",
                column: "GroupEntityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserEntityUserId",
                table: "Groups",
                column: "UserEntityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UserEntityUserId",
                table: "Groups",
                column: "UserEntityUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Groups_GroupEntityGroupId",
                table: "Players",
                column: "GroupEntityGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthProviderEntities_AuthProviderAuthenticationProviderId",
                table: "Users",
                column: "AuthProviderAuthenticationProviderId",
                principalTable: "AuthProviderEntities",
                principalColumn: "AuthenticationProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
