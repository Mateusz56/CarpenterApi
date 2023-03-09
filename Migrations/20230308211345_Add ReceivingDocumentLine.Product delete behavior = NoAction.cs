using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddReceivingDocumentLineProductdeletebehaviorNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivingDocumentLines_Products_ProductId",
                table: "ReceivingDocumentLines");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivingDocumentLines_Products_ProductId",
                table: "ReceivingDocumentLines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivingDocumentLines_Products_ProductId",
                table: "ReceivingDocumentLines");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivingDocumentLines_Products_ProductId",
                table: "ReceivingDocumentLines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
