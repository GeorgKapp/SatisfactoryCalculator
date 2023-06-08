namespace SatisfactoryCalculator.Source.Models;

internal class SchematicDependency
{
    public ISchematic[] Schematics { get; set; }
    public bool RequireAllSchematicsToBePurchased { get; set; }
    public SchematicDependencyType SchematicDependencyType { get; set; }
}