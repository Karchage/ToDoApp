using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Migrations
{
    public partial class updtabetodo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoginUser",
                table: "ToDo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginUser",
                table: "ToDo");
        }
    }
}
