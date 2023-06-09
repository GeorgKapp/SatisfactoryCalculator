
// ReSharper disable UnusedMemberInSuper.Global

// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

internal interface ICalculationService
{
    double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration);
    double NormalizeAmount(Form? form, double sourceInput);
    FuelCalculationResult CalculateFuelConsumption(GeneratorFuel generatorFuel, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(IRecipe recipe, IEntity entity, IBuilding building, double overclock);
    RecipeBuildingProductionResult CalculateRecipeBuildingProduction(IRecipe recipe, IBuilding building, double overclock);
}
