namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Schematic : IBase, IIcon
{
	public string ClassName { get; set; }
	public string DisplayName { get; set; }
	public SchematicType Type { get; set; }
	public double? MenuPriority { get; set; }
	public int TechTier { get; set; }
	public double? TimeToComplete { get; set; }
	public string SmallIconPath { get; set; }
	public string BigIconPath { get; set; }
	public string[] RelevantEvents { get; set; }
	public Reference[] Cost { get; set; }
	public string[] UnlocksRecipes { get; set; }
	public ScannableObject UnlocksScannerObject { get; set; }
	public string[] UnlocksScannerResources { get; set; }
	public string[] UnlocksScannerResourcePairs { get; set; }
	public string[] Emotes { get; set; }
	public Reference[] ItemsToGive { get; set; }
	public int? UnlocksArmEquipmentSlot { get; set; }
	public int? UnlocksInventoryEquipmentSlot { get; set; }
	public SchematicDependency[] SchematicDependencies { get; set; }
	public bool HiddenUntilDependenciesMet { get; set; }
	public bool DependenciesBlocksSchematicAccess { get; set; }
	public bool UnlocksMap { get; set; }
}
