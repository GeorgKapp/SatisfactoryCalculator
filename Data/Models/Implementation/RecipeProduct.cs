namespace Data.Models.Implementation;

public class RecipeProduct : IIDPrimaryKey
{
    public int ID { get; set; }

    public Item Item { get; set; }
    public decimal Amount { get; set; }
}
