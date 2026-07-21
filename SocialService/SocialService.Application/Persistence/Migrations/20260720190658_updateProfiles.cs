using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialService.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "profiles");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "profiles",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "profiles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "profiles",
                newName: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_profiles",
                table: "profiles",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_profiles",
                table: "profiles");

            migrationBuilder.RenameTable(
                name: "profiles",
                newName: "Profiles");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Profiles",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Profiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Profiles",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");
        }
    }
}
