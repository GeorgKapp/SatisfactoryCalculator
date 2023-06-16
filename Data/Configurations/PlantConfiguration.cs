namespace Data.Configurations;

internal class PlantConfiguration : IEntityTypeConfiguration<Plant>
{
    public void Configure(EntityTypeBuilder<Plant> entity)
    {
        entity.HasKey(e => e.ClassName);
    }
}