using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class AddTutorInTutoringDTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tutorings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TutoredId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutorings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutorings_AspNetUsers_TutoredId",
                        column: x => x.TutoredId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tutorings_AspNetUsers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tutorings_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tutorings_LessonId",
                table: "Tutorings",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorings_TutoredId",
                table: "Tutorings",
                column: "TutoredId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorings_TutorId",
                table: "Tutorings",
                column: "TutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tutorings");

            migrationBuilder.CreateTable(
                name: "Tutorats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TutoredId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TutorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutorats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutorats_AspNetUsers_TutoredId",
                        column: x => x.TutoredId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tutorats_AspNetUsers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tutorats_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tutorats_LessonId",
                table: "Tutorats",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorats_TutoredId",
                table: "Tutorats",
                column: "TutoredId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutorats_TutorId",
                table: "Tutorats",
                column: "TutorId");
        }
    }
}
