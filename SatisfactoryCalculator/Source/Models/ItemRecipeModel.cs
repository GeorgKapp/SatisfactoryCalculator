namespace SatisfactoryCalculator.Source.Models;

public class ItemRecipeModel
{
    public string ItemName { get; set; }
    public RecipeModel[] ContainedAsIngredient { get; set; } = Array.Empty<RecipeModel>();
    public RecipeModel[] ContainedAsProduct { get; set; } = Array.Empty<RecipeModel>();
    public RecipeModel[] ContainedAsBuildingIngredient { get; set; } = Array.Empty<RecipeModel>();
}
