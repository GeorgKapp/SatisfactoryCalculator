using SatisfactoryCalculator.Source.Models.Interfaces;

namespace SatisfactoryCalculator.Tests;

public class CalculationTests
{
    private readonly CalculationService _calculationService = new();

    [Fact(DisplayName = "Calculate Cable RecipePart")]
    public void CalculateCableRecipe() => CalculateRecipeProductionTime(RecipeModels.CableRecipe, BuildingModels.Constructor, 100, 2, 30, 4);

    [Fact(DisplayName = "Calculate Cable RecipePart with overlock(200%)")]
    public void CalculateCableOverclockRecipe() => CalculateRecipeProductionTime(RecipeModels.CableRecipe, BuildingModels.Constructor, 200, 1, 60, 10);

    [Fact(DisplayName = "Calculate Cable RecipePart with underclock(50%)")]
    public void CalculateCableUnderclockRecipe() => CalculateRecipeProductionTime(RecipeModels.CableRecipe, BuildingModels.Constructor, 50, 4, 15, 1.6M);

    [Fact(DisplayName = "Calculate Iron Plate RecipePart")]
    public void CalculateIronPlateRecipe() => CalculateRecipeProductionTime(RecipeModels.IronPlateRecipe, BuildingModels.Constructor, 100, 6, 10, 4);

    [Fact(DisplayName = "Calculate Residual Plastic RecipePart")]
    public void CalculateResidualPlasticRecipe() => CalculateRecipeProductionTime(RecipeModels.ResidualPlasticRecipe, BuildingModels.OilRefinery, 100, 6, 10, 30);

    [Fact(DisplayName = "Calculate Cable Part")]
    public void CalculateCableItem() => CalculateItemProduction(RecipeModels.CableRecipe, ItemModels.Cable, BuildingModels.Constructor, 100, 1, 30);

    [Fact(DisplayName = "Calculate Iron Rod Part")]
    public void CalculateIronRodProduction() => CalculateItemProduction(RecipeModels.IronRodRecipe, ItemModels.IronRod, BuildingModels.Constructor, 100, 1, 15, 4);
    
    [Fact(DisplayName = "Calculate Iron Rod Part with underclock(50%)")]
    public void CalculateIronRodUnderclock50Production() => CalculateItemProduction(RecipeModels.IronRodRecipe, ItemModels.IronRod, BuildingModels.Constructor, 50, 1, 7.5M, 1.6M);
    
    [Fact(DisplayName = "Calculate Iron Rod Part with overclock(200%)")]
    public void CalculateIronRodOverclock200Production() => CalculateItemProduction(RecipeModels.IronRodRecipe, ItemModels.IronRod, BuildingModels.Constructor, 200, 1, 30, 10);

    [Fact(DisplayName = "Calculate Iron Rod Part with overclock(250%)")]
    public void CalculateIronRodOverclock250Production() => CalculateItemProduction(RecipeModels.IronRodRecipe, ItemModels.IronRod, BuildingModels.Constructor, 250, 1, 37.5M, 13.4M);

    [Fact(DisplayName = "Calculate Wire Part")]
    public void CalculateWireItem() => CalculateItemProduction(RecipeModels.CableRecipe, ItemModels.Wire, BuildingModels.Constructor, 100, 2, 60);

    [Fact(DisplayName = "Calculate Water Part")]
    public void CalculateWaterItem() => CalculateItemProduction(RecipeModels.ResidualPlasticRecipe, ItemModels.Water, BuildingModels.OilRefinery, 100, 2, 20);

  
    private void CalculateRecipeProductionTime(IRecipe recipe, IBuilding buildingManufactorer, double overclock, decimal execptedTime, decimal expectedCyclesPerMinute, decimal expectedPowerConsumption)
    {
        var productionResult = _calculationService.CalculateRecipeBuildingProduction(recipe, buildingManufactorer, overclock);
        Assert.Equal(execptedTime, productionResult.Time);
        Assert.Equal(expectedCyclesPerMinute, productionResult.CyclesPerMinute);
        Assert.Equal(expectedPowerConsumption, productionResult.PowerConsumption);
    }

    private void CalculateItemProduction(IRecipe recipe, IEntity entity, IBuilding buildingManufactorer, double overclock, decimal execptedAmount, decimal exepectedAmountPerMinute)
    {
        var productionResult = _calculationService.CalculateRecipeItemProduction(recipe, entity, buildingManufactorer, overclock);
        Assert.Equal(execptedAmount, productionResult.Amount);
        Assert.Equal(exepectedAmountPerMinute, productionResult.AmountPerMinute);
    }

    private void CalculateItemProduction(IRecipe recipe, IEntity entity, IBuilding buildingManufactorer, double overclock, decimal execptedAmount, decimal exepectedAmountPerMinute, decimal expectedEnergyConsumption)
    {
        var productionResult = _calculationService.CalculateRecipeItemProduction(recipe, entity, buildingManufactorer, overclock);
        Assert.Equal(execptedAmount, productionResult.Amount);
        Assert.Equal(exepectedAmountPerMinute, productionResult.AmountPerMinute);
        Assert.Equal(expectedEnergyConsumption, productionResult.PowerConsumption);
    }
}