using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class EditColumnIsCanceledToIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "DepositWays");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DepositWays",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DepositWays");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "DepositWays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
