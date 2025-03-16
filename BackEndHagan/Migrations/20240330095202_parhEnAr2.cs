using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class parhEnAr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string> (
          name: "SubTitleAr",
          table: "Banners",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "");
            migrationBuilder.AddColumn<string> (
              name: "SubTitleEn",
              table: "Banners",
              type: "nvarchar(max)",
              nullable: false,
              defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn (
                name: "SubTitleAr",
                table: "Banners");

            migrationBuilder.DropColumn (
                name: "SubTitleEn",
                table: "Banners");

        }
    }
}
