using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class ItemPh2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemPhysical_ItemPhysicalId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemPhysicalId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemPhysicalId",
                table: "Items");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPhysical_ItemId",
                table: "ItemPhysical",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPhysical_Items_ItemId",
                table: "ItemPhysical",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPhysical_Items_ItemId",
                table: "ItemPhysical");

            migrationBuilder.DropIndex(
                name: "IX_ItemPhysical_ItemId",
                table: "ItemPhysical");

            migrationBuilder.AddColumn<int>(
                name: "ItemPhysicalId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
