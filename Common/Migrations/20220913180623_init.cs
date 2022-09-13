using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxonomyPostTypes",
                table: "TaxonomyPostTypes");

            migrationBuilder.DropIndex(
                name: "IX_TaxonomyPostTypes_TaxonomyId",
                table: "TaxonomyPostTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TaxonomyPostTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxonomyPostTypes",
                table: "TaxonomyPostTypes",
                columns: new[] { "TaxonomyId", "PostTypeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxonomyPostTypes",
                table: "TaxonomyPostTypes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TaxonomyPostTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxonomyPostTypes",
                table: "TaxonomyPostTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaxonomyPostTypes_TaxonomyId",
                table: "TaxonomyPostTypes",
                column: "TaxonomyId");
        }
    }
}
