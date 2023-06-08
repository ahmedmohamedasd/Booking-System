using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class updateareaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockAreas_Areas_AreaName",
                table: "BlockAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedGuestAreas_Areas_AreaName",
                table: "BookedGuestAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookedGuestAreas",
                table: "BookedGuestAreas");

            migrationBuilder.DropIndex(
                name: "IX_BookedGuestAreas_AreaName",
                table: "BookedGuestAreas");

            migrationBuilder.DropIndex(
                name: "IX_BlockAreas_AreaName",
                table: "BlockAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "AreaName",
                table: "BookedGuestAreas");

            migrationBuilder.DropColumn(
                name: "AreaName",
                table: "BlockAreas");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Guests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "BookedGuestAreas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "BlockAreas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AreaName",
                table: "Areas",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Areas",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookedGuestAreas",
                table: "BookedGuestAreas",
                columns: new[] { "GuestId", "AreaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookedGuestAreas_AreaId",
                table: "BookedGuestAreas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockAreas_AreaId",
                table: "BlockAreas",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockAreas_Areas_AreaId",
                table: "BlockAreas",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedGuestAreas_Areas_AreaId",
                table: "BookedGuestAreas",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockAreas_Areas_AreaId",
                table: "BlockAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedGuestAreas_Areas_AreaId",
                table: "BookedGuestAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookedGuestAreas",
                table: "BookedGuestAreas");

            migrationBuilder.DropIndex(
                name: "IX_BookedGuestAreas_AreaId",
                table: "BookedGuestAreas");

            migrationBuilder.DropIndex(
                name: "IX_BlockAreas_AreaId",
                table: "BlockAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "BookedGuestAreas");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "BlockAreas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Areas");

            migrationBuilder.AddColumn<string>(
                name: "AreaName",
                table: "BookedGuestAreas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AreaName",
                table: "BlockAreas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AreaName",
                table: "Areas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookedGuestAreas",
                table: "BookedGuestAreas",
                columns: new[] { "GuestId", "AreaName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "AreaName");

            migrationBuilder.CreateIndex(
                name: "IX_BookedGuestAreas_AreaName",
                table: "BookedGuestAreas",
                column: "AreaName");

            migrationBuilder.CreateIndex(
                name: "IX_BlockAreas_AreaName",
                table: "BlockAreas",
                column: "AreaName");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockAreas_Areas_AreaName",
                table: "BlockAreas",
                column: "AreaName",
                principalTable: "Areas",
                principalColumn: "AreaName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedGuestAreas_Areas_AreaName",
                table: "BookedGuestAreas",
                column: "AreaName",
                principalTable: "Areas",
                principalColumn: "AreaName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
