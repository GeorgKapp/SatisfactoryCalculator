using Building = SatisfactoryCalculator.Source.Models.Building;
using Fuel = SatisfactoryCalculator.Source.Models.Fuel;
using Item = SatisfactoryCalculator.Source.Models.Item;
using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

namespace SatisfactoryCalculator.Source.ApplicationServices;
internal interface ICalculationService
{
    double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration);
    double NormalizeAmount(Form? form, double sourceInput);
    FuelCalculationResult CalculateFuelConsumption(Fuel fuel, double overclock);
    RecipeItemProductionResult CalculateRecipeItemProduction(IRecipe recipe, IEntity entity, IBuilding building, double overclock);
    RecipeBuildingProductionResult CalculateRecipeBuildingProduction(IRecipe recipe, IBuilding building, double overclock);
}
