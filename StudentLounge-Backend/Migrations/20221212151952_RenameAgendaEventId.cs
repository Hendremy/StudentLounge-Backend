using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class RenameAgendaEventId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Agenda_AgendaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AgendaId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaEvent",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "AgendaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AgendaEvent");

            migrationBuilder.AddColumn<string>(
                name: "sId",
                table: "AgendaEvent",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "AgendaEvent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "AgendaEvent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndHour",
                table: "AgendaEvent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GroupLabel",
                table: "AgendaEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "AgendaEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartHour",
                table: "AgendaEvent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Agenda",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaEvent",
                table: "AgendaEvent",
                column: "sId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_AppUserId",
                table: "Agenda",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_AspNetUsers_AppUserId",
                table: "Agenda",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_AspNetUsers_AppUserId",
                table: "Agenda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaEvent",
                table: "AgendaEvent");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_AppUserId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "sId",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "GroupLabel",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Agenda");

            migrationBuilder.AddColumn<int>(
                name: "AgendaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AgendaEvent",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaEvent",
                table: "AgendaEvent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AgendaId",
                table: "AspNetUsers",
                column: "AgendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Agenda_AgendaId",
                table: "AspNetUsers",
                column: "AgendaId",
                principalTable: "Agenda",
                principalColumn: "Id");
        }
    }
}
