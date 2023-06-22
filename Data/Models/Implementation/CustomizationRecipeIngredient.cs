namespace Data.Models.Implementation;

public class CustomizationRecipeIngredient : IIDPrimaryKey
{
    public int ID { get; set; }
    public string ItemClassName { get; set; }
    public Item Item { get; set; }
    public decimal Amount { get; set; }
}
