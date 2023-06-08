using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class BlockAreaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBlock = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    IsBlocked = table.Column<bool>(nullable: false),
                    AreaName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockAreas_Areas_AreaName",
                        column: x => x.AreaName,
                        principalTable: "Areas",
                        principalColumn: "AreaName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockAreas_AreaName",
                table: "BlockAreas",
                column: "AreaName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockAreas");
        }
    }
}
