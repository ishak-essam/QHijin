using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class addAds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    AdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdsImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdsImgLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdsType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AdIdAnd__46655E2E38A279F4", x => x.AdId);
                    table.ForeignKey(
                        name: "FK_Ads_Employees_empID",
                        column: x => x.empID,
                        principalTable: "Employees",
                        principalColumn: "empID",
                        onDelete: ReferentialAction.Cascade);
                });



            migrationBuilder.CreateIndex(
                name: "IX_Ads_empID",
                table: "Ads",
                column: "empID");

           

       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");

  
        }
    }
}
