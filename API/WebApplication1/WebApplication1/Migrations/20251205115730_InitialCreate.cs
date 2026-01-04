using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barber",
                columns: table => new
                {
                    BarberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barber", x => x.BarberId);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    BarberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visit_Barber_BarberId",
                        column: x => x.BarberId,
                        principalTable: "Barber",
                        principalColumn: "BarberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Barber",
                columns: new[] { "BarberId", "BirthDate", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 5, 12), "Adam", "Kowalski" },
                    { 2, new DateOnly(1996, 7, 23), "Jakub", "Nowak" },
                    { 3, new DateOnly(2001, 1, 6), "Michal", "Wojcik" }
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "BirthDate", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateOnly(1991, 4, 18), "Mateusz", "Kowalczyk" },
                    { 2, new DateOnly(1988, 9, 7), "Agnieszka", "Nowak" },
                    { 3, new DateOnly(1994, 12, 22), "Krzysztof", "Zielinski" }
                });

            migrationBuilder.InsertData(
                table: "Visit",
                columns: new[] { "VisitId", "BarberId", "ClientId", "Comment", "End", "Price", "Start" },
                values: new object[,]
                {
                    { 1, 1, 1, "Strzyrzenie", new DateTime(2025, 1, 5, 16, 30, 0, 0, DateTimeKind.Unspecified), 110m, new DateTime(2025, 1, 5, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 2, "Golenie", new DateTime(2025, 2, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), 75m, new DateTime(2025, 2, 18, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, "Strzyrzenie + golenie", new DateTime(2025, 3, 1, 18, 30, 0, 0, DateTimeKind.Unspecified), 150m, new DateTime(2025, 3, 1, 17, 10, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visit_BarberId",
                table: "Visit",
                column: "BarberId");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_ClientId",
                table: "Visit",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Barber");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
