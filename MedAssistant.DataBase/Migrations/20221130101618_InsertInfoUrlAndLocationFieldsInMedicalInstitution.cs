using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAssistant.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class InsertInfoUrlAndLocationFieldsInMedicalInstitution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfoUrl",
                table: "MedicalInstitutions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "MedicalInstitutions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoUrl",
                table: "MedicalInstitutions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "MedicalInstitutions");
        }
    }
}
