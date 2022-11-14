using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class Add_Lessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lessons_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_AppUserId",
                table: "Lessons",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessons");
        }
    }
}
