namespace Data.Configurations;

internal class ScannableObjectConfiguration : IEntityTypeConfiguration<ScannableObject>
{
    public void Configure(EntityTypeBuilder<ScannableObject> entity)
    {
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
        
        entity
            .HasMany(p => p.ScanningActors)
            .WithOne()
            .IsRequired();
    }
}
