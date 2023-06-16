#pragma warning disable CS8618

namespace Data.Context;

public class ModelContext : DbContext
{
    public ModelContext() { }
    public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\Georg\\Desktop\\Test\\SatisfactoryData.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AmmunitionConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingConfiguration());
        modelBuilder.ApplyConfiguration(new ConsumableConfiguration());
        modelBuilder.ApplyConfiguration(new CreatureConfiguration());
        modelBuilder.ApplyConfiguration(new CreatureLootConfiguration());
        modelBuilder.ApplyConfiguration(new CustomizationRecipeConfiguration());
        modelBuilder.ApplyConfiguration(new CustomizationRecipeIngredientConfiguration());
        modelBuilder.ApplyConfiguration(new EmoteConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new FuelItemConfiguration());
        modelBuilder.ApplyConfiguration(new GeneratorConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new MinerConfiguration());
        modelBuilder.ApplyConfiguration(new PlantConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeIngredientConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeProductConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceConfiguration());
        modelBuilder.ApplyConfiguration(new ScannableObjectConfiguration());
        modelBuilder.ApplyConfiguration(new ScanningActorConfiguration());
        modelBuilder.ApplyConfiguration(new SchematicConfiguration());
        modelBuilder.ApplyConfiguration(new SchematicCostConfiguration());
        modelBuilder.ApplyConfiguration(new SchematicDependencyConfiguration());
        modelBuilder.ApplyConfiguration(new StatueConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new WeaponConfiguration());
    }
}