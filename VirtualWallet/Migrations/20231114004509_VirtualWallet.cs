﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualWallet.Migrations
{
    /// <inheritdoc />
    public partial class VirtualWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogue", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FixedTermDeposit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NominalRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedTermDeposit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedTermDeposit_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FixedTermDeposit_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    transactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Concept = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ToAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.transactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Catalogue",
                columns: new[] { "Id", "Image", "Points", "ProductDescription" },
                values: new object[,]
                {
                    { 3, "Image3.jpg", 32000, "Auriculares inalámbricos" },
                    { 4, "Image4.jpg", 5000, "Tarjeta de regalo de $50" },
                    { 5, "Image5.jpg", 12000, "Camiseta de edición limitada" },
                    { 6, "Image6.jpg", 15000, "Botella de vino premium" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Permisos para agregar y eliminar usuarios", "Admin" },
                    { 2, "Cliente", "Regular" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "First_name", "Last_name", "Password", "Points", "Role_Id" },
                values: new object[,]
                {
                    { 1, "pedro@pedro", "Pedro", "Gonzalez", "123", 50, 1 },
                    { 2, "fede@fede", "Fede", "Perez", "1234", 504, 2 },
                    { 3, "maca@maca", "Maca", "Pereira", "12345", 5, 2 },
                    { 4, "sofi@sofi", "Sofia", "Gomez", "123456", 23, 2 }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreationDate", "IsBlocked", "Money", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 210600m, 1 },
                    { 2, new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 100980m, 2 },
                    { 3, new DateTime(2019, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 250990m, 3 },
                    { 4, new DateTime(2019, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 420560m, 4 }
                });

            migrationBuilder.InsertData(
                table: "FixedTermDeposit",
                columns: new[] { "Id", "AccountId", "Amount", "ClosingDate", "CreationDate", "NominalRate", "State", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 10000.00m, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0m, "Active", 1 },
                    { 2, 2, 15000.00m, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.5m, "Active", 2 },
                    { 3, 3, 20000.00m, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.0m, "Active", 3 },
                    { 4, 4, 12000.00m, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.2m, "Active", 4 }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "transactionId", "AccountId", "Amount", "Concept", "Date", "ToAccountId", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 7500m, "Compra en línea 3", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "payment", 2 },
                    { 2, 1, 3000m, "Depósito en efectivo 2", new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "topup", 1 },
                    { 3, 3, 9000m, "Pago de factura 2", new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "payment", 3 },
                    { 4, 2, 6000m, "Transferencia bancaria 2", new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "payment", 2 },
                    { 5, 4, 4200m, "Recarga de tarjeta 2", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "topup", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedTermDeposit_AccountId",
                table: "FixedTermDeposit",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedTermDeposit_UserId",
                table: "FixedTermDeposit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_Id",
                table: "User",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalogue");

            migrationBuilder.DropTable(
                name: "FixedTermDeposit");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
