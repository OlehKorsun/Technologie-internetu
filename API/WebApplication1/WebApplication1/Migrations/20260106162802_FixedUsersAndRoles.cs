using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class FixedUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_IdRola",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleIdRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleIdRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleIdRole",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_IdRola",
                table: "Users",
                column: "IdRola",
                principalTable: "Roles",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_IdRola",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RoleIdRole",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "IdUser",
                keyValue: 1,
                column: "RoleIdRole",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleIdRole",
                table: "Users",
                column: "RoleIdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_IdRola",
                table: "Users",
                column: "IdRola",
                principalTable: "Roles",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleIdRole",
                table: "Users",
                column: "RoleIdRole",
                principalTable: "Roles",
                principalColumn: "IdRole");
        }
    }
}
