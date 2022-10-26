using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAssistant.DataBase.Migrations
{
    public partial class ChangeUserFORFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SName",
                table: "Users",
                newName: "FullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "SName");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
