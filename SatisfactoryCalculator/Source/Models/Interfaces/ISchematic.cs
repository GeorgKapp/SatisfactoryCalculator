namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface ISchematic : IEntity
{
    public SchematicType Type { get; set; }
    public double? MenuPriority { get; set; }
    public int TechTier { get; set; }
    public double? TimeToComplete { get; set; }
    public string?[] RelevantEvents { get; set; }
    public Reference[] Cost { get; set; }
    public IRecipe[] UnlocksRecipes { get; set; }
    public ScannableObject UnlocksScannerObject { get; set; }
    public string?[] UnlocksScannerResources { get; set; }
    public string?[] UnlocksScannerResourcePairs { get; set; }
    public string?[] Emotes { get; set; }
    public Reference[] ItemsToGive { get; set; }
    public int? UnlocksArmEquipmentSlot { get; set; }
    public int? UnlocksInventoryEquipmentSlot { get; set; }
    public SchematicDependency[] SchematicDependencies { get; set; }
    public bool HiddenUntilDependenciesMet { get; set; }
    public bool DependenciesBlocksSchematicAccess { get; set; }
    public bool UnlocksMap { get; set; }
}