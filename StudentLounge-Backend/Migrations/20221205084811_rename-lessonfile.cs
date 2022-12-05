using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class renamelessonfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LessonFiles",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "LessonFiles",
                newName: "FilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LessonFiles",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "LessonFiles",
                newName: "FilePath");
        }
    }
}
