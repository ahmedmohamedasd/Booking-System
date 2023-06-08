using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class AddGuestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    DiscountByPercentage = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    DiscountByAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateOfDeposit = table.Column<DateTime>(nullable: false),
                    DateOfBooking = table.Column<DateTime>(nullable: false),
                    PaymentCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentVisa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitNote = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    TaxIncluded = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    LeftToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KnowUsId = table.Column<int>(nullable: true),
                    WhereYouId = table.Column<int>(nullable: true),
                    BookingTypeId = table.Column<int>(nullable: false),
                    DepositWayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_BookingTypes_BookingTypeId",
                        column: x => x.BookingTypeId,
                        principalTable: "BookingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guests_DepositWays_DepositWayId",
                        column: x => x.DepositWayId,
                        principalTable: "DepositWays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guests_HowYouKnowUss_KnowUsId",
                        column: x => x.KnowUsId,
                        principalTable: "HowYouKnowUss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guests_WhereYouFroms_WhereYouId",
                        column: x => x.WhereYouId,
                        principalTable: "WhereYouFroms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_BookingTypeId",
                table: "Guests",
                column: "BookingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_DepositWayId",
                table: "Guests",
                column: "DepositWayId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_KnowUsId",
                table: "Guests",
                column: "KnowUsId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_WhereYouId",
                table: "Guests",
                column: "WhereYouId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");
        }
    }
}
