using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISSHAR.DAL.Migrations
{
    public partial class AddingStatusToAdsAndHalls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Halls",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CardTemplets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 1, 18, 34, 14, 304, DateTimeKind.Local).AddTicks(4236),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 27, 18, 51, 17, 801, DateTimeKind.Local).AddTicks(2128));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Advertisements",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisements");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CardTemplets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 27, 18, 51, 17, 801, DateTimeKind.Local).AddTicks(2128),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 1, 18, 34, 14, 304, DateTimeKind.Local).AddTicks(4236));
        }
    }
}
