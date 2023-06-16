namespace Data.Configurations;

internal class CustomizationRecipeIngredientConfiguration : IEntityTypeConfiguration<CustomizationRecipeIngredient>
{
    public void Configure(EntityTypeBuilder<CustomizationRecipeIngredient> entity)
    {
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}
