using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class addSortingAndDeletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WhereYouFroms",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Sorting",
                table: "WhereYouFroms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HowYouKnowUss",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Sorting",
                table: "HowYouKnowUss",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WhereYouFroms");

            migrationBuilder.DropColumn(
                name: "Sorting",
                table: "WhereYouFroms");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HowYouKnowUss");

            migrationBuilder.DropColumn(
                name: "Sorting",
                table: "HowYouKnowUss");
        }
    }
}
