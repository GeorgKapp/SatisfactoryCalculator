#pragma warning disable CS8618

namespace Data.Context;

public partial class ModelContext : DbContext
{
    public ModelContext() { }
    public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }
    protected ModelContext(DbContextOptions options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(
                @"Data Source=C:\Users\Georg\Desktop\Test\SatisfactoryData.db");
        }

        base.OnConfiguring(optionsBuilder);
    }

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
        modelBuilder.ApplyConfiguration(new FactoryConfigurationOutputConfiguration());
        modelBuilder.ApplyConfiguration(new FactoryBuildingConfigurationConfiguration());
        modelBuilder.ApplyConfiguration(new FactoryConfigurationConfiguration());
    }
}