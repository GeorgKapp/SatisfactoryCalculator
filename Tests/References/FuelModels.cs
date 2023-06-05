namespace SatisfactoryCalculator.Tests.References;

internal class FuelModels
{
    public static Fuel LiquidFuel =
        new Fuel(generator: GeneratorModels.FuelGenerator, ingredient: FuelContentModels.LiquidFuel);
    
    public static Fuel CoalFuel = new Fuel(generator: GeneratorModels.CoalGenerator, ingredient: FuelContentModels.Coal,
        supplementalIngredient: FuelContentModels.Water);

    public static Fuel CompactedCoalFuel = new Fuel(generator: GeneratorModels.CoalGenerator,
        ingredient: FuelContentModels.CompactedCoal, supplementalIngredient: FuelContentModels.Water);

    public static Fuel PetroleumCokeFuel = new Fuel(generator: GeneratorModels.CoalGenerator,
        ingredient: FuelContentModels.PetroleumCoke, supplementalIngredient: FuelContentModels.Water);
}
