using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBDemo.Migrations
{
    public partial class AddMissingFKDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentNumber",
                table: "Employee",
                type: "char(6)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentNumber",
                table: "Employee",
                column: "DepartmentNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentNumber",
                table: "Employee",
                column: "DepartmentNumber",
                principalTable: "Department",
                principalColumn: "Number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentNumber",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmentNumber",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DepartmentNumber",
                table: "Employee");
        }
    }
}
