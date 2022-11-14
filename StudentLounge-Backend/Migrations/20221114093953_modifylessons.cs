using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class modifylessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_AppUserId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_AppUserId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Lessons");

            migrationBuilder.CreateTable(
                name: "AppUserLesson",
                columns: table => new
                {
                    Lessonsid = table.Column<int>(type: "int", nullable: false),
                    usersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLesson", x => new { x.Lessonsid, x.usersId });
                    table.ForeignKey(
                        name: "FK_AppUserLesson_AspNetUsers_usersId",
                        column: x => x.usersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserLesson_Lessons_Lessonsid",
                        column: x => x.Lessonsid,
                        principalTable: "Lessons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLesson_usersId",
                table: "AppUserLesson",
                column: "usersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserLesson");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_AppUserId",
                table: "Lessons",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_AppUserId",
                table: "Lessons",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
