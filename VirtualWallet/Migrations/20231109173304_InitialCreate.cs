using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualWallet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreationDate", "IsBlocked", "Money" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 210600m },
                    { 2, new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 100980m },
                    { 3, new DateTime(2019, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 250990m },
                    { 4, new DateTime(2019, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 420560m },
                    { 5, new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 80980m },
                    { 6, new DateTime(2020, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 380250m },
                    { 7, new DateTime(2021, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 120900m },
                    { 8, new DateTime(2021, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 130360m },
                    { 9, new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 220800m },
                    { 10, new DateTime(2022, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 685510m },
                    { 11, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 550380m },
                    { 12, new DateTime(2023, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 310630m }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Permisos para agregar y eliminar usuarios", "Admin" },
                    { 2, "Cliente", "Regular" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
