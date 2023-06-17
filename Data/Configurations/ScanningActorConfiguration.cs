namespace Data.Configurations;

internal class ScanningActorConfiguration : IEntityTypeConfiguration<ScanningActor>
{
    public void Configure(EntityTypeBuilder<ScanningActor> entity)
    {
        entity.ToTable(nameof(ScanningActor));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}
