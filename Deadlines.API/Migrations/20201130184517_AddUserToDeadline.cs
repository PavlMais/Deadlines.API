using Microsoft.EntityFrameworkCore.Migrations;

namespace Deadlines.API.Migrations
{
    public partial class AddUserToDeadline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DbUserId",
                table: "Deadlines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "DbUserId1",
                table: "Deadlines",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deadlines_DbUserId1",
                table: "Deadlines",
                column: "DbUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Deadlines_AspNetUsers_DbUserId1",
                table: "Deadlines",
                column: "DbUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deadlines_AspNetUsers_DbUserId1",
                table: "Deadlines");

            migrationBuilder.DropIndex(
                name: "IX_Deadlines_DbUserId1",
                table: "Deadlines");

            migrationBuilder.DropColumn(
                name: "DbUserId",
                table: "Deadlines");

            migrationBuilder.DropColumn(
                name: "DbUserId1",
                table: "Deadlines");
        }
    }
}
