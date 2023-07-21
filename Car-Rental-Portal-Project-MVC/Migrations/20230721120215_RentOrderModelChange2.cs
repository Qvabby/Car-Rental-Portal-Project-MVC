using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Rental_Portal_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class RentOrderModelChange2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "RentOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "RentOrders");
        }
    }
}
