namespace Data.Configurations;

internal class ConsumableConfiguration : IEntityTypeConfiguration<Consumable>
{
    public void Configure(EntityTypeBuilder<Consumable> entity)
    {
        entity.ToTable(nameof(Consumable));
        
        entity.HasKey(p => p.ClassName);

        entity.HasOne(p => p.Item)
            .WithOne(p => p.Consumable)
            .HasForeignKey<Consumable>(p => p.ClassName);
    }
}