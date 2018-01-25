using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User_Dashboard.Migrations
{
    public partial class bidder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bidder",
                table: "Auction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bidder",
                table: "Auction");
        }
    }
}
