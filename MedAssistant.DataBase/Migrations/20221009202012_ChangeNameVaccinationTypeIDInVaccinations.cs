using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAssistant.DataBase.Migrations
{
    public partial class ChangeNameVaccinationTypeIDInVaccinations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVaccinationType",
                table: "Vaccinations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdVaccinationType",
                table: "Vaccinations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
