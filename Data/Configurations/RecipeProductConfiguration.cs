namespace Data.Configurations;

internal class RecipeProductConfiguration : IEntityTypeConfiguration<RecipeProduct>
{
    public void Configure(EntityTypeBuilder<RecipeProduct> entity)
    {
        entity.ToTable(nameof(RecipeProduct));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}