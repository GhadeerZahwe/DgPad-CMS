using Microsoft.EntityFrameworkCore.Migrations;

namespace dgPadCms.Migrations
{
    public partial class newFrnKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaxonomyId",
                table: "Terms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Terms_TaxonomyId",
                table: "Terms",
                column: "TaxonomyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Terms_Taxonomies_TaxonomyId",
                table: "Terms",
                column: "TaxonomyId",
                principalTable: "Taxonomies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terms_Taxonomies_TaxonomyId",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Terms_TaxonomyId",
                table: "Terms");

            migrationBuilder.DropColumn(
                name: "TaxonomyId",
                table: "Terms");
        }
    }
}
