namespace Data.Configurations;

internal class PlantConfiguration : IEntityTypeConfiguration<Plant>
{
    public void Configure(EntityTypeBuilder<Plant> entity)
    {
        entity.ToTable(nameof(Plant));
        
        entity.HasKey(e => e.ClassName);
    }
}