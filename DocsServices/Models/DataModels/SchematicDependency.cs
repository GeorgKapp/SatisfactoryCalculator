// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class SchematicDependency
{
	public string[] Schematics { get; set; } = Array.Empty<string>();
	public bool RequireAllSchematicsToBePurchased { get; set; }
	public SchematicDependencyType SchematicDependencyType { get; set; }
}
