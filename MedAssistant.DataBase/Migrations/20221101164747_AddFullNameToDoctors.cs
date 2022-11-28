using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAssistant.DataBase.Migrations
{
    public partial class AddFullNameToDoctors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "SName",
                table: "Doctors",
                newName: "FullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Doctors",
                newName: "SName");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
