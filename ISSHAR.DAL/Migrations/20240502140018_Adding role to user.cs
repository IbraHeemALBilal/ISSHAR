using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISSHAR.DAL.Migrations
{
    public partial class Addingroletouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CardTemplets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 2, 17, 0, 18, 648, DateTimeKind.Local).AddTicks(858),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 1, 18, 34, 14, 304, DateTimeKind.Local).AddTicks(4236));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CardTemplets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 1, 18, 34, 14, 304, DateTimeKind.Local).AddTicks(4236),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 2, 17, 0, 18, 648, DateTimeKind.Local).AddTicks(858));
        }
    }
}
