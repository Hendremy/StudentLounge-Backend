using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class RenameLessonId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsLessonId",
                table: "AppUserLesson");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Lessons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LessonsLessonId",
                table: "AppUserLesson",
                newName: "LessonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsId",
                table: "AppUserLesson",
                column: "LessonsId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsId",
                table: "AppUserLesson");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lessons",
                newName: "LessonId");

            migrationBuilder.RenameColumn(
                name: "LessonsId",
                table: "AppUserLesson",
                newName: "LessonsLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsLessonId",
                table: "AppUserLesson",
                column: "LessonsLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
