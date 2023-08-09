using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WastelandRilfeworks.Data.Migrations
{
    public partial class AddedFullDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Weapons",
                newName: "ShortDescription");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 8, 18, 39, 29, 158, DateTimeKind.Utc).AddTicks(6045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 8, 18, 33, 20, 161, DateTimeKind.Utc).AddTicks(9054));

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Weapons");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Weapons",
                newName: "Description");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 8, 18, 33, 20, 161, DateTimeKind.Utc).AddTicks(9054),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 8, 18, 39, 29, 158, DateTimeKind.Utc).AddTicks(6045));
        }
    }
}
