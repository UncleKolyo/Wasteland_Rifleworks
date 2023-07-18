using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WastelandRilfeworks.Data.Migrations
{
    public partial class AddedCreateTimeToWeapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 18, 14, 2, 16, 215, DateTimeKind.Utc).AddTicks(1064));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Weapons");
        }
    }
}
