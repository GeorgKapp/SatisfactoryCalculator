namespace SatisfactoryCalculator.Tests.References;

internal class FuelModels
{
    public static FuelModel CoalFuel = new FuelModel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.CoalFuel,
        SupplementalIngredient = FuelContentModels.WaterFuel,
    };

    public static FuelModel CompactedCoalFuel = new FuelModel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.CompactedCoalFuel,
        SupplementalIngredient = FuelContentModels.WaterFuel,
    };

    public static FuelModel PetroleumCokeFuel = new FuelModel
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.PetroleumCokeFuel,
        SupplementalIngredient = FuelContentModels.WaterFuel,
    };
}
