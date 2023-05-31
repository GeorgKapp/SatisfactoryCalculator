namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class CalculationService : ICalculationService
{
    public double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration)
    {
        if (amount is null)
            return null;

        amount = NormalizeAmount(form, amount.Value);

        double factor = 60.0 / manufactoringDuration;
        return amount * factor;
    }

    public double NormalizeAmount(Form? form, double sourceInput) =>
        IsFormLiquidOrGas(form)
            ? sourceInput / 1000
            : sourceInput;
    
    public FuelCalculationResult CalculateFuelConsumption(FuelModel fuelModel, double overclock)
    {
        var powerProduction = CalculatePowerGeneratorPowerCapacity(fuelModel.Generator, overclock);
        var amountPerMinute = NormalizeAmount(fuelModel.Ingredient.Item.Form, (powerProduction / fuelModel.Ingredient.Item.EnergyValue) * secondsPerMinute);

        var calculationResult = new FuelCalculationResult();
        calculationResult.AmountPerMinute = amountPerMinute;

        if (fuelModel.SupplementalIngredient is not null)
        {
            calculationResult.SupplementalAmountPerMinute =
                powerProduction
                * fuelModel.Generator.SupplementalToPowerRatio!.Value
                * secondsPerMinute
                / NormalizeAmount(fuelModel.SupplementalIngredient.Item.Form, fuelModel.Generator.SupplementalLoadAmount!.Value);
        }

        if (fuelModel.ByProduct is not null)
            calculationResult.ByProductAmountPerMinute = amountPerMinute * NormalizeAmount(fuelModel.ByProduct.Item.Form, fuelModel.ByProductAmount!.Value);

        return calculationResult;
    }

    public RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, BuildingModel building, double overclock)
    {
        var buildingProductionResult = CalculateRecipeBuildingProduction(recipe, building, overclock);
        return CalculateRecipeItemProduction(recipe, item, buildingProductionResult);
    }

    public RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, RecipeBuildingProductionResult buildingProductionResult)
    {
        var foundItem = recipe.Ingredients.Concat(recipe.Products)
            .Where(p => p.Item.ClassName == item.ClassName)
            .FirstOrDefault() ?? throw new ArgumentException("Item could not be found");

        var amount = NormalizeAmount(foundItem.Item.Form, foundItem.SourceAmount);
        var amountPerMinute = amount * buildingProductionResult.CyclesPerMinute;

        return new RecipeItemProductionResult
        {
            Amount = amount,
            AmountPerMinute = amountPerMinute,
            Time = buildingProductionResult.Time,
            PowerConsumption = buildingProductionResult.PowerConsumption
        };
    }

    public RecipeBuildingProductionResult CalculateRecipeBuildingProduction(RecipeModel recipe, BuildingModel building, double overclock)
    {
        var overclockMultiplier = GetOverClockMultiplier(overclock);
        var buildingSpeed = GetManufactoringBuildingSpeed(building);
        var powerConsumption = CalculatePowerConsumption(building.PowerConsumption, overclockMultiplier);

        var time = recipe.ManufactoringDuration / buildingSpeed / overclockMultiplier;
        var cyclesPerMinute = secondsPerMinute / time;

        return new RecipeBuildingProductionResult
        {
            Time = time,
            CyclesPerMinute = cyclesPerMinute,
            PowerConsumption = powerConsumption
        };
    }
    
    private double CalculatePowerGeneratorPowerCapacity(GeneratorModel generator, double overclock)
    {
        var powerProduction = generator.PowerProduction;
        var overClockMultiplier = GetOverClockMultiplier(overclock);
        var multiplicationFactor = Math.Pow(overClockMultiplier, 1 / generator.PowerProductionExponent);
        var result = powerProduction * multiplicationFactor;
        return result;
    }
    
    private double CalculatePowerConsumption(double? powerConsumption, double overclockMultiplier)
    {
        if (!powerConsumption.HasValue)
            return 0;

        var result = powerConsumption.Value * Math.Pow(overclockMultiplier, powerConsumptionExponent) + double.Epsilon;
        return Math.Round(result, 1);
    }

    private double GetOverClockMultiplier(double percentage) => 
        percentage / defaultPercentage;

    private double GetManufactoringBuildingSpeed(BuildingModel building) => 
        building.ManufactoringSpeed.HasValue
            ? building.ManufactoringSpeed.Value
            : 1;

    private bool IsFormLiquidOrGas(Form? form) => 
        form is not null && 
        (form == Form.Liquid || form == Form.Gas);
    
    
    private const double defaultPercentage = 100;
    private const double secondsPerMinute = 60;
    private const double powerConsumptionExponent = 1.321928;
}