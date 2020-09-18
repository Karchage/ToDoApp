using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Migrations
{
    public partial class _upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ToDo");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ToDo",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "ToDo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDue",
                table: "ToDo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFor",
                table: "ToDo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ToDo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "ToDo");

            migrationBuilder.DropColumn(
                name: "DateDue",
                table: "ToDo");

            migrationBuilder.DropColumn(
                name: "DateFor",
                table: "ToDo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDo",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ToDo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
