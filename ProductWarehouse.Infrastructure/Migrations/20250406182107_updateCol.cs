using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductWarehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedQuantity",
                table: "products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservedQuantity",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
