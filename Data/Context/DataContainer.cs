namespace Data.Context;

public class DataContainer
{
    public List<Ammunition> Ammunition { get; set; } = new();
    public List<Building> Buildings { get; set; } = new();
    public List<Consumable> Consumables { get; set; } = new();
    public List<Creature> Creatures { get; set; } = new();
    public List<CustomizationRecipe> CustomizationRecipes { get; set; } = new();
    public List<Emote> Emotes { get; set; } = new();
    public List<Equipment> Equipments { get; set; } = new();
    public List<Generator> Generators { get; set; } = new();
    public List<Item> Items { get; set; } = new();
    public List<Miner> Miners { get; set; } = new();
    public List<Plant> Plants { get; set; } = new();
    public List<Recipe> Recipes { get; set; } = new();
    public List<Resource> Resources { get; set; } = new();
    public List<Schematic> Schematics { get; set; } = new();
    public List<Statue> Statues { get; set; } = new();
    public List<Vehicle> Vehicles { get; set; } = new();
    public List<Weapon> Weapons { get; set; } = new();
}
