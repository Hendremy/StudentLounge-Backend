using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class Agenda_HasOnUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_AspNetUsers_AppUserId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_AppUserId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Agenda");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Agenda",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_UserId",
                table: "Agenda",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_AspNetUsers_UserId",
                table: "Agenda",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_AspNetUsers_UserId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_UserId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Agenda");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "AgendaEvent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Agenda",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
