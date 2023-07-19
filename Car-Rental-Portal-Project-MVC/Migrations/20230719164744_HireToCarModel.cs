using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Rental_Portal_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class HireToCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HiredFrom",
                table: "ApplicationCars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HiredTo",
                table: "ApplicationCars",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HiredFrom",
                table: "ApplicationCars");

            migrationBuilder.DropColumn(
                name: "HiredTo",
                table: "ApplicationCars");
        }
    }
}
