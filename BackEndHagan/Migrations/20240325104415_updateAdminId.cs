using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class updateAdminId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Admin__E1C7EEED86D5AABA",
                table: "Admin");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Admin__E1C7EEED86D5AABA",
                table: "Admin",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Admin__E1C7EEED86D5AABA",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Admin");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Admin__E1C7EEED86D5AABA",
                table: "Admin",
                column: "empID");
        }
    }
}
