using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class addServicesContactUs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    ContId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empID = table.Column<int>(type: "int", nullable: false),
                    TextAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.ContId);
                    table.ForeignKey(
                        name: "FK_ContactUs_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesSite",
                columns: table => new
                {
                    SerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empID = table.Column<int>(type: "int", nullable: false),
                    ParphAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParphEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesSite", x => x.SerId);
                    table.ForeignKey(
                        name: "FK_ServicesSite_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_empID",
                table: "ContactUs",
                column: "empID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesSite_empID",
                table: "ServicesSite",
                column: "empID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "ServicesSite");
        }
    }
}
