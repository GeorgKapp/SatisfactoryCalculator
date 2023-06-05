using IRecipe = SatisfactoryCalculator.Source.Models.Interfaces.IRecipe;

namespace SatisfactoryCalculator.Source.Models;

internal class Recipe : IRecipe
{
    public Recipe(string className, string name, bool isAlternateRecipe, bool constructedByBuildGun, bool constructedInWorkbench, bool constructedInWorkshop, double manufactoringDuration, RecipePart[] ingredients, RecipePart[] products, RecipeBuilding[] buildings)
    {
        ClassName = className;
        Name = name;
        IsAlternateRecipe = isAlternateRecipe;
        ConstructedByBuildGun = constructedByBuildGun;
        ConstructedInWorkbench = constructedInWorkbench;
        ConstructedInWorkshop = constructedInWorkshop;
        ManufactoringDuration = manufactoringDuration;
        Ingredients = ingredients;
        Products = products;
        Buildings = buildings;
        Image = null;
    }

    public string ClassName { get; set; }
    public string Name { get; set; }
    public BitmapImage? Image { get; set; }
    public bool IsAlternateRecipe { get; set; }
    public bool ConstructedByBuildGun { get; set; }
    public bool ConstructedInWorkshop { get; set; }
    public bool ConstructedInWorkbench { get; set; }
    public double ManufactoringDuration { get; set; }
    public RecipeBuilding[] Buildings { get; set; }
    public RecipePart[] Ingredients { get; set; }
    public RecipePart[] Products { get; set; }
}
