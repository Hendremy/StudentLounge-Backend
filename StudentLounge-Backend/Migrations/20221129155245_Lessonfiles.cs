using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class Lessonfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "LessonFiles",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "LessonFiles",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "LessonFiles",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LessonFiles",
                newName: "FileName");
        }
    }
}
