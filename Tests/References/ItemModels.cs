namespace SatisfactoryCalculator.Tests.References;

internal class ItemModels
{
    public static Item Wire = new Item(className: "Wire_C", name: "Wire", form: Shared.Enums.Form.Solid,
        energyValue: 0);

    public static Item Cable = new Item(className: "Cable_C", name: "Cable", form: Shared.Enums.Form.Solid,
        energyValue: 0);

    public static Item IronPlate = new Item(className: "IronPlate_C", name: "Iron Plate", form: Shared.Enums.Form.Solid,
        energyValue: 0);
    
    public static Item IronRod = new Item(className: "IronRod_C", name: "Iron Rod", form: Shared.Enums.Form.Solid,
        energyValue: 0);

    public static Item IronIngot = new Item(className: "IronIngot_C", name: "Iron Ingot", form: Shared.Enums.Form.Solid,
        energyValue: 0);

    public static Item PolymerResin = new Item(className: "PolymerResin_C", name: "Polymer Resin",
        form: Shared.Enums.Form.Solid, energyValue: 0);

    public static Item Plastic = new Item(className: "Plastic_C", name: "Plastic", form: Shared.Enums.Form.Solid,
        energyValue: 0);

    public static Item Water = new Item(className: "Water_C", name: "Water", form: Shared.Enums.Form.Liquid,
        energyValue: 0);

    public static Item Rubber = new Item(className: "Rubber_C", name: "Rubber", form: Shared.Enums.Form.Solid,
        energyValue: 0);

    public static Item LiquidFuel = new Item(className: "LiquidFuel_C", name: "LiquidFuel",
        form: Shared.Enums.Form.Liquid, energyValue: 0.75);

    public static Item Coal = new Item(className: "Coal_C", name: "Coal", form: Shared.Enums.Form.Solid,
        energyValue: 300);

    public static Item CompactedCoal = new Item(className: "CompactedCoal_C", name: "Compacted Coal",
        form: Shared.Enums.Form.Solid, energyValue: 630);

    public static Item PetroleumCoke = new Item(className: "PetroleumCoke_C", name: "Petroleum Coke",
        form: Shared.Enums.Form.Solid, energyValue: 180);

    public static Item PackagedFuel = new Item(className: "Fuel_C", name: "LiquidFuel", form: Shared.Enums.Form.Solid,
        energyValue: 750);
}