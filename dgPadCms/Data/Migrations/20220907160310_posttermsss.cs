using Microsoft.EntityFrameworkCore.Migrations;

namespace dgPadCms.Migrations
{
    public partial class posttermsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTerm_Posts_PostId",
                table: "PostTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTerm_Terms_TermId",
                table: "PostTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTerm",
                table: "PostTerm");

            migrationBuilder.RenameTable(
                name: "PostTerm",
                newName: "PostTerms");

            migrationBuilder.RenameIndex(
                name: "IX_PostTerm_PostId",
                table: "PostTerms",
                newName: "IX_PostTerms_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTerms",
                table: "PostTerms",
                columns: new[] { "TermId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTerms_Posts_PostId",
                table: "PostTerms",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTerms_Terms_TermId",
                table: "PostTerms",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTerms_Posts_PostId",
                table: "PostTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTerms_Terms_TermId",
                table: "PostTerms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTerms",
                table: "PostTerms");

            migrationBuilder.RenameTable(
                name: "PostTerms",
                newName: "PostTerm");

            migrationBuilder.RenameIndex(
                name: "IX_PostTerms_PostId",
                table: "PostTerm",
                newName: "IX_PostTerm_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTerm",
                table: "PostTerm",
                columns: new[] { "TermId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTerm_Posts_PostId",
                table: "PostTerm",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTerm_Terms_TermId",
                table: "PostTerm",
                column: "TermId",
                principalTable: "Terms",
                principalColumn: "Id");
        }
    }
}
