using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WastelandRilfeworks.Data.Migrations
{
    public partial class AddedSchematics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 8, 18, 33, 20, 161, DateTimeKind.Utc).AddTicks(9054),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 19, 14, 47, 53, 720, DateTimeKind.Utc).AddTicks(1076));

            migrationBuilder.AddColumn<int>(
                name: "WeaponSchematicId",
                table: "Weapons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Schematics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(205)", maxLength: 205, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schematics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_WeaponSchematicId",
                table: "Weapons",
                column: "WeaponSchematicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Schematics_WeaponSchematicId",
                table: "Weapons",
                column: "WeaponSchematicId",
                principalTable: "Schematics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Schematics_WeaponSchematicId",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "Schematics");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_WeaponSchematicId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "WeaponSchematicId",
                table: "Weapons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Weapons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 19, 14, 47, 53, 720, DateTimeKind.Utc).AddTicks(1076),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 8, 18, 33, 20, 161, DateTimeKind.Utc).AddTicks(9054));
        }
    }
}
