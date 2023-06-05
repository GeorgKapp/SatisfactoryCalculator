// ReSharper disable UnusedMember.Global
namespace SatisfactoryCalculator.Tests.References;

internal static class FuelModels
{
    public static readonly Fuel LiquidFuel = new(
        GeneratorModels.FuelGenerator, 
        FuelContentModels.LiquidFuel);
    
    public static readonly Fuel CoalFuel = new(
        GeneratorModels.CoalGenerator, 
        FuelContentModels.Coal,
        FuelContentModels.Water);

    public static readonly Fuel CompactedCoalFuel = new(
        GeneratorModels.CoalGenerator,
        FuelContentModels.CompactedCoal, 
        FuelContentModels.Water);

    public static readonly Fuel PetroleumCokeFuel = new(
        GeneratorModels.CoalGenerator,
        FuelContentModels.PetroleumCoke,
        FuelContentModels.Water);
}
