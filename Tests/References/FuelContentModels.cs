namespace SatisfactoryCalculator.Tests.References;

internal class FuelContentModels
{
    public static FuelContentModel LiquidFuel = new FuelContentModel 
    { 
        Item = ItemModels.LiquidFuel, 
        ItemName = ItemModels.LiquidFuel.ClassName 
    };
    
    public static FuelContentModel Coal = new FuelContentModel 
    { 
        Item = ItemModels.Coal, 
        ItemName = ItemModels.Coal.ClassName 
    };

    public static FuelContentModel CompactedCoal = new FuelContentModel
    {
        Item = ItemModels.CompactedCoal,
        ItemName = ItemModels.CompactedCoal.ClassName
    };

    public static FuelContentModel PetroleumCoke = new FuelContentModel
    {
        Item = ItemModels.PetroleumCoke,
        ItemName = ItemModels.PetroleumCoke.ClassName
    };

    public static FuelContentModel Water = new FuelContentModel
    {
        Item = ItemModels.Water,
        ItemName = ItemModels.Water.ClassName
    };
}

