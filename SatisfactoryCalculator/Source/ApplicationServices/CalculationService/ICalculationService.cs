namespace SatisfactoryCalculator.Source.ApplicationServices;
internal interface ICalculationService
{
    double NormalizeAmount(Form? form, double sourceInput);
    FuelItemResult CalculateFuelConsumptionItemResult(FuelModel fuelModel, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, BuildingModel building, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, RecipeBuildingProductionResult buildingProductionResult);
    RecipeBuildingProductionResult CalculateRecipeBuildingProduction(RecipeModel recipe, BuildingModel building, double overclock);
}
