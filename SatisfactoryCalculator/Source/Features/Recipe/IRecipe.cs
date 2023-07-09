// ReSharper disable UnusedMemberInSuper.Global

using SatisfactoryCalculator.Source.Features.Shared.Models;

namespace SatisfactoryCalculator.Source.Features.Recipe;

internal interface IRecipe : IEntity
{
    bool IsAlternate { get; }
    bool ConstructedByBuildGun { get; init; }
    bool ConstructedInWorkshop { get; init; }
    bool ConstructedInWorkbench { get; init; }
    decimal ManufactoringDuration { get; }
    RecipeBuilding[] Buildings { get; }
    RecipePart[] Ingredients { get; }
    RecipePart[] Products { get; }
}