using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class modifylessonsregiter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_AspNetUsers_usersId",
                table: "AppUserLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lessons_Lessonsid",
                table: "AppUserLesson");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Lessons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Lessons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "usersId",
                table: "AppUserLesson",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "Lessonsid",
                table: "AppUserLesson",
                newName: "LessonsId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserLesson_usersId",
                table: "AppUserLesson",
                newName: "IX_AppUserLesson_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_AspNetUsers_UsersId",
                table: "AppUserLesson",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_AppUserLesson_AspNetUsers_UsersId",
                table: "AppUserLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsId",
                table: "AppUserLesson");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lessons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lessons",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "AppUserLesson",
                newName: "usersId");

            migrationBuilder.RenameColumn(
                name: "LessonsId",
                table: "AppUserLesson",
                newName: "Lessonsid");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserLesson_UsersId",
                table: "AppUserLesson",
                newName: "IX_AppUserLesson_usersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_AspNetUsers_usersId",
                table: "AppUserLesson",
                column: "usersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_Lessons_Lessonsid",
                table: "AppUserLesson",
                column: "Lessonsid",
                principalTable: "Lessons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
