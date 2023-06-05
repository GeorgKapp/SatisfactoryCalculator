using Building = SatisfactoryCalculator.Source.Models.Building;
using Fuel = SatisfactoryCalculator.Source.Models.Fuel;
using Item = SatisfactoryCalculator.Source.Models.Item;
using Recipe = SatisfactoryCalculator.Source.Models.Recipe;

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
    
    public FuelCalculationResult CalculateRoundedFuelConsumption(Fuel fuel, double overclock)
    {
        var fuelConsumptionresult = CalculateFuelConsumption(fuel, overclock);
        fuelConsumptionresult.AmountPerMinute = Math.Round(Math.Round(fuelConsumptionresult.AmountPerMinute, 4), 2);
        fuelConsumptionresult.PowerProduction = Math.Round(Math.Round(fuelConsumptionresult.PowerProduction, 4), 2);
        
        if(fuelConsumptionresult.SupplementalAmountPerMinute.HasValue)
            fuelConsumptionresult.SupplementalAmountPerMinute = Math.Round(fuelConsumptionresult.SupplementalAmountPerMinute.Value, MidpointRounding.AwayFromZero);
        
        return fuelConsumptionresult;
    }
    
    public FuelCalculationResult CalculateFuelConsumption(Fuel fuel, double overclock)
    {
        if (overclock is < 0 or > 250)
            throw new ArgumentException("Overclock Parameter must be between 0 and 250");
        
        var overClockMultiplier = OverClockMultiplier(overclock);
        var powerCapacity = fuel.Generator.PowerProduction * overClockMultiplier;
        var fuelBurnTime = fuel.Ingredient.Item.EnergyValue / powerCapacity;
        var amountPerMinute = secondsPerMinute / fuelBurnTime;

        var calculationResult = new FuelCalculationResult
        {
            AmountPerMinute = NormalizeAmount(fuel.Ingredient.Item.Form, amountPerMinute),
            PowerProduction = powerCapacity,
            FuelBurnTime = fuelBurnTime
        };

        if (fuel.SupplementalIngredient is not null)
        {
            var supplementalAmountPerMinute = powerCapacity
                                              * fuel.Generator.SupplementalToPowerRatio!.Value
                                              * secondsPerMinute
                                              / fuel.Generator.SupplementalLoadAmount!.Value;
            
            calculationResult.SupplementalAmountPerMinute = supplementalAmountPerMinute;
        }
        
        //TODO: needs to be checked for nuclear fuel generators
        if (fuel.ByProduct is not null)
            calculationResult.ByProductAmountPerMinute = amountPerMinute * NormalizeAmount(fuel.ByProduct.Item.Form, fuel.ByProductAmount!.Value);

        return calculationResult;
    }

    public RecipeItemProductionResult CalculateRecipeItemProduction(IRecipe recipe, IEntity entity, IBuilding building, double overclock)
    {
        var buildingProductionResult = CalculateRecipeBuildingProduction(recipe, building, overclock);

        var foundItem = recipe.Ingredients.Concat(recipe.Products)
            .Where(p => p.Part.ClassName == entity.ClassName)
            .FirstOrDefault() ?? throw new ArgumentException("Part could not be found");

        var form = foundItem is IItem recipePartItem 
            ? recipePartItem.Form 
            : null;
        
        var amount = NormalizeAmount(form, foundItem.SourceAmount);
        var amountPerMinute = amount * buildingProductionResult.CyclesPerMinute;

        return new RecipeItemProductionResult
        {
            Amount = amount,
            AmountPerMinute = amountPerMinute,
            Time = buildingProductionResult.Time,
            PowerConsumption = buildingProductionResult.PowerConsumption
        };
    }

    public RecipeBuildingProductionResult CalculateRecipeBuildingProduction(IRecipe recipe, IBuilding building, double overclock)
    {
        var overclockMultiplier = OverClockMultiplier(overclock);
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

    private double GetManufactoringBuildingSpeed(IBuilding building) => 
        building.ManufactoringSpeed.HasValue
            ? building.ManufactoringSpeed.Value
            : 1;

    private bool IsFormLiquidOrGas(Form? form) => 
        form is not null && 
        (form == Form.Liquid || form == Form.Gas);
    
    private double OverClockMultiplier(double overclock) => overclock / 100;

    private const double defaultPercentage = 100;
    private const double secondsPerMinute = 60;
    private const double powerConsumptionExponent = 1.321928;
}