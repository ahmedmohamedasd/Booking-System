using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class AddBookedGuestArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookedGuestAreas",
                columns: table => new
                {
                    GuestId = table.Column<int>(nullable: false),
                    AreaName = table.Column<string>(nullable: false),
                    DateOfBooking = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedGuestAreas", x => new { x.GuestId, x.AreaName });
                    table.ForeignKey(
                        name: "FK_BookedGuestAreas_Areas_AreaName",
                        column: x => x.AreaName,
                        principalTable: "Areas",
                        principalColumn: "AreaName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookedGuestAreas_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookedGuestAreas_AreaName",
                table: "BookedGuestAreas",
                column: "AreaName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedGuestAreas");
        }
    }
}
