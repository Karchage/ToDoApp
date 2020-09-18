using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Migrations
{
    public partial class _upd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFor",
                table: "ToDo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDue",
                table: "ToDo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "ToDo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFor",
                table: "ToDo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDue",
                table: "ToDo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "ToDo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
