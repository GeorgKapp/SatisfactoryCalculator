// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable once CheckNamespace

using SatisfactoryCalculator.Shared.Utility;

namespace SatisfactoryCalculator.Shared.Services;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class CalculationService : ICalculationService
{
    public decimal? CalculateAmountPerMinte(Form? form, decimal? amount, decimal manufactoringDuration)
    {
        if (amount is null)
            return null;

        amount = form.NormalizeAmount(amount.Value);

        var factor = (decimal)60.0 / manufactoringDuration;
        return amount * factor;
    }

    public FuelCalculationResult CalculateFuelConsumption(
        double overclock,
        decimal generatorPowerProduction,
        decimal fuelEnergyValue,
        Form fuelForm,
        decimal? supplementalToPowerRatio,
        decimal? supplementalLoadAmount,
        decimal? byProductAmount,
        Form? byProductForm)
    {
        if (overclock is < 0 or > 250)
            throw new ArgumentException("Overclock Parameter must be between 0 and 250");

        if (overclock == 0)
            return new()
            {
                AmountPerMinute = 0,
                PowerProduction = 0,
                FuelBurnTime = 0,
                SupplementalAmountPerMinute = supplementalToPowerRatio.HasValue ? 0 : null,
                ByProductAmountPerMinute = byProductAmount.HasValue ? 0 : null
            };

        var overClockMultiplier = overclock.GetOverClockMultiplier();
        var powerCapacity = generatorPowerProduction * (decimal)overClockMultiplier;
        var fuelBurnTime = fuelEnergyValue / powerCapacity;
        var amountPerMinute = CalculationUtilties.SecondsPerMinute / fuelBurnTime;

        var calculationResult = new FuelCalculationResult
        {
            AmountPerMinute = fuelForm.NormalizeAmount(amountPerMinute),
            PowerProduction = powerCapacity,
            FuelBurnTime = fuelBurnTime
        };

        if (supplementalToPowerRatio.HasValue)
        {
            var supplementalAmountPerMinute = powerCapacity
                                              * supplementalToPowerRatio.Value
                                              * CalculationUtilties.SecondsPerMinute
                                              / supplementalLoadAmount!.Value;
            
            calculationResult.SupplementalAmountPerMinute = supplementalAmountPerMinute;
        }

        if (byProductAmount.HasValue)
            calculationResult.ByProductAmountPerMinute = amountPerMinute * byProductForm!.Value.NormalizeAmount(byProductAmount.Value);

        return calculationResult;
    }
    
    public decimal CalculatePowerConsumption(decimal? powerConsumption, double overclockMultiplier)
    {
        if (!powerConsumption.HasValue)
            return 0;

        var result = powerConsumption.Value * (decimal)Math.Pow(overclockMultiplier, CalculationUtilties.PowerConsumptionExponent) + (decimal)double.Epsilon;
        return Math.Round(result, 1);
    }

    // ReSharper disable once HeapView.ClosureAllocation
    public RecipeItemProductionResult CalculateRecipeItemProduction(
        double overclock,
        decimal recipeManufactoringDuration, 
        Form recipePartForm, 
        decimal recipePartSourceAmount, 
        decimal? buildingManufactoringSpeed,
        decimal? buildingPowerConsumption)
    {
        var buildingProductionResult = CalculateRecipeBuildingProduction(
            overclock,
            recipeManufactoringDuration,
            buildingManufactoringSpeed,
            buildingPowerConsumption);
        
        var amount = recipePartForm.NormalizeAmount(recipePartSourceAmount);
        var amountPerMinute = amount * buildingProductionResult.CyclesPerMinute;

        return new()
        {
            Amount = amount,
            AmountPerMinute = amountPerMinute,
            Time = buildingProductionResult.Time,
            PowerConsumption = buildingProductionResult.PowerConsumption
        };
    }

    public RecipeBuildingProductionResult CalculateRecipeBuildingProduction(
        double overclock,
        decimal recipeManufactoringDuration,
        decimal? manufactoringSpeed, 
        decimal? powerConsumption)
    {
        var overclockMultiplier = overclock.GetOverClockMultiplier();
        var buildingSpeed = manufactoringSpeed.GetManufactoringBuildingSpeed();
        var calculatedPowerConsumption = CalculatePowerConsumption(powerConsumption, overclockMultiplier);

        var time = recipeManufactoringDuration / buildingSpeed / (decimal)overclockMultiplier;
        var cyclesPerMinute = CalculationUtilties.SecondsPerMinute / time;

        return new()
        {
            Time = time,
            CyclesPerMinute = cyclesPerMinute,
            PowerConsumption = calculatedPowerConsumption
        };
    }
}