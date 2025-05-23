using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Week_02.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Ssn = table.Column<Guid>(type: "uuid", nullable: false),
                    Fname = table.Column<string>(type: "text", nullable: false),
                    Minit = table.Column<string>(type: "text", nullable: false),
                    Lname = table.Column<string>(type: "text", nullable: false),
                    Bday = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Sex = table.Column<char>(type: "character(1)", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false),
                    Super_ssn = table.Column<int>(type: "integer", nullable: false),
                    Dno = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Ssn);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
