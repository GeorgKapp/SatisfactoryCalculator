namespace SatisfactoryCalculator.Tests.References;

internal class FuelModels
{
    public static Fuel LiquidFuel = new Fuel
    {
        Generator = GeneratorModels.FuelGenerator,
        Ingredient = FuelContentModels.LiquidFuel,
    };
    
    public static Fuel CoalFuel = new Fuel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.Coal,
        SupplementalIngredient = FuelContentModels.Water,
    };

    public static Fuel CompactedCoalFuel = new Fuel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.CompactedCoal,
        SupplementalIngredient = FuelContentModels.Water,
    };

    public static Fuel PetroleumCokeFuel = new Fuel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.PetroleumCoke,
        SupplementalIngredient = FuelContentModels.Water,
    };
}
