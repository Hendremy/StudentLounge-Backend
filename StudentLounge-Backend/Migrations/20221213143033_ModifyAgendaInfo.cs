using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class ModifyAgendaInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Label",
                table: "AgendaEvent",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "GroupLabel",
                table: "AgendaEvent",
                newName: "Location");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AgendaEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Agenda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Agenda");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "AgendaEvent",
                newName: "Label");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "AgendaEvent",
                newName: "GroupLabel");
        }
    }
}
