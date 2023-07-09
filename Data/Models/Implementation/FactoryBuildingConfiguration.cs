namespace Data.Models.Implementation;

public class FactoryBuildingConfiguration : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public double Overclock { get; set; }
    
    public string BuildingClassName { get; set; }
    public Building Building { get; set; }
    public int BuildingAmount { get; set; }
    public decimal EnergyConsumption { get; set; }
    
    public string ProducedItemClassName { get; set; }
    public Item ProducedItem { get; set; }
    public decimal Amount { get; set; }
    
    public FactoryConfiguration FactoryConfiguration { get; set; }
}
