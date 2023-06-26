using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MinerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinerResource_Miner_MinerClassName",
                table: "MinerResource");

            migrationBuilder.RenameColumn(
                name: "MinerClassName",
                table: "MinerResource",
                newName: "MinersClassName");

            migrationBuilder.RenameIndex(
                name: "IX_MinerResource_MinerClassName",
                table: "MinerResource",
                newName: "IX_MinerResource_MinersClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_MinerResource_Miner_MinersClassName",
                table: "MinerResource",
                column: "MinersClassName",
                principalTable: "Miner",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinerResource_Miner_MinersClassName",
                table: "MinerResource");

            migrationBuilder.RenameColumn(
                name: "MinersClassName",
                table: "MinerResource",
                newName: "MinerClassName");

            migrationBuilder.RenameIndex(
                name: "IX_MinerResource_MinersClassName",
                table: "MinerResource",
                newName: "IX_MinerResource_MinerClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_MinerResource_Miner_MinerClassName",
                table: "MinerResource",
                column: "MinerClassName",
                principalTable: "Miner",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
