using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class updateGuestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_DepositWays_DepositWayId",
                table: "Guests");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountByPercentage",
                table: "Guests",
                type: "decimal(3,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountByAmount",
                table: "Guests",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "DepositWayId",
                table: "Guests",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_DepositWays_DepositWayId",
                table: "Guests",
                column: "DepositWayId",
                principalTable: "DepositWays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_DepositWays_DepositWayId",
                table: "Guests");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountByPercentage",
                table: "Guests",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountByAmount",
                table: "Guests",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepositWayId",
                table: "Guests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_DepositWays_DepositWayId",
                table: "Guests",
                column: "DepositWayId",
                principalTable: "DepositWays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
