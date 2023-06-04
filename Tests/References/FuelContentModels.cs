namespace SatisfactoryCalculator.Tests.References;

internal class FuelContentModels
{
    public static FuelContentModel LiquidFuel = new FuelContentModel 
    { 
        Item = ItemModels.LiquidFuel
    };
    
    public static FuelContentModel Coal = new FuelContentModel 
    { 
        Item = ItemModels.Coal
    };

    public static FuelContentModel CompactedCoal = new FuelContentModel
    {
        Item = ItemModels.CompactedCoal
    };

    public static FuelContentModel PetroleumCoke = new FuelContentModel
    {
        Item = ItemModels.PetroleumCoke
    };

    public static FuelContentModel Water = new FuelContentModel
    {
        Item = ItemModels.Water
    };
}

