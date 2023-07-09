using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FactoryConfigMoreDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlternateRecipeClassName",
                table: "FactoryConfigurationOutput",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "FactoryConfigurationOutput",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BuildingAmount",
                table: "FactoryConfigurationOutput",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FactoryConfigurationOutput_AlternateRecipeClassName",
                table: "FactoryConfigurationOutput",
                column: "AlternateRecipeClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryConfigurationOutput_Recipe_AlternateRecipeClassName",
                table: "FactoryConfigurationOutput",
                column: "AlternateRecipeClassName",
                principalTable: "Recipe",
                principalColumn: "ClassName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactoryConfigurationOutput_Recipe_AlternateRecipeClassName",
                table: "FactoryConfigurationOutput");

            migrationBuilder.DropIndex(
                name: "IX_FactoryConfigurationOutput_AlternateRecipeClassName",
                table: "FactoryConfigurationOutput");

            migrationBuilder.DropColumn(
                name: "AlternateRecipeClassName",
                table: "FactoryConfigurationOutput");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "FactoryConfigurationOutput");

            migrationBuilder.DropColumn(
                name: "BuildingAmount",
                table: "FactoryConfigurationOutput");
        }
    }
}
