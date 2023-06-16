namespace Data.Models.Implementation;

public class ScanningActor : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public Item? Item { get; set; }
    public Building? Building { get; set; }
}

