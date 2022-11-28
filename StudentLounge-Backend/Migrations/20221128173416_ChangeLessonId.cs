using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class ChangeLessonId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsId",
                table: "AppUserLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserLesson",
                table: "AppUserLesson");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonsId",
                table: "AppUserLesson");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Firstname");

            migrationBuilder.AddColumn<string>(
                name: "LessonId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LessonsLessonId",
                table: "AppUserLesson",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserLesson",
                table: "AppUserLesson",
                columns: new[] { "LessonsLessonId", "UsersId" });

            migrationBuilder.CreateTable(
                name: "LessonFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonFiles_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonFiles_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId");
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "LessonId", "Name" },
                values: new object[,]
                {
                    { "70c02712-6f41-11ed-a1eb-0242ac120002", "Mathématiques" },
                    { "7b4b00ee-6f41-11ed-a1eb-0242ac120002", "Informatique" },
                    { "7b4b053a-6f41-11ed-a1eb-0242ac120002", "Cybersécurité" },
                    { "7b4b0684-6f41-11ed-a1eb-0242ac120002", "Anglais" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonFiles_AuthorId",
                table: "LessonFiles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonFiles_LessonId",
                table: "LessonFiles",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsLessonId",
                table: "AppUserLesson",
                column: "LessonsLessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLesson_Lessons_LessonsLessonId",
                table: "AppUserLesson");

            migrationBuilder.DropTable(
                name: "LessonFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserLesson",
                table: "AppUserLesson");

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonId",
                keyColumnType: "nvarchar(450)",
                keyValue: "70c02712-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonId",
                keyColumnType: "nvarchar(450)",
                keyValue: "7b4b00ee-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonId",
                keyColumnType: "nvarchar(450)",
                keyValue: "7b4b053a-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "LessonId",
                keyColumnType: "nvarchar(450)",
                keyValue: "7b4b0684-6f41-11ed-a1eb-0242ac120002");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonsLessonId",
                table: "AppUserLesson");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LessonsId",
                table: "AppUserLesson",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserLesson",
                table: "AppUserLesson",
                columns: new[] { "LessonsId", "UsersId" });

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
