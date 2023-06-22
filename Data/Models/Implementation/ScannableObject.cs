using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Data.Models.Implementation;

public class ScannableObject : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public string? ItemClassName { get; set; }
    public Item? Item { get; set; }
    
    public string? CreatureClassName { get; set; }
    public Creature? Creature { get; set; }
    
    public string? PlantClassName { get; set; }
    public Plant? Plant { get; set; }
    
    public ICollection<ScanningActor> ScanningActors = new List<ScanningActor>();
}
