namespace SatisfactoryCalculator.Tests.References;

internal class FuelContentModels
{
    public static FuelContentModel CoalFuel = new FuelContentModel 
    { 
        Item = ItemModels.Coal, 
        ItemName = ItemModels.Coal.ClassName 
    };

    public static FuelContentModel CompactedCoalFuel = new FuelContentModel
    {
        Item = ItemModels.CompactedCoal,
        ItemName = ItemModels.CompactedCoal.ClassName
    };

    public static FuelContentModel PetroleumCokeFuel = new FuelContentModel
    {
        Item = ItemModels.PetroleumCoke,
        ItemName = ItemModels.PetroleumCoke.ClassName
    };

    public static FuelContentModel WaterFuel = new FuelContentModel
    {
        Item = ItemModels.Water,
        ItemName = ItemModels.Water.ClassName
    };
}

