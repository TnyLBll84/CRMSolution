using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_DB.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustId",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "ComplaintType",
                table: "Complaints",
                newName: "Status");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_CustomerId",
                table: "Complaints",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ProductId",
                table: "Complaints",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Products_ProductId",
                table: "Complaints",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Products_ProductId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_CustomerId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ProductId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "CustId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Complaints",
                newName: "ComplaintType");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }
    }
}
