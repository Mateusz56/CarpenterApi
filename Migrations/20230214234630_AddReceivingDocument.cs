using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddReceivingDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceivingDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivingDocumentLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceivingDocumentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivingDocumentLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivingDocumentLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceivingDocumentLines_ReceivingDocuments_ReceivingDocumentId",
                        column: x => x.ReceivingDocumentId,
                        principalTable: "ReceivingDocuments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingDocumentLines_ProductId",
                table: "ReceivingDocumentLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivingDocumentLines_ReceivingDocumentId",
                table: "ReceivingDocumentLines",
                column: "ReceivingDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceivingDocumentLines");

            migrationBuilder.DropTable(
                name: "ReceivingDocuments");
        }
    }
}
