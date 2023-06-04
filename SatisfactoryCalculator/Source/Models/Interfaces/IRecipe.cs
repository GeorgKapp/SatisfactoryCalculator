namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IRecipe : IEntity
{
    bool IsAlternateRecipe { get; set; }
    bool ConstructedByBuildGun { get; set; }
    bool ConstructedInWorkshop { get; set; }
    bool ConstructedInWorkbench { get; set; }
    double ManufactoringDuration { get; set; }
    RecipeBuilding[] Buildings { get; set; }
    RecipeItem[] Ingredients { get; set; }
    RecipeItem[] Products { get; set; }
}