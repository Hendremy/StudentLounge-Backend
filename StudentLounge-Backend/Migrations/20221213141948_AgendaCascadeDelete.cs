using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class AgendaCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaEvent_Agenda_AgendaId",
                table: "AgendaEvent");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaId",
                table: "AgendaEvent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaEvent_Agenda_AgendaId",
                table: "AgendaEvent",
                column: "AgendaId",
                principalTable: "Agenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaEvent_Agenda_AgendaId",
                table: "AgendaEvent");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaId",
                table: "AgendaEvent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaEvent_Agenda_AgendaId",
                table: "AgendaEvent",
                column: "AgendaId",
                principalTable: "Agenda",
                principalColumn: "Id");
        }
    }
}
