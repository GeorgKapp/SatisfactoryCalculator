namespace Data.Configurations;

internal class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    public void Configure(EntityTypeBuilder<RecipeIngredient> entity)
    {
        entity.ToTable(nameof(RecipeIngredient));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}