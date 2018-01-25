using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User_Dashboard.Migrations
{
    public partial class bid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HighestBid",
                table: "Auction",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighestBid",
                table: "Auction");
        }
    }
}
