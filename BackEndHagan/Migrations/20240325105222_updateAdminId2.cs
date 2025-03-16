using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class updateAdminId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Admin__E1C7EEED86D5AABA",
                table: "Admin");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                table: "Admin",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                table: "Admin");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Admin__E1C7EEED86D5AABA",
                table: "Admin",
                column: "Id");
        }
    }
}
