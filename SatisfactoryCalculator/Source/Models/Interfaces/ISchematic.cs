namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface ISchematic : IEntity
{
    string Description { get; }
    SchematicType Type { get; }
    double? MenuPriority { get; }
    int TechTier { get; }
    double? TimeToComplete { get; }
    string?[] RelevantEvents { get; }
    IRecipe[] UnlocksRecipes { get; }
    ScannableObject UnlocksScannerObject { get; }
    string?[] UnlocksScannerResources { get; }
    string?[] UnlocksScannerResourcePairs { get; }
    string?[] Emotes { get; }
    ItemToGive[] ItemsToGive { get; }
    int? UnlocksArmEquipmentSlot { get; }
    int? UnlocksInventoryEquipmentSlot { get; }
    SchematicDependency[] SchematicDependencies { get; }
    bool HiddenUntilDependenciesMet { get; }
    bool DependenciesBlocksSchematicAccess { get; }
    bool UnlocksMap { get; }
}