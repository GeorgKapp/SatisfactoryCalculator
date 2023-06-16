namespace Data.Configurations;

internal class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    public void Configure(EntityTypeBuilder<RecipeIngredient> entity)
    {
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}