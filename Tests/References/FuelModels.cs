namespace SatisfactoryCalculator.Tests.References;

internal class FuelModels
{
    public static FuelModel LiquidFuel = new FuelModel
    {
        Generator = GeneratorModels.FuelGenerator,
        Ingredient = FuelContentModels.LiquidFuel,
    };
    
    public static FuelModel CoalFuel = new FuelModel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.Coal,
        SupplementalIngredient = FuelContentModels.Water,
    };

    public static FuelModel CompactedCoalFuel = new FuelModel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.CompactedCoal,
        SupplementalIngredient = FuelContentModels.Water,
    };

    public static FuelModel PetroleumCokeFuel = new FuelModel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.PetroleumCoke,
        SupplementalIngredient = FuelContentModels.Water,
    };
}
