namespace Data.Models.Implementation;

public class FuelItem : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public Item Fuel { get; set; }
    public Item? Supplement { get; set; }
    public Item? ByProduct { get; set; }
    public decimal? ByProductAmount { get; set; }
}
