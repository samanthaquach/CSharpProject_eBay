using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User_Dashboard.Migrations
{
    public partial class daysleft3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DaysLeft",
                table: "Auction",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DaysLeft",
                table: "Auction",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
