namespace Data.Configurations;

internal class CustomizationRecipeIngredientConfiguration : IEntityTypeConfiguration<CustomizationRecipeIngredient>
{
    public void Configure(EntityTypeBuilder<CustomizationRecipeIngredient> entity)
    {
        entity.ToTable(nameof(CustomizationRecipeIngredient));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}
