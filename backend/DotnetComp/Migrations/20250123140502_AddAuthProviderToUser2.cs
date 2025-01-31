using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthProviderToUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthProviderEntity_AuthProviderAuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthProviderEntity",
                table: "AuthProviderEntity");

            migrationBuilder.RenameTable(
                name: "AuthProviderEntity",
                newName: "AuthProviderEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthProviderEntities",
                table: "AuthProviderEntities",
                column: "AuthenticationProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthProviderEntities_AuthProviderAuthenticationProviderId",
                table: "Users",
                column: "AuthProviderAuthenticationProviderId",
                principalTable: "AuthProviderEntities",
                principalColumn: "AuthenticationProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthProviderEntities_AuthProviderAuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthProviderEntities",
                table: "AuthProviderEntities");

            migrationBuilder.RenameTable(
                name: "AuthProviderEntities",
                newName: "AuthProviderEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthProviderEntity",
                table: "AuthProviderEntity",
                column: "AuthenticationProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthProviderEntity_AuthProviderAuthenticationProviderId",
                table: "Users",
                column: "AuthProviderAuthenticationProviderId",
                principalTable: "AuthProviderEntity",
                principalColumn: "AuthenticationProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
