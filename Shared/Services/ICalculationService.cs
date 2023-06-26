// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Shared.Services;

public interface ICalculationService
{
    decimal? CalculateAmountPerMinte(Form? form, decimal? amount, decimal manufactoringDuration);

    FuelCalculationResult CalculateFuelConsumption(
        double overclock,
        decimal generatorPowerProduction,
        decimal fuelEnergyValue,
        Form fuelForm,
        decimal? supplementalToPowerRatio,
        decimal? supplementalLoadAmount,
        decimal? byProductAmount,
        Form? byProductForm);
    
    decimal CalculatePowerConsumption(decimal? powerConsumption, double overclockMultiplier);

    RecipeItemProductionResult CalculateRecipeItemProduction(
        double overclock,
        decimal recipeManufactoringDuration,
        Form recipePartForm,
        decimal recipePartSourceAmount,
        decimal? buildingManufactoringSpeed,
        decimal? buildingPowerConsumption);
    
    RecipeBuildingProductionResult CalculateRecipeBuildingProduction(
        double overclock,
        decimal recipeManufactoringDuration,
        decimal? manufactoringSpeed,
        decimal? powerConsumption);
}
