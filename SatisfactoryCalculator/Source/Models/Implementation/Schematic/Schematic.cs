// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

// ReSharper disable once UnusedType.Global
internal class Schematic : ISchematic
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public SchematicType Type { get; init; }
    public double? MenuPriority { get; init; }
    public int TechTier { get; init; }
    public double? TimeToComplete { get; init; }
    public string?[] RelevantEvents { get; init; }
    public IRecipe[] UnlocksRecipes { get; init; }
    public ScannableObject UnlocksScannerObject { get; init; }
    public string?[] UnlocksScannerResources { get; init; }
    public string?[] UnlocksScannerResourcePairs { get; init; }
    public string?[] Emotes { get; init; }
    public ItemToGive[] ItemsToGive { get; init; }
    public int? UnlocksArmEquipmentSlot { get; init; }
    public int? UnlocksInventoryEquipmentSlot { get; init; }
    public SchematicDependency[] SchematicDependencies { get; init; }
    public bool HiddenUntilDependenciesMet { get; init; }
    public bool DependenciesBlocksSchematicAccess { get; init; }
    public bool UnlocksMap { get; init; }
}
