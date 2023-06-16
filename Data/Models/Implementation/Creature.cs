namespace Data.Models.Implementation;

public class Creature : IClassNamePrimaryKey, INameEntity, IDescriptionEntity, IImage
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SmallImagePath { get; set; }
    public string BigImagePath { get; set; }
    
    public int HitPoints { get; set; }
    public ICollection<string> Damage { get; set; } = new List<string>();
    public CreatureBehaviour Behaviour { get; set; }
    public CreatureLoot? Loot { get; set; }
    public ICollection<Creature> Variants { get; set; } = new List<Creature>();
}
