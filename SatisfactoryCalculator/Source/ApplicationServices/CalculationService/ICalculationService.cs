
// ReSharper disable UnusedMemberInSuper.Global

// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal interface ICalculationService
{
    decimal? CalculateAmountPerMinte(Form? form, decimal? amount, decimal manufactoringDuration);
    decimal NormalizeAmount(Form? form, decimal sourceInput);
    FuelCalculationResult CalculateFuelConsumption(GeneratorFuel generatorFuel, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(IRecipe recipe, IEntity entity, IBuilding building, double overclock);
    RecipeBuildingProductionResult CalculateRecipeBuildingProduction(IRecipe recipe, IBuilding building, double overclock);
}
