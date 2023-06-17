namespace Data.Configurations;

internal class FuelItemConfiguration : IEntityTypeConfiguration<FuelItem>
{
    public void Configure(EntityTypeBuilder<FuelItem> entity)
    {
        entity.ToTable(nameof(FuelItem));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}
