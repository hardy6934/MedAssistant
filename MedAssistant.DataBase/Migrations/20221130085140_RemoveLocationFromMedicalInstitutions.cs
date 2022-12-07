using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAssistant.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLocationFromMedicalInstitutions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "MedicalInstitutions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "MedicalInstitutions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
