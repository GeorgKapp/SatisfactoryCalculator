using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FactoryConfigForeignKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactoryBuildingConfiguration_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryConfigurationOutput_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryConfigurationOutput");

            migrationBuilder.AlterColumn<int>(
                name: "FactoryConfigurationID",
                table: "FactoryConfigurationOutput",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FactoryConfigurationID",
                table: "FactoryBuildingConfiguration",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryBuildingConfiguration_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryBuildingConfiguration",
                column: "FactoryConfigurationID",
                principalTable: "FactoryConfiguration",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryConfigurationOutput_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryConfigurationOutput",
                column: "FactoryConfigurationID",
                principalTable: "FactoryConfiguration",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FactoryBuildingConfiguration_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryBuildingConfiguration");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryConfigurationOutput_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryConfigurationOutput");

            migrationBuilder.AlterColumn<int>(
                name: "FactoryConfigurationID",
                table: "FactoryConfigurationOutput",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "FactoryConfigurationID",
                table: "FactoryBuildingConfiguration",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryBuildingConfiguration_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryBuildingConfiguration",
                column: "FactoryConfigurationID",
                principalTable: "FactoryConfiguration",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryConfigurationOutput_FactoryConfiguration_FactoryConfigurationID",
                table: "FactoryConfigurationOutput",
                column: "FactoryConfigurationID",
                principalTable: "FactoryConfiguration",
                principalColumn: "ID");
        }
    }
}
