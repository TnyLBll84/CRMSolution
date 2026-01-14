using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_DB.Migrations
{
    /// <inheritdoc />
    public partial class addCreditCardtoDBwithencryption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditCard",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "Customers");
        }
    }
}
