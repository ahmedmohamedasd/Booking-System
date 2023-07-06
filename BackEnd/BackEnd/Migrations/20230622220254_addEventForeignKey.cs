using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class addEventForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "GuestEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GuestEvents_EventId",
                table: "GuestEvents",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestEvents_Events_EventId",
                table: "GuestEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestEvents_Events_EventId",
                table: "GuestEvents");

            migrationBuilder.DropIndex(
                name: "IX_GuestEvents_EventId",
                table: "GuestEvents");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "GuestEvents");
        }
    }
}
