namespace Data.Configurations;

internal class WeaponConfiguration : IEntityTypeConfiguration<Weapon>
{
    public void Configure(EntityTypeBuilder<Weapon> entity)
    {
        entity.ToTable(nameof(Weapon));
        
        entity.HasKey(p => p.ClassName);
        
        entity.Property(p => p.EquipmentSlot)
            .HasConversion<string>();
        
        entity.HasOne(p => p.Item)
            .WithOne(p => p.Weapon)
            .HasForeignKey<Weapon>(p => p.ClassName);
    }
}
