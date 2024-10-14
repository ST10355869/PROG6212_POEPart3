using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10355869_PROG6212_Part2.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimStatus",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimStatus",
                table: "Lecturers");
        }
    }
}
