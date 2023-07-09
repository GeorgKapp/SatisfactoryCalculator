namespace Data.Models.Implementation;

public class FactoryConfigurationOutput : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public string? BuildingClassName { get; set; }
    public Building? Building { get; set; }
    
    public string? ItemClassName { get; set; }
    public Item? Item { get; set; }
    
    public string? AlternateRecipeClassName { get; set; }
    public Recipe? AlternateRecipe { get; set; }

    public decimal? Amount { get; set; }
    public decimal? BuildingAmount { get; set; }
    public FactoryConfiguration FactoryConfiguration { get; set; }
}
