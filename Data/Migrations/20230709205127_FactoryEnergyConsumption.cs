using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FactoryEnergyConsumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactoryBuildingConfiguration_Building_ProducedBuildingClassName",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryBuildingConfiguration_Item_ProducedItemClassName",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_FactoryBuildingConfiguration_ProducedBuildingClassName",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.DropColumn(
                name: "ProducedBuildingClassName",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.AlterColumn<string>(
                name: "ProducedItemClassName",
                table: "FactoryBuildingConfiguration",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EnergyConsumption",
                table: "FactoryBuildingConfiguration",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryBuildingConfiguration_Item_ProducedItemClassName",
                table: "FactoryBuildingConfiguration",
                column: "ProducedItemClassName",
                principalTable: "Item",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactoryBuildingConfiguration_Item_ProducedItemClassName",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.DropColumn(
                name: "EnergyConsumption",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.AlterColumn<string>(
                name: "ProducedItemClassName",
                table: "FactoryBuildingConfiguration",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ProducedBuildingClassName",
                table: "FactoryBuildingConfiguration",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FactoryBuildingConfiguration_ProducedBuildingClassName",
                table: "FactoryBuildingConfiguration",
                column: "ProducedBuildingClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryBuildingConfiguration_Building_ProducedBuildingClassName",
                table: "FactoryBuildingConfiguration",
                column: "ProducedBuildingClassName",
                principalTable: "Building",
                principalColumn: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryBuildingConfiguration_Item_ProducedItemClassName",
                table: "FactoryBuildingConfiguration",
                column: "ProducedItemClassName",
                principalTable: "Item",
                principalColumn: "ClassName");
        }
    }
}
