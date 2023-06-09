using System.Diagnostics.CodeAnalysis;

// ReSharper disable MemberCanBeMadeStatic.Local

// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.ApplicationServices;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
internal class CalculationService : ICalculationService
{
    public double? CalculateAmountPerMinte(Form? form, double? amount, double manufactoringDuration)
    {
        if (amount is null)
            return null;

        amount = NormalizeAmount(form, amount.Value);

        var factor = 60.0 / manufactoringDuration;
        return amount * factor;
    }

    public double NormalizeAmount(Form? form, double sourceInput) =>
        IsFormLiquidOrGas(form)
            ? sourceInput / 1000
            : sourceInput;
    
    public FuelCalculationResult CalculateRoundedFuelConsumption(GeneratorFuel generatorFuel, double overclock)
    {
        var fuelConsumptionresult = CalculateFuelConsumption(generatorFuel, overclock);
        fuelConsumptionresult.AmountPerMinute = Math.Round(Math.Round(fuelConsumptionresult.AmountPerMinute, 4), 2);
        fuelConsumptionresult.PowerProduction = Math.Round(Math.Round(fuelConsumptionresult.PowerProduction, 4), 2);
        
        if(fuelConsumptionresult.SupplementalAmountPerMinute.HasValue)
            fuelConsumptionresult.SupplementalAmountPerMinute = Math.Round(fuelConsumptionresult.SupplementalAmountPerMinute.Value, MidpointRounding.AwayFromZero);
        
        return fuelConsumptionresult;
    }
    
    public FuelCalculationResult CalculateFuelConsumption(GeneratorFuel generatorFuel, double overclock)
    {
        if (overclock is < 0 or > 250)
            throw new ArgumentException("Overclock Parameter must be between 0 and 250");
        
        var overClockMultiplier = OverClockMultiplier(overclock);
        var powerCapacity = generatorFuel.Generator.PowerProduction * overClockMultiplier;
        var fuelBurnTime = generatorFuel.Ingredient.Item.EnergyValue / powerCapacity;
        var amountPerMinute = SecondsPerMinute / fuelBurnTime;

        var calculationResult = new FuelCalculationResult
        {
            AmountPerMinute = NormalizeAmount(generatorFuel.Ingredient.Item.Form, amountPerMinute),
            PowerProduction = powerCapacity,
            FuelBurnTime = fuelBurnTime
        };

        if (generatorFuel.SupplementalIngredient is not null)
        {
            var supplementalAmountPerMinute = powerCapacity
                                              * generatorFuel.Generator.SupplementalToPowerRatio!.Value
                                              * SecondsPerMinute
                                              / generatorFuel.Generator.SupplementalLoadAmount!.Value;
            
            calculationResult.SupplementalAmountPerMinute = supplementalAmountPerMinute;
        }

        if (generatorFuel.ByProduct is not null)
            calculationResult.ByProductAmountPerMinute = amountPerMinute * NormalizeAmount(generatorFuel.ByProduct.Item.Form, generatorFuel.ByProductAmount!.Value);

        return calculationResult;
    }

    // ReSharper disable once HeapView.ClosureAllocation
    public RecipeItemProductionResult CalculateRecipeItemProduction(IRecipe recipe, IEntity entity, IBuilding building, double overclock)
    {
        var buildingProductionResult = CalculateRecipeBuildingProduction(recipe, building, overclock);

        var foundItem = recipe.Ingredients
            .Concat(recipe.Products)
            .FirstOrDefault(p => p.Part.ClassName == entity.ClassName) 
                        ?? throw new ArgumentException("Part could not be found");

        var recipePartItem = foundItem.Part as IItem;
        var form = recipePartItem?.Form;
        
        var amount = NormalizeAmount(form, foundItem.SourceAmount);
        var amountPerMinute = amount * buildingProductionResult.CyclesPerMinute;

        return new()
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
        var cyclesPerMinute = SecondsPerMinute / time;

        return new()
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

        var result = powerConsumption.Value * Math.Pow(overclockMultiplier, PowerConsumptionExponent) + double.Epsilon;
        return Math.Round(result, 1);
    }

    private double GetManufactoringBuildingSpeed(IBuilding building) => building.ManufactoringSpeed ?? 1;
    private bool IsFormLiquidOrGas(Form? form) => form is Form.Liquid or Form.Gas;
    private double OverClockMultiplier(double overclock) => overclock / DefaultPercentage;

    private const double DefaultPercentage = 100;
    private const double SecondsPerMinute = 60;
    private const double PowerConsumptionExponent = 1.321928;
}