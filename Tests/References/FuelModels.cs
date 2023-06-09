// ReSharper disable UnusedMember.Global
namespace SatisfactoryCalculator.Tests.References;

internal static class FuelModels
{
    public static readonly GeneratorFuel LiquidGeneratorFuel = new()
    {
        Generator = GeneratorModels.FuelGenerator,
        Ingredient = FuelContentModels.LiquidFuel
    };
    
    public static readonly GeneratorFuel CoalGeneratorFuel = new()
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.Coal,
        SupplementalIngredient = FuelContentModels.Water
    };
    
    public static readonly GeneratorFuel CompactedCoalGeneratorFuel = new()
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.CompactedCoal,
        SupplementalIngredient = FuelContentModels.Water
    };
    
    public static readonly GeneratorFuel PetroleumCokeGeneratorFuel = new()
    {
        Generator = GeneratorModels.CoalGenerator,
        Ingredient = FuelContentModels.PetroleumCoke,
        SupplementalIngredient = FuelContentModels.Water
    };
}
