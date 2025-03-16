using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class VatServiceTrainerIdPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Services",
                table: "Trainer",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VAT",
                table: "Trainer",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PaymentRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "PaymentRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Services",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VAT",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequest_TrainerId",
                table: "PaymentRequest",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequest_Trainer_TrainerId",
                table: "PaymentRequest",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "trnId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequest_Trainer_TrainerId",
                table: "PaymentRequest");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequest_TrainerId",
                table: "PaymentRequest");

            migrationBuilder.DropColumn(
                name: "Services",
                table: "Trainer");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Trainer");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "PaymentRequest");

            migrationBuilder.DropColumn(
                name: "Services",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PaymentRequest",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
