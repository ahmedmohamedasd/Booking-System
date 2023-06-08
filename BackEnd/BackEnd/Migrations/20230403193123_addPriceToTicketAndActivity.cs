using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class addPriceToTicketAndActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceForAdult",
                table: "GuestTickets",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceForKids",
                table: "GuestTickets",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ActivityPrice",
                table: "GuestActivities",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForAdult",
                table: "GuestTickets");

            migrationBuilder.DropColumn(
                name: "PriceForKids",
                table: "GuestTickets");

            migrationBuilder.DropColumn(
                name: "ActivityPrice",
                table: "GuestActivities");
        }
    }
}
