using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetComp.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthProviderToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthProviderAuthenticationProviderId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AuthProviderEntity",
                columns: table => new
                {
                    AuthenticationProviderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthProviderEntity", x => x.AuthenticationProviderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthProviderAuthenticationProviderId",
                table: "Users",
                column: "AuthProviderAuthenticationProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthProviderEntity_AuthProviderAuthenticationProviderId",
                table: "Users",
                column: "AuthProviderAuthenticationProviderId",
                principalTable: "AuthProviderEntity",
                principalColumn: "AuthenticationProviderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthProviderEntity_AuthProviderAuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AuthProviderEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuthProviderAuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthProviderAuthenticationProviderId",
                table: "Users");
        }
    }
}
