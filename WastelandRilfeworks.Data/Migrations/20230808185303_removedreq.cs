using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WastelandRilfeworks.Data.Migrations
{
    public partial class removedreq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 8, 18, 53, 3, 585, DateTimeKind.Utc).AddTicks(8389),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 8, 18, 39, 29, 158, DateTimeKind.Utc).AddTicks(6045));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 8, 18, 39, 29, 158, DateTimeKind.Utc).AddTicks(6045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 8, 18, 53, 3, 585, DateTimeKind.Utc).AddTicks(8389));
        }
    }
}
