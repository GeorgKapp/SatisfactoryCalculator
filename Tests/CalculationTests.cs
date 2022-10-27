namespace SatisfactoryCalculator.Tests;

public class CalculationTests
{
    private readonly CalculationService _calculationService;

    public CalculationTests()
    {
        _calculationService = new CalculationService();
    }

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
        var productionResult = _calculationService.CalculateRecipeProduction(recipe, buildingManufactorer, overclock);
        Assert.Equal(execptedTime, productionResult.Time);
        Assert.Equal(expectedCyclesPerMinute, productionResult.CyclesPerMinute);
        Assert.Equal(expectedPowerConsumption, productionResult.PowerConsumption);
    }





}