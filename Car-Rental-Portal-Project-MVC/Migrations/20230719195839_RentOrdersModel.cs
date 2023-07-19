using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Rental_Portal_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class RentOrdersModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentOrders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ApplicationCarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentOrders", x => x.id);
                    table.ForeignKey(
                        name: "FK_RentOrders_ApplicationCars_ApplicationCarId",
                        column: x => x.ApplicationCarId,
                        principalTable: "ApplicationCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentOrders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentOrders_ApplicationCarId",
                table: "RentOrders",
                column: "ApplicationCarId");

            migrationBuilder.CreateIndex(
                name: "IX_RentOrders_ApplicationUserId",
                table: "RentOrders",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentOrders");
        }
    }
}
