﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QHijin.Migrations
{
    /// <inheritdoc />
    public partial class addStatusTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Trainer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Trainer");
        }
    }
}
