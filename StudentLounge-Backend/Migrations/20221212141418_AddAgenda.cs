using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class AddAgenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgendaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgendaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaEvent_Agenda_AgendaId",
                        column: x => x.AgendaId,
                        principalTable: "Agenda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AgendaId",
                table: "AspNetUsers",
                column: "AgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaEvent_AgendaId",
                table: "AgendaEvent",
                column: "AgendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Agenda_AgendaId",
                table: "AspNetUsers",
                column: "AgendaId",
                principalTable: "Agenda",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Agenda_AgendaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AgendaEvent");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AgendaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AgendaId",
                table: "AspNetUsers");
        }
    }
}
