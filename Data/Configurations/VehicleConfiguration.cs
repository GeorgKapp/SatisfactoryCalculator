namespace Data.Configurations;

internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> entity)
    {
        entity.HasKey(e => e.ClassName);

        entity.HasOne(d => d.Item)
            .WithOne(p => p.Vehicle)
            .HasForeignKey<Vehicle>(d => d.ClassName);
    }
}
