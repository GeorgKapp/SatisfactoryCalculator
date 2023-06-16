namespace Data.Configurations;

internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> entity)
    {
        entity.HasKey(e => e.ClassName);

        entity.HasOne(d => d.Item)
            .WithOne(p => p.Resource)
            .HasForeignKey<Resource>(d => d.ClassName);
    }
}
