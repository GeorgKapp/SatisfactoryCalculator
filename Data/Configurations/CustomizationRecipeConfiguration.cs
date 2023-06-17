namespace Data.Configurations;

internal class CustomizationRecipeConfiguration : IEntityTypeConfiguration<CustomizationRecipe>
{
    public void Configure(EntityTypeBuilder<CustomizationRecipe> entity)
    {
        entity.ToTable(nameof(CustomizationRecipe));
        
        entity.HasKey(e => e.ClassName);
        
        entity
            .HasMany(p => p.Ingredients)
            .WithOne()
            .IsRequired();
    }
}