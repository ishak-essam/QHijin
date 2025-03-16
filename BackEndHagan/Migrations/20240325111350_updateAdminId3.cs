using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class updateAdminId3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ__Admin__AFB3EC6C0CBA2A3A",
                table: "Admin",
                newName: "IX_Admin_empID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Admin_empID",
                table: "Admin",
                newName: "UQ__Admin__AFB3EC6C0CBA2A3A");
        }
    }
}
