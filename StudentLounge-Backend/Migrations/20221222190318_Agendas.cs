using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class Agendas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_AspNetUsers_UserId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_AgendaEvent_Agenda_AgendaId",
                table: "AgendaEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonFiles_AspNetUsers_AuthorId",
                table: "LessonFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agenda",
                table: "Agenda");

            migrationBuilder.RenameTable(
                name: "Agenda",
                newName: "Agendas");

            migrationBuilder.RenameIndex(
                name: "IX_Agenda_UserId",
                table: "Agendas",
                newName: "IX_Agendas_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "LessonFiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Agendas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendas",
                table: "Agendas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaEvent_Agendas_AgendaId",
                table: "AgendaEvent",
                column: "AgendaId",
                principalTable: "Agendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_AspNetUsers_UserId",
                table: "Agendas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFiles_AspNetUsers_AuthorId",
                table: "LessonFiles",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaEvent_Agendas_AgendaId",
                table: "AgendaEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_AspNetUsers_UserId",
                table: "Agendas");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonFiles_AspNetUsers_AuthorId",
                table: "LessonFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendas",
                table: "Agendas");

            migrationBuilder.RenameTable(
                name: "Agendas",
                newName: "Agenda");

            migrationBuilder.RenameIndex(
                name: "IX_Agendas_UserId",
                table: "Agenda",
                newName: "IX_Agenda_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "LessonFiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Agenda",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agenda",
                table: "Agenda",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_AspNetUsers_UserId",
                table: "Agenda",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaEvent_Agenda_AgendaId",
                table: "AgendaEvent",
                column: "AgendaId",
                principalTable: "Agenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFiles_AspNetUsers_AuthorId",
                table: "LessonFiles",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
