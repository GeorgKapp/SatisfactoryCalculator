namespace Data.Models.Implementation;

public class Vehicle : IClassNamePrimaryKey
{
    //One to One 
    public string ClassName { get; set; }
    public Item Item { get; set; }
    
    public decimal? FuelConsumption { get; set; }
    public int? InventorySize { get; set; }
}
