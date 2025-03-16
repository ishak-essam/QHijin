using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Employees_EmployeeEmpId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Titles_EmployeeEmpId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "TitleIds",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    empID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Work_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Work_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "titleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Work_empID",
                table: "Work",
                column: "empID");

            migrationBuilder.CreateIndex(
                name: "IX_Work_TitleId",
                table: "Work",
                column: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeEmpId",
                table: "Titles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleIds",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_EmployeeEmpId",
                table: "Titles",
                column: "EmployeeEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Employees_EmployeeEmpId",
                table: "Titles",
                column: "EmployeeEmpId",
                principalTable: "Employees",
                principalColumn: "empID");
        }
    }
}
