namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class CalculationService
{
    private const double defaultPercentage = 100;
    private const double secondsPerMinute = 60;
    private const double powerConsumptionExponent = 1.321928;

    public double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration)
    {
        if (amount is null)
            return null;

        amount = CalculateAmount(form, amount);

        double factor = 60.0 / manufactoringDuration;
        return amount * factor;
    }

    public double CalculateAmount(Form? form, double sourceInput)
    {
        if (form is not null && (form == Form.Liquid || form == Form.Gas))
            sourceInput /= 1000;

        return sourceInput;
    }

    public double? CalculateAmount(Form? form, double? sourceInput) =>
        sourceInput is not null
            ? CalculateAmount(form, sourceInput.Value)
            : null;

    private double CalculateFuelAmountPerMinute(double fuelEnergyValue, double powerProduction, double powerProductionExponent, double efficiency)
    {
        var efficiencyMultiplicator = efficiency / 100;
        var actualPowerProduction = powerProduction * efficiencyMultiplicator;
        var amount = fuelEnergyValue / actualPowerProduction;
        return 0;
    }

    public FuelItemResult CalculateFuelConsumptionItemResult(FuelModel fuelModel, double overclock)
    {
        var overclockMultiplier = GetOverClockMultiplier(overclock);
        var powerProduction = fuelModel.Generator.PowerProduction ?? 1;
        var energyValue = fuelModel.Ingredient.Item.EnergyValue;
        var powerProductionExponent = fuelModel.Generator.PowerProductionExponent ?? 1;
        var normalizedAmount = IsFormLiquidOrGas(fuelModel.Ingredient.Item.Form) ? 1000 : 1;

        var amountPerMinute = ((powerProduction / energyValue) * secondsPerMinute) / normalizedAmount * Math.Pow(overclockMultiplier, 1 / powerProductionExponent);

        return new FuelItemResult
        {
            Amount = 1,
            AmountPerMinute = amountPerMinute
        };
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

        var duration = recipe.ManufactoringDuration / buildingSpeed / overclockMultiplier;
        var time = duration;
        var cyclesPerMinute = secondsPerMinute / time;

        return new RecipeBuildingProductionResult
        {
            Time = time,
            CyclesPerMinute = cyclesPerMinute,
            PowerConsumption = powerConsumption
        };
    }

    private double GetOverClockMultiplier(double percentage) => 
        percentage / defaultPercentage;

    private double GetManufactoringBuildingSpeed(BuildingModel building) => 
        building.ManufactoringSpeed.HasValue
            ? building.ManufactoringSpeed.Value
            : 1;

    private double CalculatePowerConsumption(double? initialPowerConsumption, double overclockMultiplier)
    {
        if (!initialPowerConsumption.HasValue)
            return 0;

        var result = initialPowerConsumption.Value * Math.Pow(overclockMultiplier, powerConsumptionExponent) + double.Epsilon;
        return Math.Round(result, 1);
    }

    private double NormalizeAmount(Form? form, double sourceInput) =>
        IsFormLiquidOrGas(form)
            ? sourceInput / 1000
            : sourceInput;

    private bool IsFormLiquidOrGas(Form? form) => form is not null && (form == Form.Liquid || form == Form.Gas);

}