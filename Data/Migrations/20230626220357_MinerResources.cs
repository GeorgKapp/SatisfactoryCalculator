using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MinerResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MinerResource",
                columns: table => new
                {
                    ExtractableResourcesClassName = table.Column<string>(type: "TEXT", nullable: false),
                    MinerClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinerResource", x => new { x.ExtractableResourcesClassName, x.MinerClassName });
                    table.ForeignKey(
                        name: "FK_MinerResource_Miner_MinerClassName",
                        column: x => x.MinerClassName,
                        principalTable: "Miner",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinerResource_Resource_ExtractableResourcesClassName",
                        column: x => x.ExtractableResourcesClassName,
                        principalTable: "Resource",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MinerResource_MinerClassName",
                table: "MinerResource",
                column: "MinerClassName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinerResource");
        }
    }
}
