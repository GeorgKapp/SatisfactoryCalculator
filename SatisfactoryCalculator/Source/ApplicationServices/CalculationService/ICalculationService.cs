namespace SatisfactoryCalculator.Source.ApplicationServices;
internal interface ICalculationService
{
    double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration);
    double NormalizeAmount(Form? form, double sourceInput);
    void UpdateFuelModel(FuelModel fuelModel, double overclock);
    double CalculateFuelConsumption(FuelModel fuelModel, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, BuildingModel building, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, RecipeBuildingProductionResult buildingProductionResult);
    RecipeBuildingProductionResult CalculateRecipeBuildingProduction(RecipeModel recipe, BuildingModel building, double overclock);
}
