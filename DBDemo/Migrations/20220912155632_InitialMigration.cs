using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBDemo.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    SSN = table.Column<string>(type: "char(9)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    MiddleInitials = table.Column<string>(type: "varchar(5)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Salary = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.SSN);
                });

            migrationBuilder.CreateTable(
                name: "Dependent",
                columns: table => new
                {
                    EmployeeSSN = table.Column<string>(type: "char(9)", nullable: false),
                    DependentName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Relationship = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependent", x => new { x.EmployeeSSN, x.DependentName });
                    table.ForeignKey(
                        name: "FK_Dependent_Employee_EmployeeSSN",
                        column: x => x.EmployeeSSN,
                        principalTable: "Employee",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependent");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
