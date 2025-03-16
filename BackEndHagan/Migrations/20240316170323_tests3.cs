using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class tests3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int> (
                name: "AdminEmpId",
                table: "Titles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string> (
                name: "TitleIds",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex (
                name: "IX_Titles_AdminEmpId",
                table: "Titles",
                column: "AdminEmpId");

            migrationBuilder.AddForeignKey (
                name: "FK_Titles_Admin_AdminEmpId",
                table: "Titles",
                column: "AdminEmpId",
                principalTable: "Admin",
                principalColumn: "empID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
