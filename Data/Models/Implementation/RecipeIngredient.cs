namespace Data.Models.Implementation;

public class RecipeIngredient : IIDPrimaryKey
{
    public int ID { get; set; }
    
    public Item Item { get; set; }
    public decimal Amount { get; set; }
}
