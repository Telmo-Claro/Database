using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBDemo.Migrations
{
    public partial class CompleteSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Number = table.Column<string>(type: "char(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", nullable: true),
                    ManagerSSN = table.Column<string>(type: "char(9)", nullable: true),
                    ManagerStartDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Department_Employee_ManagerSSN",
                        column: x => x.ManagerSSN,
                        principalTable: "Employee",
                        principalColumn: "SSN");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentLocation",
                columns: table => new
                {
                    DepartmentNumber = table.Column<string>(type: "char(6)", nullable: false),
                    Location = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentLocation", x => new { x.DepartmentNumber, x.Location });
                    table.ForeignKey(
                        name: "FK_DepartmentLocation_Department_DepartmentNumber",
                        column: x => x.DepartmentNumber,
                        principalTable: "Department",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Number = table.Column<string>(type: "char(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    DepartmentNumber = table.Column<string>(type: "char(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Project_Department_DepartmentNumber",
                        column: x => x.DepartmentNumber,
                        principalTable: "Department",
                        principalColumn: "Number");
                });

            migrationBuilder.CreateTable(
                name: "WorksOn",
                columns: table => new
                {
                    EmployeeSSN = table.Column<string>(type: "char(9)", nullable: false),
                    ProjectNumber = table.Column<string>(type: "char(6)", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksOn", x => new { x.EmployeeSSN, x.ProjectNumber });
                    table.ForeignKey(
                        name: "FK_WorksOn_Employee_EmployeeSSN",
                        column: x => x.EmployeeSSN,
                        principalTable: "Employee",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorksOn_Project_ProjectNumber",
                        column: x => x.ProjectNumber,
                        principalTable: "Project",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerSSN",
                table: "Department",
                column: "ManagerSSN");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DepartmentNumber",
                table: "Project",
                column: "DepartmentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorksOn_ProjectNumber",
                table: "WorksOn",
                column: "ProjectNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentLocation");

            migrationBuilder.DropTable(
                name: "WorksOn");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
