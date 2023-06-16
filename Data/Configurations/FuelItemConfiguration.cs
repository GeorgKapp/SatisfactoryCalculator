namespace Data.Configurations;

internal class FuelItemConfiguration : IEntityTypeConfiguration<FuelItem>
{
    public void Configure(EntityTypeBuilder<FuelItem> entity)
    {
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}
