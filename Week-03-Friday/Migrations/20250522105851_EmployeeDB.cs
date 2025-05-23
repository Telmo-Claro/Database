using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Week_03_Friday.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "employees");
        }
    }
}
