using SatisfactoryCalculator.Shared.Utility;

namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class ModelCalculationService
{
    private readonly CalculationService _calculationService;

    public ModelCalculationService(CalculationService calculationService)
    {
        _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
    }

    public decimal? CalculateAmountPerMinte(Form? form, decimal? amount, decimal manufactoringDuration) =>
        _calculationService.CalculateAmountPerMinte(form, amount, manufactoringDuration);
    
    public FuelCalculationResult CalculateRoundedFuelConsumption(
        double overclock,
        GeneratorFuel generatorFuel)
    {
        var fuelConsumptionresult = _calculationService.CalculateFuelConsumption(
            overclock, 
            generatorFuel.Generator.PowerProduction,
            generatorFuel.Ingredient.Item.EnergyValue!.Value,
            generatorFuel.Ingredient.Item.Form!.Value,
            generatorFuel.Generator.SupplementalToPowerRatio,
            generatorFuel.Generator.SupplementalLoadAmount,
            generatorFuel.ByProductAmount,
            generatorFuel.ByProduct?.Item?.Form);
        
        fuelConsumptionresult.AmountPerMinute = Math.Round(Math.Round(fuelConsumptionresult.AmountPerMinute, 4), 2);
        fuelConsumptionresult.PowerProduction = Math.Round(Math.Round(fuelConsumptionresult.PowerProduction, 4), 2);
        
        if(fuelConsumptionresult.SupplementalAmountPerMinute.HasValue)
            fuelConsumptionresult.SupplementalAmountPerMinute = Math.Round(fuelConsumptionresult.SupplementalAmountPerMinute.Value, MidpointRounding.AwayFromZero);
        
        if(fuelConsumptionresult.ByProductAmountPerMinute.HasValue)
            fuelConsumptionresult.ByProductAmountPerMinute = Math.Round(fuelConsumptionresult.ByProductAmountPerMinute.Value, MidpointRounding.AwayFromZero);

        return fuelConsumptionresult;
    }
    
    // ReSharper disable once HeapView.ClosureAllocation
    public RecipeItemProductionResult CalculateRecipeItemProduction(
        double overclock,
        IRecipe recipe, 
        IEntity recipeEntity,
        IBuilding recipeBuilding)
    {
        var buildingProductionResult = CalculateRecipeBuildingProduction(
            overclock,
            recipe.ManufactoringDuration,
            recipeBuilding.ManufactoringSpeed,
            recipeBuilding.PowerConsumption);
        
        var foundRecipePart = recipe.Ingredients
                            .Concat(recipe.Products)
                            .FirstOrDefault(p => p.Part.ClassName == recipeEntity.ClassName) 
                        ?? throw new ArgumentException("Part could not be found");
        
        var recipePartForm = foundRecipePart.Part switch
        {
            IItem item => item.Form,
            IBuilding building => null,
            _ => throw new ArgumentException("Recipe Part is neither Item or Building")
        };

        var amount = recipePartForm.NormalizeAmount(foundRecipePart.SourceAmount);
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
        var calculatedPowerConsumption = _calculationService.CalculatePowerConsumption(powerConsumption, overclockMultiplier);

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
