using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class generateFiveTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracting_Policy",
                columns: table => new
                {
                    con_pNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empID = table.Column<int>(type: "int", nullable: false),
                    titleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracting_Policy", x => x.con_pNo);
                    table.ForeignKey(
                        name: "FK_Contracting_Policy_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivery_Period",
                columns: table => new
                {
                    d_pN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empID = table.Column<int>(type: "int", nullable: false),
                    titleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery_Period", x => x.d_pN);
                    table.ForeignKey(
                        name: "FK_Delivery_Period_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FBId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    textEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FBId);
                    table.ForeignKey(
                        name: "FK_Feedback_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HowTobuy",
                columns: table => new
                {
                    HowBuyN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empID = table.Column<int>(type: "int", nullable: false),
                    titleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HowTobuy", x => x.HowBuyN);
                    table.ForeignKey(
                        name: "FK_HowTobuy_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Policy_Refund",
                columns: table => new
                {
                    p_refN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empID = table.Column<int>(type: "int", nullable: false),
                    titleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy_Refund", x => x.p_refN);
                    table.ForeignKey(
                        name: "FK_Policy_Refund_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracting_Policy_empID",
                table: "Contracting_Policy",
                column: "empID");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Period_empID",
                table: "Delivery_Period",
                column: "empID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserId",
                table: "Feedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HowTobuy_empID",
                table: "HowTobuy",
                column: "empID");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_Refund_empID",
                table: "Policy_Refund",
                column: "empID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracting_Policy");

            migrationBuilder.DropTable(
                name: "Delivery_Period");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "HowTobuy");

            migrationBuilder.DropTable(
                name: "Policy_Refund");
        }
    }
}
