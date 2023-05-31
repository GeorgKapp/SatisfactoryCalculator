namespace SatisfactoryCalculator.Source.ApplicationServices;
internal interface ICalculationService
{
    double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration);
    double NormalizeAmount(Form? form, double sourceInput);
    FuelCalculationResult CalculateFuelConsumption(FuelModel fuelModel, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, BuildingModel building, double overclock);
    RecipeBuildingProductionResult CalculateRecipeBuildingProduction(RecipeModel recipe, BuildingModel building, double overclock);
}
