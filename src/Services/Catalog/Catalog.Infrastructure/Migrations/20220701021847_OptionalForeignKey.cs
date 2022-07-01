using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    public partial class OptionalForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryForeignKey",
                table: "Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryForeignKey",
                table: "Categories",
                column: "ParentCategoryForeignKey",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryForeignKey",
                table: "Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryForeignKey",
                table: "Categories",
                column: "ParentCategoryForeignKey",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
