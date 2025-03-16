using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class Terms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TextEn",
                table: "TermsAndConditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                 unicode: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TextAr",
                table: "TermsAndConditions",
                type: "nvarchar(max)",
                nullable: false,
                 unicode: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TextEn",
                table: "PriceAndRate",
                type: "nvarchar(max)",
                nullable: false,
                 unicode: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TextAr",
                table: "PriceAndRate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                 unicode: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TextEn",
                table: "TermsAndConditions",
                type: "nvarchar(max)",
                nullable: true,
                 unicode: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TextAr",
                table: "TermsAndConditions",
                type: "nvarchar(max)",
                nullable: true,
                 unicode: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TextEn",
                table: "PriceAndRate",
                type: "nvarchar(max)",
                nullable: true,
                 unicode: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TextAr",
                table: "PriceAndRate",
                type: "nvarchar(max)",
                nullable: true, unicode: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
