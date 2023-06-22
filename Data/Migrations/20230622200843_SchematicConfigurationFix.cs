using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SchematicConfigurationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScannableObject_Schematic_SchematicClassName",
                table: "ScannableObject");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor");

            migrationBuilder.DropForeignKey(
                name: "FK_SchematicSchematicDependency_SchematicDependency_DependenciesID",
                table: "SchematicSchematicDependency");

            migrationBuilder.DropForeignKey(
                name: "FK_SchematicSchematicDependency_Schematic_SchematicsClassName",
                table: "SchematicSchematicDependency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchematicSchematicDependency",
                table: "SchematicSchematicDependency");

            migrationBuilder.RenameTable(
                name: "SchematicSchematicDependency",
                newName: "SchematicDependencySchematics");

            migrationBuilder.RenameColumn(
                name: "DependenciesID",
                table: "SchematicDependencySchematics",
                newName: "SchematicDependencyID");

            migrationBuilder.RenameIndex(
                name: "IX_SchematicSchematicDependency_SchematicsClassName",
                table: "SchematicDependencySchematics",
                newName: "IX_SchematicDependencySchematics_SchematicsClassName");

            migrationBuilder.AddColumn<string>(
                name: "SchematicClassName",
                table: "SchematicDependency",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScannableObjectID",
                table: "ScanningActor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SchematicClassName",
                table: "ScannableObject",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchematicDependencySchematics",
                table: "SchematicDependencySchematics",
                columns: new[] { "SchematicDependencyID", "SchematicsClassName" });

            migrationBuilder.CreateIndex(
                name: "IX_SchematicDependency_SchematicClassName",
                table: "SchematicDependency",
                column: "SchematicClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_ScannableObject_Schematic_SchematicClassName",
                table: "ScannableObject",
                column: "SchematicClassName",
                principalTable: "Schematic",
                principalColumn: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor",
                column: "ScannableObjectID",
                principalTable: "ScannableObject",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchematicDependency_Schematic_SchematicClassName",
                table: "SchematicDependency",
                column: "SchematicClassName",
                principalTable: "Schematic",
                principalColumn: "ClassName");

            migrationBuilder.AddForeignKey(
                name: "FK_SchematicDependencySchematics_SchematicDependency_SchematicDependencyID",
                table: "SchematicDependencySchematics",
                column: "SchematicDependencyID",
                principalTable: "SchematicDependency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchematicDependencySchematics_Schematic_SchematicsClassName",
                table: "SchematicDependencySchematics",
                column: "SchematicsClassName",
                principalTable: "Schematic",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScannableObject_Schematic_SchematicClassName",
                table: "ScannableObject");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor");

            migrationBuilder.DropForeignKey(
                name: "FK_SchematicDependency_Schematic_SchematicClassName",
                table: "SchematicDependency");

            migrationBuilder.DropForeignKey(
                name: "FK_SchematicDependencySchematics_SchematicDependency_SchematicDependencyID",
                table: "SchematicDependencySchematics");

            migrationBuilder.DropForeignKey(
                name: "FK_SchematicDependencySchematics_Schematic_SchematicsClassName",
                table: "SchematicDependencySchematics");

            migrationBuilder.DropIndex(
                name: "IX_SchematicDependency_SchematicClassName",
                table: "SchematicDependency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchematicDependencySchematics",
                table: "SchematicDependencySchematics");

            migrationBuilder.DropColumn(
                name: "SchematicClassName",
                table: "SchematicDependency");

            migrationBuilder.RenameTable(
                name: "SchematicDependencySchematics",
                newName: "SchematicSchematicDependency");

            migrationBuilder.RenameColumn(
                name: "SchematicDependencyID",
                table: "SchematicSchematicDependency",
                newName: "DependenciesID");

            migrationBuilder.RenameIndex(
                name: "IX_SchematicDependencySchematics_SchematicsClassName",
                table: "SchematicSchematicDependency",
                newName: "IX_SchematicSchematicDependency_SchematicsClassName");

            migrationBuilder.AlterColumn<int>(
                name: "ScannableObjectID",
                table: "ScanningActor",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "SchematicClassName",
                table: "ScannableObject",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchematicSchematicDependency",
                table: "SchematicSchematicDependency",
                columns: new[] { "DependenciesID", "SchematicsClassName" });

            migrationBuilder.AddForeignKey(
                name: "FK_ScannableObject_Schematic_SchematicClassName",
                table: "ScannableObject",
                column: "SchematicClassName",
                principalTable: "Schematic",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                table: "ScanningActor",
                column: "ScannableObjectID",
                principalTable: "ScannableObject",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SchematicSchematicDependency_SchematicDependency_DependenciesID",
                table: "SchematicSchematicDependency",
                column: "DependenciesID",
                principalTable: "SchematicDependency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchematicSchematicDependency_Schematic_SchematicsClassName",
                table: "SchematicSchematicDependency",
                column: "SchematicsClassName",
                principalTable: "Schematic",
                principalColumn: "ClassName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
