namespace SatisfactoryCalculator.Source.Models;

internal class RecipeModel
{
    public RecipeModel(string className, string recipeName, bool isAlternateRecipe, bool constructedByBuildGun, bool constructedInWorkbench, bool constructedInWorkshop, double manufactoringDuration, RecipeContentModel[] ingredients, RecipeContentModel[] products, RecipeBuildingModel[] buildings)
    {
        ClassName = className;
        RecipeName = recipeName;
        IsAlternateRecipe = isAlternateRecipe;
        ConstructedByBuildGun = constructedByBuildGun;
        ConstructedInWorkbench = constructedInWorkbench;
        ConstructedInWorkshop = constructedInWorkshop;
        ManufactoringDuration = manufactoringDuration;
        Ingredients = ingredients;
        Products = products;
        Buildings = buildings;
    }

    public string ClassName { get; set; }
    public string RecipeName { get; set; }
    public bool IsAlternateRecipe { get; set; }
    public bool ConstructedByBuildGun { get; set; }
    public bool ConstructedInWorkshop { get; set; }
    public bool ConstructedInWorkbench { get; set; }
    public double ManufactoringDuration { get; set; }
    public RecipeBuildingModel[] Buildings { get; set; } = Array.Empty<RecipeBuildingModel>();
    public RecipeContentModel[] Ingredients { get; set; } = Array.Empty<RecipeContentModel>();
    public RecipeContentModel[] Products { get; set; } = Array.Empty<RecipeContentModel>();
}
