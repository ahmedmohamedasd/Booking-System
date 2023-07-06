using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class GuestEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DiscountByPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountByAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateOfDeposit = table.Column<DateTime>(nullable: true),
                    DateOfBooking = table.Column<DateTime>(nullable: false),
                    PaymentCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentVisa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeftToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EventPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCountOfguest = table.Column<int>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: true),
                    KnowUsId = table.Column<int>(nullable: true),
                    WhereYouId = table.Column<int>(nullable: true),
                    DepositWayId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestEvents_DepositWays_DepositWayId",
                        column: x => x.DepositWayId,
                        principalTable: "DepositWays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestEvents_HowYouKnowUss_KnowUsId",
                        column: x => x.KnowUsId,
                        principalTable: "HowYouKnowUss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestEvents_WhereYouFroms_WhereYouId",
                        column: x => x.WhereYouId,
                        principalTable: "WhereYouFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestEvents_DepositWayId",
                table: "GuestEvents",
                column: "DepositWayId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestEvents_KnowUsId",
                table: "GuestEvents",
                column: "KnowUsId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestEvents_WhereYouId",
                table: "GuestEvents",
                column: "WhereYouId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestEvents");
        }
    }
}
