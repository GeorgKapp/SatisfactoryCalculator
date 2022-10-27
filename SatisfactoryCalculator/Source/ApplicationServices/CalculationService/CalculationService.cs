using SatisfactoryCalculator.DocsServices.Models.DataModels;

namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class CalculationService
{
    private const double defaultPercentage = 100;
    private const double secondsPerMinute = 60;

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

    public BuildingCalculationResult CalculateRecipeProduction(RecipeModel recipe, BuildingModel building, double overclock)
    {
        var overclockMultiplier = GetOverClockMultiplier(overclock);
        var buildingSpeed = GetManufactoringBuildingSpeed(building);

        var duration = recipe.ManufactoringDuration / buildingSpeed / overclockMultiplier;
        var time = duration;
        var cyclesPerMinute = secondsPerMinute / time;
        var powerConsumption = CalculatePowerConsumption(building.PowerConsumption, overclockMultiplier);

        return new BuildingCalculationResult
        {
            Time = time,
            CyclesPerMinute = cyclesPerMinute,
            PowerConsumption = powerConsumption
        };
    }

    private double GetOverClockMultiplier(double percentage) => percentage / defaultPercentage;
    private double GetManufactoringBuildingSpeed(BuildingModel building) => building.ManufactoringSpeed.HasValue
        ? building.ManufactoringSpeed.Value
        : 1;

    private double CalculatePowerConsumption(double? initialPowerConsumption, double overclockMultiplier)
    {
        if (!initialPowerConsumption.HasValue)
            return 0;

        var powerConsumptionExponent = initialPowerConsumption.Value * Math.Pow(overclockMultiplier, 1.6);
        return Math.Round((powerConsumptionExponent + double.Epsilon) * 100.0) / 100.0;
    }
}
