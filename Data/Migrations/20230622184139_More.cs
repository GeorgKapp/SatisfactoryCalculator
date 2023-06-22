using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class More : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor");

            migrationBuilder.AlterColumn<int>(
                name: "ScannableObjectID",
                table: "ScanningActor",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor",
                column: "ScannableObjectID",
                principalTable: "ScannableObject",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor");

            migrationBuilder.AlterColumn<int>(
                name: "ScannableObjectID",
                table: "ScanningActor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor",
                column: "ScannableObjectID",
                principalTable: "ScannableObject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
