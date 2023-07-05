namespace Data.Configurations;

internal class FactoryBuildingConfigurationConfiguration : IEntityTypeConfiguration<FactoryBuildingConfiguration>
{
    public void Configure(EntityTypeBuilder<FactoryBuildingConfiguration> entity)
    {
        entity.ToTable(nameof(FactoryBuildingConfiguration));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}