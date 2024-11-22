using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10355869_PROG6212_Part2.Migrations
{
    /// <inheritdoc />
    public partial class backToNormal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Address",
            //    table: "Lecturers");

            //migrationBuilder.DropColumn(
            //    name: "Name",
            //    table: "Lecturers");

            //migrationBuilder.DropColumn(
            //    name: "PhoneNumber",
            //    table: "Lecturers");

            //migrationBuilder.DropColumn(
            //    name: "Surname",
            //    table: "Lecturers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
