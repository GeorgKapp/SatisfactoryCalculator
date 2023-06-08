// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618
namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Schematic : IBase, IIcon
{
	public string ClassName { get; set; } = null!;
	public string Description { get; set; }
	// ReSharper disable once PropertyCanBeMadeInitOnly.Global
	public string? DisplayName { get; set; }
	public SchematicType Type { get; set; }
	public double? MenuPriority { get; set; }
	public int TechTier { get; set; }
	public double? TimeToComplete { get; set; }
	public string? SmallIconPath { get; set; }
	public string? BigIconPath { get; set; }
	public string[] RelevantEvents { get; set; } = Array.Empty<string>();
	
	// ReSharper disable once PropertyCanBeMadeInitOnly.Global
	public Reference[] Cost { get; set; } = Array.Empty<Reference>();
	public string[] UnlocksRecipes { get; set; } = Array.Empty<string>();
	public ScannableObject[] UnlocksScannerObjects { get; set; } = Array.Empty<ScannableObject>();
	public string[] UnlocksScannerResources { get; set; } = Array.Empty<string>();
	public string[] UnlocksScannerResourcePairs { get; set; } = Array.Empty<string>();
	public string[] Emotes { get; set; } = Array.Empty<string>();
	public Reference[] ItemsToGive { get; set; } = Array.Empty<Reference>();
	public int? UnlocksArmEquipmentSlot { get; set; }
	public int? UnlocksInventoryEquipmentSlot { get; set; }
	public SchematicDependency[] SchematicDependencies { get; set; } = Array.Empty<SchematicDependency>();
	public bool HiddenUntilDependenciesMet { get; set; }
	public bool DependenciesBlocksSchematicAccess { get; set; }
	public bool UnlocksMap { get; set; }
}
