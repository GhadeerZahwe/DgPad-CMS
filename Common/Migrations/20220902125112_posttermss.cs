using Microsoft.EntityFrameworkCore.Migrations;
namespace Common.Migrations
{
    public partial class posttermss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PostTerm",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    TermId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTerm", x => new { x.TermId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostTerm_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostTerm_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTerm_PostId",
                table: "PostTerm",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTerm");

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "Posts",
                type: "int",
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
    }
}
