namespace Data.Configurations;

internal class FactoryConfigurationConfiguration : IEntityTypeConfiguration<FactoryConfiguration>
{
    public void Configure(EntityTypeBuilder<FactoryConfiguration> entity)
    {
        entity.ToTable(nameof(FactoryConfiguration));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
        
        entity
            .HasMany(p => p.FactoryBuildingConfigurations)
            .WithOne(p => p.FactoryConfiguration)
            .IsRequired();
        
        entity
            .HasMany(p => p.DesiredOutputs)
            .WithOne(p => p.FactoryConfiguration)
            .IsRequired();
    }
}