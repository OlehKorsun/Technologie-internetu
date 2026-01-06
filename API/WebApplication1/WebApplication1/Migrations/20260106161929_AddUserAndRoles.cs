using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Client",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdRola = table.Column<int>(type: "int", nullable: false),
                    RoleIdRole = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IdRola",
                        column: x => x.IdRola,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleIdRole",
                        column: x => x.RoleIdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole");
                });

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRole", "Title" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "Email", "IdRola", "Login", "Password", "RoleIdRole", "Salt" },
                values: new object[] { 1, "admin@admin.com", 1, "admin", "HASHED_PASSWORD", null, "HASHED_SALT" });

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRola",
                table: "Users",
                column: "IdRola");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleIdRole",
                table: "Users",
                column: "RoleIdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Users_UserId",
                table: "Client",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Users_UserId",
                table: "Client");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Client");
        }
    }
}
