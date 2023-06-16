namespace Data.Models.Implementation;

public class SchematicDependency : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public bool RequireAllSchematicsToBePurchased { get; set; }
    public SchematicDependencyType SchematicDependencyType { get; set; }
    public ICollection<Schematic> Schematics { get; set; } = new List<Schematic>();
}
