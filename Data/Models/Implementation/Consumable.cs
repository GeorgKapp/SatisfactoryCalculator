namespace Data.Models.Implementation;

public class Consumable : IClassNamePrimaryKey
{
    public string ClassName { get; set; }
    public Item Item { get; set; }
    
    public decimal? HealthGain { get; set; }
}
