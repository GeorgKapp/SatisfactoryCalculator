using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AnotherFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScannableObject_Item_ItemClassName",
                table: "ScannableObject");

            migrationBuilder.AlterColumn<string>(
                name: "ItemClassName",
                table: "ScannableObject",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "CreatureClassName",
                table: "ScannableObject",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScannableObject_CreatureClassName",
                table: "ScannableObject",
                column: "CreatureClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_ScannableObject_Creature_CreatureClassName",
                table: "ScannableObject",
                column: "CreatureClassName",
                principalTable: "Creature",
                principalColumn: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_ScannableObject_Item_ItemClassName",
                table: "ScannableObject",
                column: "ItemClassName",
                principalTable: "Item",
                principalColumn: "ClassName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScannableObject_Creature_CreatureClassName",
                table: "ScannableObject");

            migrationBuilder.DropForeignKey(
                name: "FK_ScannableObject_Item_ItemClassName",
                table: "ScannableObject");

            migrationBuilder.DropIndex(
                name: "IX_ScannableObject_CreatureClassName",
                table: "ScannableObject");

            migrationBuilder.DropColumn(
                name: "CreatureClassName",
                table: "ScannableObject");

            migrationBuilder.AlterColumn<string>(
                name: "ItemClassName",
                table: "ScannableObject",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScannableObject_Item_ItemClassName",
                table: "ScannableObject",
                column: "ItemClassName",
                principalTable: "Item",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
