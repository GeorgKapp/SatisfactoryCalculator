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

    public ICollection<SchematicCost> Costs { get; set; } = new List<SchematicCost>();
    public ICollection<SchematicDependency> Dependencies { get; set; } = new List<SchematicDependency>();
    public ICollection<ScannableObject> UnlocksScannableObjects  { get; set; } = new List<ScannableObject>();
    public ICollection<Recipe> UnlocksRecipes  { get; set; } = new List<Recipe>();
    public ICollection<Resource> UnlocksScannerResources  { get; set; } = new List<Resource>();
    public ICollection<Resource> UnlocksScannerResourcePairs  { get; set; } = new List<Resource>();
    public ICollection<Emote> UnlocksEmotes  { get; set; } = new List<Emote>();
    public ICollection<Item> GivesItems  { get; set; } = new List<Item>();
}
