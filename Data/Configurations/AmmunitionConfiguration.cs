namespace Data.Configurations;

internal class AmmunitionConfiguration : IEntityTypeConfiguration<Ammunition>
{
    public void Configure(EntityTypeBuilder<Ammunition> entity)
    {
        entity.ToTable(nameof(Ammunition));
        
        entity.HasKey(p => p.ClassName);
        
        entity.HasOne(p => p.Item)
            .WithOne(p => p.Ammunition)
            .HasForeignKey<Ammunition>(p => p.ClassName);
    }
}
