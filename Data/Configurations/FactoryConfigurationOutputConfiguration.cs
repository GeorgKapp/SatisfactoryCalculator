namespace Data.Configurations;

internal class FactoryConfigurationOutputConfiguration : IEntityTypeConfiguration<FactoryConfigurationOutput>
{
    public void Configure(EntityTypeBuilder<FactoryConfigurationOutput> entity)
    {
        entity.ToTable(nameof(FactoryConfigurationOutput));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}