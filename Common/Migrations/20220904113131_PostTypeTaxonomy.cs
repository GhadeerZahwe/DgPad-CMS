using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class PostTypeTaxonomy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostType_Taxonomies_TaxonomyId",
                table: "PostType");

            migrationBuilder.DropIndex(
                name: "IX_PostType_TaxonomyId",
                table: "PostType");

            migrationBuilder.DropColumn(
                name: "TaxonomyId",
                table: "PostType");

            migrationBuilder.CreateTable(
                name: "TaxonomyPostTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxonomyId = table.Column<int>(nullable: false),
                    PostTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxonomyPostTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxonomyPostTypes_PostType_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxonomyPostTypes_Taxonomies_TaxonomyId",
                        column: x => x.TaxonomyId,
                        principalTable: "Taxonomies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyPostTypes_PostTypeId",
                table: "TaxonomyPostTypes",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyPostTypes_TaxonomyId",
                table: "TaxonomyPostTypes",
                column: "TaxonomyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxonomyPostTypes");

            migrationBuilder.AddColumn<int>(
                name: "TaxonomyId",
                table: "PostType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PostType_TaxonomyId",
                table: "PostType",
                column: "TaxonomyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostType_Taxonomies_TaxonomyId",
                table: "PostType",
                column: "TaxonomyId",
                principalTable: "Taxonomies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
