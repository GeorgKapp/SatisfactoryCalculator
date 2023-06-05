// ReSharper disable UnusedMember.Global
namespace SatisfactoryCalculator.Tests.References;

internal static class ItemModels
{
    public static readonly Item Wire = new(
        "Wire_C", 
        "Wire", 
        Shared.Enums.Form.Solid,
        0);

    public static readonly Item Cable = new(
        "Cable_C", 
        "Cable",
        Shared.Enums.Form.Solid,
        0);

    public static readonly Item IronPlate = new(
        "IronPlate_C", 
        "Iron Plate", 
        Shared.Enums.Form.Solid,
        0);
    
    public static readonly Item IronRod = new(
        "IronRod_C", 
        "Iron Rod", 
        Shared.Enums.Form.Solid,
        0);

    public static readonly Item IronIngot = new(
        "IronIngot_C", 
        "Iron Ingot", 
        Shared.Enums.Form.Solid,
        0);

    public static readonly Item PolymerResin = new(
        "PolymerResin_C", 
        "Polymer Resin",
        Shared.Enums.Form.Solid, 
        0);

    public static readonly Item Plastic = new(
        "Plastic_C", 
        "Plastic", 
        Shared.Enums.Form.Solid,
        0);

    public static readonly Item Water = new(
        "Water_C", 
        "Water", 
        Shared.Enums.Form.Liquid,
        0);

    public static readonly Item Rubber = new(
        "Rubber_C", 
        "Rubber", 
        Shared.Enums.Form.Solid,
        0);

    public static readonly Item LiquidFuel = new(
        "LiquidFuel_C", 
        "LiquidFuel",
        Shared.Enums.Form.Liquid, 
        0.75);

    public static readonly Item Coal = new(
        "Coal_C", 
        "Coal",
        Shared.Enums.Form.Solid,
        300);

    public static readonly Item CompactedCoal = new(
        "CompactedCoal_C", 
        "Compacted Coal",
        Shared.Enums.Form.Solid, 
        630);

    public static readonly Item PetroleumCoke = new(
        "PetroleumCoke_C", 
        "Petroleum Coke",
        Shared.Enums.Form.Solid, 
        180);

    public static readonly Item PackagedFuel = new(
        "Fuel_C", 
        "LiquidFuel", 
        Shared.Enums.Form.Solid,
        750);
}