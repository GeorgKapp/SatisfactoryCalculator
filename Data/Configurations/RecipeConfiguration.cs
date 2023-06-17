namespace Data.Configurations;

internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> entity)
    {
        entity.ToTable(nameof(Recipe));
        
        entity.HasKey(e => e.ClassName);
        
        entity
            .HasMany(p => p.Buildings)
            .WithMany()
            .UsingEntity(p => p.ToTable("RecipeBuilding"));
        
        entity
            .HasMany(p => p.Ingredients)
            .WithOne()
            .IsRequired();
        
        entity
            .HasMany(p => p.Products)
            .WithOne()
            .IsRequired();
        
        entity
            .Property(p => p.VariablePowerConsumptionRange)
            .HasConversion(
                p => p.ConvertToDataString(),
                p => p.ConvertToPowerConsumptionRange());
    }
}
