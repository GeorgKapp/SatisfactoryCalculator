namespace Data.Models.Implementation;

public class CreatureLoot : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public string ItemClassName { get; set; }
    public Item Item { get; set; }
    public int Amount { get; set; }
}
