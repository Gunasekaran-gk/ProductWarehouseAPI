using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductWarehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class checkConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Products_Price_NonNegative",
                table: "products",
                sql: "[Price] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Products_TotalQuantity_NonNegative",
                table: "products",
                sql: "[TotalQuantity] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Products_Price_NonNegative",
                table: "products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Products_TotalQuantity_NonNegative",
                table: "products");
        }
    }
}
