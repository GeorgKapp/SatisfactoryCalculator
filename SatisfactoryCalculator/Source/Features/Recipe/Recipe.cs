// ReSharper disable CheckNamespace
#pragma warning disable CS8618

namespace SatisfactoryCalculator.Source.Features.Recipe;

internal class Recipe : IRecipe
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public bool IsAlternate { get; init; }
    public bool ConstructedByBuildGun { get; init; }
    public bool ConstructedInWorkshop { get; init; }
    public bool ConstructedInWorkbench { get; init; }
    public decimal ManufactoringDuration { get; init; }
    public RecipeBuilding[] Buildings { get; init; }
    public RecipePart[] Ingredients { get; init; }
    public RecipePart[] Products { get; init; }
}
