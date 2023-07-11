using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Rental_Portal_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rented",
                table: "ApplicationCars");

            migrationBuilder.RenameColumn(
                name: "RentedByUserId",
                table: "ApplicationCars",
                newName: "HiredByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HiredByUserId",
                table: "ApplicationCars",
                newName: "RentedByUserId");

            migrationBuilder.AddColumn<bool>(
                name: "Rented",
                table: "ApplicationCars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
