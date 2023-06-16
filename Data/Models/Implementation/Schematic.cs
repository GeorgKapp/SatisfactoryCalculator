namespace Data.Models.Implementation;

public class Schematic : IClassNamePrimaryKey, INameEntity, IDescriptionEntity, IImage
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string? SmallImagePath { get; set; }
    public string? BigImagePath { get; set; }
    public string Description { get; set; }
    
    public SchematicType SchematicType { get; set; }
    public RelevantEvent? RelevantEvent { get; set; }
    public decimal? MenuPriority { get; set; }
    public int TechTier { get; set; }
    public decimal? TimeToComplete { get; set; }
    public int? UnlocksArmEquipmentSlot { get; set; }
    public int? UnlocksInventoryEquipmentSlot { get; set; }
    public bool HiddenUntilDependenciesMet { get; set; }
    public bool DependenciesBlocksSchematicAccess { get; set; }
    public bool UnlocksMap { get; set; }

    public ICollection<SchematicCost> Costs = new List<SchematicCost>();
    public ICollection<SchematicDependency> Dependencies = new List<SchematicDependency>();
    public ICollection<ScannableObject> UnlocksScannableObjects  { get; set; }
    public ICollection<Recipe> UnlocksRecipes  { get; set; }
    public ICollection<Resource> UnlocksScannerResources  { get; set; }
    public ICollection<Resource> UnlocksScannerResourcePairs  { get; set; }
    public ICollection<Emote> UnlocksEmotes  { get; set; }
    public ICollection<Item> GivesItems  { get; set; }
}
