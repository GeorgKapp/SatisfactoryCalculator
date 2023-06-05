#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class SchematicDependency
{
	public string?[] Schematics { get; set; }
	public bool RequireAllSchematicsToBePurchased { get; set; }
	public SchematicDependencyType SchematicDependencyType { get; set; }
}
