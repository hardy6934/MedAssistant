using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace MedAssistant.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class RemoveManufactureFromMedecine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Monufacturer",
                table: "Medicines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Monufacturer",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
