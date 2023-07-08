using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Rental_Portal_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddedCarsAndRelationsWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "int", maxLength: 2023, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", maxLength: 99999, nullable: false),
                    Engine = table.Column<float>(type: "real", nullable: false),
                    Transmission = table.Column<int>(type: "int", nullable: false),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    FuelTank = table.Column<int>(type: "int", nullable: false),
                    WheelType = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PeopleAmount = table.Column<int>(type: "int", nullable: false),
                    Rented = table.Column<bool>(type: "bit", nullable: false),
                    RentedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationCars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCars_UserId",
                table: "ApplicationCars",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationCars");
        }
    }
}
