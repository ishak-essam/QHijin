using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class updateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Admin__empID__5FB337D6",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_empID",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_TitleId",
                table: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_empID",
                table: "Admin",
                column: "empID");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_TitleId",
                table: "Admin",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Employees_empID",
                table: "Admin",
                column: "empID",
                principalTable: "Employees",
                principalColumn: "empID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Employees_empID",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_empID",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_TitleId",
                table: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_empID",
                table: "Admin",
                column: "empID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_TitleId",
                table: "Admin",
                column: "TitleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Admin__empID__5FB337D6",
                table: "Admin",
                column: "empID",
                principalTable: "Employees",
                principalColumn: "empID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
