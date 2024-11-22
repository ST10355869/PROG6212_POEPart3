using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10355869_PROG6212_Part2.Migrations
{
    /// <inheritdoc />
    public partial class tryAddEDITModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_editLecturerModels",
                table: "editLecturerModels");

            migrationBuilder.RenameTable(
                name: "editLecturerModels",
                newName: "EditLecturerModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EditLecturerModels",
                table: "EditLecturerModels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EditLecturerModels",
                table: "EditLecturerModels");

            migrationBuilder.RenameTable(
                name: "EditLecturerModels",
                newName: "editLecturerModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_editLecturerModels",
                table: "editLecturerModels",
                column: "Id");
        }
    }
}
