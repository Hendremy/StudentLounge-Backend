using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class Droplessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsId",
                table: "AppUserLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "Lesson");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Firstname");

            migrationBuilder.AlterColumn<string>(
                name: "LessonsId",
                table: "AppUserLesson",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Lesson",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LessonFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonFile_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonFile_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lesson",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "70c02712-6f41-11ed-a1eb-0242ac120002", "Mathématiques" },
                    { "7b4b00ee-6f41-11ed-a1eb-0242ac120002", "Informatique" },
                    { "7b4b053a-6f41-11ed-a1eb-0242ac120002", "Cybersécurité" },
                    { "7b4b0684-6f41-11ed-a1eb-0242ac120002", "Anglais" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonFile_AuthorId",
                table: "LessonFile",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonFile_LessonId",
                table: "LessonFile",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_Lesson_LessonsId",
                table: "AppUserLesson",
                column: "LessonsId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lesson_LessonsId",
                table: "AppUserLesson");

            migrationBuilder.DropTable(
                name: "LessonFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.DeleteData(
                table: "Lesson",
                keyColumn: "Id",
                keyValue: "70c02712-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.DeleteData(
                table: "Lesson",
                keyColumn: "Id",
                keyValue: "7b4b00ee-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.DeleteData(
                table: "Lesson",
                keyColumn: "Id",
                keyValue: "7b4b053a-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.DeleteData(
                table: "Lesson",
                keyColumn: "Id",
                keyValue: "7b4b0684-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.RenameTable(
                name: "Lesson",
                newName: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AlterColumn<int>(
                name: "LessonsId",
                table: "AppUserLesson",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Lessons",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsId",
                table: "AppUserLesson",
                column: "LessonsId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
