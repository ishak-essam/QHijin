using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class GentTrn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    trnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    empId = table.Column<int>(type: "int", nullable: false),
                    trnImgLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trnImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trnCV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rpayment_link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rentmoney = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.trnId);
                    table.ForeignKey(
                        name: "FK_Trainer_Employees_empId",
                        column: x => x.empId,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trainer_Items_itemId",
                        column: x => x.itemId,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainer_empId",
                table: "Trainer",
                column: "empId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainer_itemId",
                table: "Trainer",
                column: "itemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trainer");
        }
    }
}
