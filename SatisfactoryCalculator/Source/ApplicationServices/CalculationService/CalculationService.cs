﻿namespace SatisfactoryCalculator.Source.ApplicationServices;

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
        if (overclock is < 0 or > 250)
            throw new ArgumentException("Overclock Parameter must be between 0 and 250");
        
        var overClockMultiplier = GetOverClockMultiplier(overclock);
        var powerCapacity = fuelModel.Generator.PowerProduction * overClockMultiplier;
        var fuelBurnTime = fuelModel.Ingredient.Item.EnergyValue / powerCapacity;
        var amountPerMinute = secondsPerMinute / fuelBurnTime;

        var calculationResult = new FuelCalculationResult
        {
            AmountPerMinute = Math.Round(Math.Round(NormalizeAmount(fuelModel.Ingredient.Item.Form, amountPerMinute), 4), 2),
            PowerProduction = Math.Round(Math.Round(powerCapacity, 4), 2),
            FuelBurnTime = fuelBurnTime
        };

        if (fuelModel.SupplementalIngredient is not null)
        {
            var supplementalAmountPerMinute = powerCapacity
                                              * fuelModel.Generator.SupplementalToPowerRatio!.Value
                                              * secondsPerMinute
                                              / fuelModel.Generator.SupplementalLoadAmount!.Value;
            
            calculationResult.SupplementalAmountPerMinute = Math.Round(supplementalAmountPerMinute, MidpointRounding.AwayFromZero);
        }
        
        //TODO: needs to be checked for nuclear fuel generators
        if (fuelModel.ByProduct is not null)
            calculationResult.ByProductAmountPerMinute = amountPerMinute * NormalizeAmount(fuelModel.ByProduct.Item.Form, fuelModel.ByProductAmount!.Value);

        return calculationResult;
    }

    public RecipeItemProductionResult CalculateRecipeItemProduction(RecipeModel recipe, ItemModel item, BuildingModel building, double overclock)
    {
        var buildingProductionResult = CalculateRecipeBuildingProduction(recipe, building, overclock);

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