using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Creation_LearningMigration.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTbl_DepartmentsTbl_DepartmentId",
                table: "EmployeeTbl");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTbl_DepartmentId",
                table: "EmployeeTbl");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "EmployeeTbl");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTbl_DeptID",
                table: "EmployeeTbl",
                column: "DeptID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTbl_DepartmentsTbl_DeptID",
                table: "EmployeeTbl",
                column: "DeptID",
                principalTable: "DepartmentsTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTbl_DepartmentsTbl_DeptID",
                table: "EmployeeTbl");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTbl_DeptID",
                table: "EmployeeTbl");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "EmployeeTbl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTbl_DepartmentId",
                table: "EmployeeTbl",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTbl_DepartmentsTbl_DepartmentId",
                table: "EmployeeTbl",
                column: "DepartmentId",
                principalTable: "DepartmentsTbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
