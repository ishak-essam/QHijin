using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class ItemPh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemPhysicalId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VideoLocalPath",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ItemPhysical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eye = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    front = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    back = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPhysical", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemPhysicalId",
                table: "Items",
                column: "ItemPhysicalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemPhysical_ItemPhysicalId",
                table: "Items",
                column: "ItemPhysicalId",
                principalTable: "ItemPhysical",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemPhysical_ItemPhysicalId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemPhysical");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemPhysicalId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemPhysicalId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "VideoLocalPath",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Items");
        }
    }
}
