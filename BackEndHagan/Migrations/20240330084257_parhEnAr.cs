using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class parhEnAr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParphAr",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
      
            migrationBuilder.AddColumn<string>(
                name: "ParphEn",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParphAr",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "ParphEn",
                table: "Banners");
        }
    }
}
