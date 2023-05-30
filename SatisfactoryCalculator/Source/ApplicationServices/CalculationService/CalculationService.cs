using SatisfactoryCalculator.DocsServices.Models.DataModels;

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

    public void UpdateFuelModel(FuelModel fuelModel, double overclock)
    {
        var powerProduction = CalculatePowerGeneratorPowerCapacity(fuelModel.Generator, overclock);
        var energyValue = fuelModel.Ingredient.Item.EnergyValue;
        var multiplier = (powerProduction / energyValue) * secondsPerMinute;

        fuelModel.Ingredient.AmountPerMinute = multiplier;
        
        if(fuelModel.SupplementalIngredient is not null)
            fuelModel.SupplementalIngredient.AmountPerMinute = CalculateGeneratorSupplementalConsumption(fuelModel, overclock);
        
        if (fuelModel.ByProduct is not null)
            fuelModel.ByProduct.AmountPerMinute = multiplier * fuelModel.ByProductAmount;
    }

    public double CalculateFuelConsumption(FuelModel fuelModel, double overclock)
    {
        var overclockMultiplier = GetOverClockMultiplier(overclock);
        var powerProduction = fuelModel.Generator.PowerProduction;
        var energyValue = fuelModel.Ingredient.Item.EnergyValue;
        var powerProductionExponent = fuelModel.Generator.PowerProductionExponent;
        var normalizedAmount = IsFormLiquidOrGas(fuelModel.Ingredient.Item.Form) ? 1000 : 1;
        var amountPerMinute = ((powerProduction / energyValue) * secondsPerMinute) / normalizedAmount * Math.Pow(overclockMultiplier, 1 / powerProductionExponent);
        return amountPerMinute;
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
    
    private double CalculateGeneratorSupplementalConsumption(FuelModel fuelModel, double overclock)
    {
        var powerProduction = CalculatePowerGeneratorPowerCapacity(fuelModel.Generator, overclock);
        var ratio = secondsPerMinute / (IsFormLiquidOrGas(fuelModel.SupplementalIngredient!.Item.Form) ? 1000 : 1);
        var result = powerProduction * fuelModel.Generator.SupplementalToPowerRatio!.Value * ratio;
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