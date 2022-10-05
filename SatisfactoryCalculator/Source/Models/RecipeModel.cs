namespace SatisfactoryCalculator.Source.Models;

public class RecipeModel
{
    public string ClassName { get; set; }
    public string RecipeName { get; set; }
    public bool IsAlternateRecipe { get; set; }
    public bool ConstructedByBuildGun { get; set; }
    public bool ConstructedInWorkshop { get; set; }
    public bool ConstructedInWorkbench { get; set; }
    public RecipeBuildingModel[] Buildings { get; set; } = Array.Empty<RecipeBuildingModel>();
    public RecipeContentModel[] Ingredients { get; set; } = Array.Empty<RecipeContentModel>();
    public RecipeContentModel[] Products { get; set; } = Array.Empty<RecipeContentModel>();
}