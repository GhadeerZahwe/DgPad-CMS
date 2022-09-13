using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class Postterm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TermId",
                table: "Posts",
                column: "TermId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Terms_TermId",
                table: "Posts",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Terms_TermId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_TermId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "Posts");
        }
    }
}
