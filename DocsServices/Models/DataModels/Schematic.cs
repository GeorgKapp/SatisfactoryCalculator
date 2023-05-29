namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Schematic : IBase, IIcon
{
	public string ClassName { get; set; } = null!;
	
	// ReSharper disable once PropertyCanBeMadeInitOnly.Global
	public string? DisplayName { get; set; }
	public SchematicType Type { get; set; }
	public double? MenuPriority { get; set; }
	public int TechTier { get; set; }
	public double? TimeToComplete { get; set; }
	public string? SmallIconPath { get; set; }
	public string? BigIconPath { get; set; }
	public string[] RelevantEvents { get; set; } = null!;
	
	// ReSharper disable once PropertyCanBeMadeInitOnly.Global
	public Reference[] Cost { get; set; } = null!;
	public string[] UnlocksRecipes { get; set; } = null!;
	public ScannableObject UnlocksScannerObject { get; set; } = null!;
	public string[] UnlocksScannerResources { get; set; } = null!;
	public string[] UnlocksScannerResourcePairs { get; set; } = null!;
	public string[] Emotes { get; set; } = null!;
	public Reference[] ItemsToGive { get; set; } = null!;
	public int? UnlocksArmEquipmentSlot { get; set; }
	public int? UnlocksInventoryEquipmentSlot { get; set; }
	public SchematicDependency[] SchematicDependencies { get; set; } = null!;
	public bool HiddenUntilDependenciesMet { get; set; }
	public bool DependenciesBlocksSchematicAccess { get; set; }
	public bool UnlocksMap { get; set; }
}
