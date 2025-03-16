using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class editidTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Admin_AdminEmpId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Titles_AdminEmpId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "AdminEmpId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "TitleIds",
                table: "Admin");

            migrationBuilder.AddColumn<int>(
                name: "TitleId",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_TitleId",
                table: "Admin",
                column: "TitleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Titles_TitleId",
                table: "Admin",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "titleID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Titles_TitleId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_TitleId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Admin");

            migrationBuilder.AddColumn<int>(
                name: "AdminEmpId",
                table: "Titles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleIds",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Titles_AdminEmpId",
                table: "Titles",
                column: "AdminEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Admin_AdminEmpId",
                table: "Titles",
                column: "AdminEmpId",
                principalTable: "Admin",
                principalColumn: "empID");
        }
    }
}
