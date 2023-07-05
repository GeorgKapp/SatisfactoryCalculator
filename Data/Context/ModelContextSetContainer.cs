namespace Data.Context;

public partial class ModelContext
{
    public DbSet<Ammunition> Ammunitions { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Consumable> Consumables { get; set; }
    public DbSet<Creature> Creatures { get; set; }
    public DbSet<CustomizationRecipe> CustomizationRecipes { get; set; }
    public DbSet<Emote> Emotes { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Generator> Generators { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Miner> Miners { get; set; }
    public DbSet<Plant> Plants { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Schematic> Schematics { get; set; }
    public DbSet<Statue> Statues { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<FactoryConfiguration> FactoryConfigurations { get; set; }
}
