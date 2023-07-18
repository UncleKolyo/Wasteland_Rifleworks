using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WastelandRilfeworks.Data.Migrations
{
    public partial class AddedTitleImagePathStringToWeapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 18, 14, 36, 50, 828, DateTimeKind.Utc).AddTicks(9513),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 18, 14, 2, 16, 215, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.AddColumn<string>(
                name: "TitleImagePathString",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleImagePathString",
                table: "Weapons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 18, 14, 2, 16, 215, DateTimeKind.Utc).AddTicks(1064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 18, 14, 36, 50, 828, DateTimeKind.Utc).AddTicks(9513));
        }
    }
}
