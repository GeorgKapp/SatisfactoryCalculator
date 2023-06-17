namespace Data.Configurations;

internal class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> entity)
    {
        entity.ToTable(nameof(Equipment));
        
        entity.HasKey(e => e.ClassName);
        
        entity.Property(d => d.EquipmentSlot)
            .HasConversion<string>();

        entity.HasOne(d => d.Item)
            .WithOne(p => p.Equipment)
            .HasForeignKey<Equipment>(d => d.ClassName);
    }
}