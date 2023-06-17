namespace Data.Configurations;

internal class CreatureLootConfiguration : IEntityTypeConfiguration<CreatureLoot>
{
    public void Configure(EntityTypeBuilder<CreatureLoot> entity)
    {
        entity.ToTable(nameof(CreatureLoot));
        
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}
