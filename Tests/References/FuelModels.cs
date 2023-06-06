// ReSharper disable UnusedMember.Global
namespace SatisfactoryCalculator.Tests.References;

internal static class FuelModels
{
    public static readonly Fuel LiquidFuel = new()
    {
        Generator = GeneratorModels.FuelGenerator,
        Ingredient = FuelContentModels.LiquidFuel
    };
    
    public static readonly Fuel CoalFuel = new()
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.Coal,
        SupplementalIngredient = FuelContentModels.Water
    };
    
    public static readonly Fuel CompactedCoalFuel = new()
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.CompactedCoal,
        SupplementalIngredient = FuelContentModels.Water
    };
    
    public static readonly Fuel PetroleumCokeFuel = new()
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.PetroleumCoke,
        SupplementalIngredient = FuelContentModels.Water
    };
}
