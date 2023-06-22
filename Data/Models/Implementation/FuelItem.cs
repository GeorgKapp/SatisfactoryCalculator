namespace Data.Models.Implementation;

public class FuelItem : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public string FuelClassName { get; set; }
    public Item Fuel { get; set; }
    
    public string? SupplementClassName { get; set; }
    public Item? Supplement { get; set; }
    
    public string? ByProductClassName { get; set; }
    public Item? ByProduct { get; set; }
    public decimal? ByProductAmount { get; set; }
}
