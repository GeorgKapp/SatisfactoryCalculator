namespace Data.Configurations;

internal class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> entity)
    {
        entity.ToTable(nameof(Building));
        
        entity.HasKey(p => p.ClassName);

        entity
            .Property(p => p.PowerConsumptionRange)
            .HasConversion(
                p => p.ConvertToDataString(),
                p => p.ConvertToPowerConsumptionRange());
    }
}
