using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FactoryConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FactoryConfiguration",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DesiredOverclock = table.Column<double>(type: "REAL", nullable: true),
                    SplitOverclockEvenly = table.Column<bool>(type: "INTEGER", nullable: false),
                    CalculatedInVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryConfiguration", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FactoryBuildingConfiguration",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Overclock = table.Column<double>(type: "REAL", nullable: false),
                    BuildingClassName = table.Column<string>(type: "TEXT", nullable: false),
                    BuildingAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    ProducedBuildingClassName = table.Column<string>(type: "TEXT", nullable: true),
                    ProducedItemClassName = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    FactoryConfigurationID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryBuildingConfiguration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FactoryBuildingConfiguration_Building_BuildingClassName",
                        column: x => x.BuildingClassName,
                        principalTable: "Building",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactoryBuildingConfiguration_Building_ProducedBuildingClassName",
                        column: x => x.ProducedBuildingClassName,
                        principalTable: "Building",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_FactoryBuildingConfiguration_FactoryConfiguration_FactoryConfigurationID",
                        column: x => x.FactoryConfigurationID,
                        principalTable: "FactoryConfiguration",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FactoryBuildingConfiguration_Item_ProducedItemClassName",
                        column: x => x.ProducedItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName");
                });

            migrationBuilder.CreateTable(
                name: "FactoryConfigurationOutput",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BuildingClassName = table.Column<string>(type: "TEXT", nullable: true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: true),
                    FactoryConfigurationID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryConfigurationOutput", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FactoryConfigurationOutput_Building_BuildingClassName",
                        column: x => x.BuildingClassName,
                        principalTable: "Building",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_FactoryConfigurationOutput_FactoryConfiguration_FactoryConfigurationID",
                        column: x => x.FactoryConfigurationID,
                        principalTable: "FactoryConfiguration",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FactoryConfigurationOutput_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactoryBuildingConfiguration_BuildingClassName",
                table: "FactoryBuildingConfiguration",
                column: "BuildingClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryBuildingConfiguration_FactoryConfigurationID",
                table: "FactoryBuildingConfiguration",
                column: "FactoryConfigurationID");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryBuildingConfiguration_ProducedBuildingClassName",
                table: "FactoryBuildingConfiguration",
                column: "ProducedBuildingClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryBuildingConfiguration_ProducedItemClassName",
                table: "FactoryBuildingConfiguration",
                column: "ProducedItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryConfigurationOutput_BuildingClassName",
                table: "FactoryConfigurationOutput",
                column: "BuildingClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryConfigurationOutput_FactoryConfigurationID",
                table: "FactoryConfigurationOutput",
                column: "FactoryConfigurationID");

            migrationBuilder.CreateIndex(
                name: "IX_FactoryConfigurationOutput_ItemClassName",
                table: "FactoryConfigurationOutput",
                column: "ItemClassName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactoryBuildingConfiguration");

            migrationBuilder.DropTable(
                name: "FactoryConfigurationOutput");

            migrationBuilder.DropTable(
                name: "FactoryConfiguration");
        }
    }
}
