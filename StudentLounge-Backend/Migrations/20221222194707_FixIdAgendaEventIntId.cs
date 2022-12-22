using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentLounge_Backend.Migrations
{
    public partial class FixIdAgendaEventIntId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaEvent",
                table: "AgendaEvent");

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "AgendaEvent",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaEvent",
                table: "AgendaEvent");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AgendaEvent");

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "AgendaEvent",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaEvent",
                table: "AgendaEvent",
                column: "EventId");
        }
    }
}
