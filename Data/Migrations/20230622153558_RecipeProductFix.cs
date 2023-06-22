using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RecipeProductFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProduct_Item_ItemClassName",
                table: "RecipeProduct");

            migrationBuilder.AlterColumn<string>(
                name: "ItemClassName",
                table: "RecipeProduct",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "BuildingClassName",
                table: "RecipeProduct",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProduct_BuildingClassName",
                table: "RecipeProduct",
                column: "BuildingClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProduct_Building_BuildingClassName",
                table: "RecipeProduct",
                column: "BuildingClassName",
                principalTable: "Building",
                principalColumn: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProduct_Item_ItemClassName",
                table: "RecipeProduct",
                column: "ItemClassName",
                principalTable: "Item",
                principalColumn: "ClassName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProduct_Building_BuildingClassName",
                table: "RecipeProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeProduct_Item_ItemClassName",
                table: "RecipeProduct");

            migrationBuilder.DropIndex(
                name: "IX_RecipeProduct_BuildingClassName",
                table: "RecipeProduct");

            migrationBuilder.DropColumn(
                name: "BuildingClassName",
                table: "RecipeProduct");

            migrationBuilder.AlterColumn<string>(
                name: "ItemClassName",
                table: "RecipeProduct",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeProduct_Item_ItemClassName",
                table: "RecipeProduct",
                column: "ItemClassName",
                principalTable: "Item",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
