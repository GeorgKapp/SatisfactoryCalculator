namespace SatisfactoryCalculator.Tests;

public class CalculationTests
{
    private CalculationService _calculationService = new();

    [Fact(DisplayName = "Calculate Cable Recipe")]
    public void CalculateCableRecipe() => CalculateRecipeProductionTime(RecipeModels.CableRecipe, BuildingModels.Constructor, 100, 2, 30, 4);

    [Fact(DisplayName = "Calculate Cable Recipe with overlock(200%)")]
    public void CalculateCableOverclockRecipe() => CalculateRecipeProductionTime(RecipeModels.CableRecipe, BuildingModels.Constructor, 200, 1, 60, 12.13);

    [Fact(DisplayName = "Calculate Cable Recipe with underclock(50%)")]
    public void CalculateCableUnderclockRecipe() => CalculateRecipeProductionTime(RecipeModels.CableRecipe, BuildingModels.Constructor, 50, 4, 15, 1.32);

    [Fact(DisplayName = "Calculate Iron Plate Recipe")]
    public void CalculateIronPlateRecipe() => CalculateRecipeProductionTime(RecipeModels.IronPlateRecipe, BuildingModels.Constructor, 100, 6, 10, 4);

    [Fact(DisplayName = "Calculate Residual Plastic Recipe")]
    public void CalculateResidualPlasticRecipe() => CalculateRecipeProductionTime(RecipeModels.ResidualPlasticRecipe, BuildingModels.OilRefinery, 100, 6, 10, 30);

    private void CalculateRecipeProductionTime(RecipeModel recipe, BuildingModel buildingManufactorer, double overclock, double execptedTime, double expectedCyclesPerMinute, double expectedPowerConsumption)
    {
        var productionResult = _calculationService.CalculateRecipeBuildingProduction(recipe, buildingManufactorer, overclock);
        Assert.Equal(execptedTime, productionResult.Time);
        Assert.Equal(expectedCyclesPerMinute, productionResult.CyclesPerMinute);
        Assert.Equal(expectedPowerConsumption, productionResult.PowerConsumption);
    }

    [Fact(DisplayName = "Calculate Cable Item")]
    public void CalculateCableItem() => CalculateItemProduction(RecipeModels.CableRecipe, ItemModels.Cable, BuildingModels.Constructor, 100, 1, 30);

    [Fact(DisplayName = "Calculate Wire Item")]
    public void CalculateWireItem() => CalculateItemProduction(RecipeModels.CableRecipe, ItemModels.Wire, BuildingModels.Constructor, 100, 2, 60);

    [Fact(DisplayName = "Calculate Water Item")]
    public void CalculateWaterItem() => CalculateItemProduction(RecipeModels.ResidualPlasticRecipe, ItemModels.Water, BuildingModels.OilRefinery, 100, 2, 20);

    private void CalculateItemProduction(RecipeModel recipe, ItemModel item, BuildingModel buildingManufactorer, double overclock, double execptedAmount, double exepectedAmountPerMinute)
    {
        var productionResult = _calculationService.CalculateRecipeItemProduction(recipe, item, buildingManufactorer, overclock);
        Assert.Equal(execptedAmount, productionResult.Amount);
        Assert.Equal(exepectedAmountPerMinute, productionResult.AmountPerMinute);
    }

    [Fact(DisplayName = "Calculate Coal Fuel Consumption")]
    public void CalculateCoalFuelConsumption() => CalculateFuelConsumption(FuelModels.CoalFuel, 100, 1, 15);

    [Fact(DisplayName = "Calculate Coal Fuel Consumption with overlock(200%)")]
    public void CalculateCoalFuelConsumptionOverclock() => CalculateFuelConsumption(FuelModels.CoalFuel, 200, 1, 25.565411892857238);

    private void CalculateFuelConsumption(FuelModel fuelModel, double overclock, double execptedAmount, double expectedAmountPerMinute)
    {
        var result = _calculationService.CalculateFuelConsumptionItemResult(FuelModels.CoalFuel, overclock);
        Assert.Equal(execptedAmount, result.Amount);
        Assert.Equal(expectedAmountPerMinute, result.AmountPerMinute);
    }
}